using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Employee_Profession.DataBase;
using Microsoft.Win32;
using Word = Microsoft.Office.Interop.Word;

namespace Employee_Profession.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        public List<Employee> employees { get; set; }
        public List<Department> departments { get; set; }
        public List<EmployeeInDepartment> employeeInDepartments { get; set; }

        public ReportPage()
        {
            InitializeComponent();
            employees = bd_connection.connection.Employee.Where(x => x.Date_end_service == null).ToList();
            departments = bd_connection.connection.Department.ToList();
            employeeInDepartments = new List<EmployeeInDepartment>();

            foreach (Department dep in departments)
            {
                EmployeeInDepartment newDep = new EmployeeInDepartment();
                newDep.ID = dep.ID;
                newDep.Title = dep.Title;
                newDep.Number_business_rates = dep.Number_business_rates;
                newDep.Count_Employee = 0;

                foreach (Employee emp in employees)
                {
                    if (emp.ID_Department == newDep.ID)
                    {
                        newDep.Count_Employee++;
                    }
                }

                if(newDep.Count_Employee == newDep.Number_business_rates)
                {
                    newDep.DepartFull = "Отдел укомплектован";
                }
                else if (newDep.Count_Employee > newDep.Number_business_rates)
                {
                    newDep.DepartFull = "Сотрудников больше, чем ставок";
                }
                else if (newDep.Count_Employee < newDep.Number_business_rates)
                {
                    newDep.DepartFull = "Нехватка сотрудников";
                }

                employeeInDepartments.Add(newDep);
            }
        }

        private void btn_EmployeeInDepartment_Click(object sender, RoutedEventArgs e)
        {
            var application = new Word.Application();
            Word.Document document = application.Documents.Add();

            Word.Paragraph tableParagraph = document.Paragraphs.Add();
            Word.Range tableRange = tableParagraph.Range;
            Word.Table empInDepTable = document.Tables.Add(tableRange, (employeeInDepartments.Count() + 1), 2);
            empInDepTable.Borders.InsideLineStyle = empInDepTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            empInDepTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

            foreach (var empDep in employeeInDepartments)
            {
                Word.Range cellRange;

                cellRange = empInDepTable.Cell(1, 1).Range;
                cellRange.Text = "Название отдела";
                cellRange = empInDepTable.Cell(1, 2).Range;
                cellRange.Text = "Количество сотрудников";

                empInDepTable.Rows[1].Range.Bold = 1;
                empInDepTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                for(int i = 0; i<employeeInDepartments.Count(); i++)
                {
                    cellRange = empInDepTable.Cell(i + 2, 1).Range;
                    cellRange.Text = employeeInDepartments[i].Title;
                    cellRange = empInDepTable.Cell(i + 2, 2).Range;
                    cellRange.Text = employeeInDepartments[i].Count_Employee.ToString();

                }
            }

            try
            {
                OpenFileDialog openFile = new OpenFileDialog();
                
                
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "КоличествоСотрудниковПоОтделам (*.docx)|*.docx|All Files (*.*)|*.*";
                
                if (saveFileDialog.ShowDialog() == true)
                {
                    document.SaveAs2(saveFileDialog.FileName);
                }
                MessageBox.Show("Успешно!");
            }
            catch
            {
                MessageBox.Show("Вы не выбрали путь для сохранения");
            }
        }

        private void btn_DepartmentsFull_Click(object sender, RoutedEventArgs e)
        {
            var application = new Word.Application();
            Word.Document document = application.Documents.Add();

            Word.Paragraph tableParagraph = document.Paragraphs.Add();
            Word.Range tableRange = tableParagraph.Range;
            Word.Table empInDepTable = document.Tables.Add(tableRange, (employeeInDepartments.Count() + 1), 4);
            empInDepTable.Borders.InsideLineStyle = empInDepTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            empInDepTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

            foreach (var empDep in employeeInDepartments)
            {
                Word.Range cellRange;

                cellRange = empInDepTable.Cell(1, 1).Range;
                cellRange.Text = "Название отдела";
                cellRange = empInDepTable.Cell(1, 2).Range;
                cellRange.Text = "Количество сотрудников";
                cellRange = empInDepTable.Cell(1, 3).Range;
                cellRange.Text = "Количество рабочих ставок";
                cellRange = empInDepTable.Cell(1, 4).Range;
                cellRange.Text = "Укомплектованность";

                empInDepTable.Rows[1].Range.Bold = 1;
                empInDepTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                for (int i = 0; i < employeeInDepartments.Count(); i++)
                {
                    cellRange = empInDepTable.Cell(i + 2, 1).Range;
                    cellRange.Text = employeeInDepartments[i].Title;
                    cellRange = empInDepTable.Cell(i + 2, 2).Range;
                    cellRange.Text = employeeInDepartments[i].Count_Employee.ToString();
                    cellRange = empInDepTable.Cell(i + 2, 3).Range;
                    cellRange.Text = employeeInDepartments[i].Number_business_rates.ToString();
                    cellRange = empInDepTable.Cell(i + 2, 4).Range;
                    cellRange.Text = employeeInDepartments[i].DepartFull;
                }
            }

            try
            {
                OpenFileDialog openFile = new OpenFileDialog();


                SaveFileDialog saveFileDialog = new SaveFileDialog();

                if (saveFileDialog.ShowDialog() == true)
                {
                    document.SaveAs2(saveFileDialog.FileName);
                }
                MessageBox.Show("Успешно!");
            }
            catch
            {
                MessageBox.Show("Вы не выбрали путь для сохранения");
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MenuPage());
        }

        private void btn_EmployeeInWork_Click(object sender, RoutedEventArgs e)
        {
            var application = new Word.Application();
            Word.Document document = application.Documents.Add();

            Word.Paragraph tableParagraph = document.Paragraphs.Add();
            Word.Range tableRange = tableParagraph.Range;
            Word.Table empInDepTable = document.Tables.Add(tableRange, (employees.Count() + 1), 7);
            empInDepTable.Borders.InsideLineStyle = empInDepTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            empInDepTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

            foreach (var empDep in employees)
            {
                Word.Range cellRange;

                cellRange = empInDepTable.Cell(1, 1).Range;
                cellRange.Text = "№";
                cellRange = empInDepTable.Cell(1, 2).Range;
                cellRange.Text = "ФИО";
                cellRange = empInDepTable.Cell(1, 3).Range;
                cellRange.Text = "Пол";
                cellRange = empInDepTable.Cell(1, 4).Range;
                cellRange.Text = "Дата рождения";
                cellRange = empInDepTable.Cell(1, 5).Range;
                cellRange.Text = "Дата приема на работу";
                cellRange = empInDepTable.Cell(1, 6).Range;
                cellRange.Text = "Профессия";
                cellRange = empInDepTable.Cell(1, 7).Range;
                cellRange.Text = "Отдел";

                empInDepTable.Rows[1].Range.Bold = 1;
                empInDepTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                for (int i = 0; i < employees.Count(); i++)
                {
                    cellRange = empInDepTable.Cell(i + 2, 1).Range;
                    cellRange.Text = (i + 1).ToString(); ;
                    cellRange = empInDepTable.Cell(i + 2, 2).Range;
                    cellRange.Text = employees[i].Surname + " " + employees[i].Name + " " + employees[i].Patronymic;
                    cellRange = empInDepTable.Cell(i + 2, 3).Range;
                    cellRange.Text = employees[i].Gender.Title;
                    cellRange = empInDepTable.Cell(i + 2, 4).Range;
                    cellRange.Text = employees[i].Date_of_birth.ToString();
                    cellRange = empInDepTable.Cell(i + 2, 5).Range;
                    cellRange.Text = employees[i].Date_joining_service.ToString();
                    cellRange = empInDepTable.Cell(i + 2, 6).Range;
                    cellRange.Text = employees[i].Profession.Title;
                    cellRange = empInDepTable.Cell(i + 2, 7).Range;
                    cellRange.Text = employees[i].Department.Title;
                }
            }

            try
            {
                OpenFileDialog openFile = new OpenFileDialog();


                SaveFileDialog saveFileDialog = new SaveFileDialog();

                if (saveFileDialog.ShowDialog() == true)
                {
                    document.SaveAs2(saveFileDialog.FileName);
                }
                MessageBox.Show("Успешно!");
            }
            catch
            {
                MessageBox.Show("Вы не выбрали путь для сохранения");
            }
        }

        private void btn_Vacant_Click(object sender, RoutedEventArgs e)
        {
            var application = new Word.Application();
            Word.Document document = application.Documents.Add();

            Word.Paragraph tableParagraph = document.Paragraphs.Add();
            Word.Range tableRange = tableParagraph.Range;
            Word.Table empInDepTable = document.Tables.Add(tableRange, (employeeInDepartments.Count() + 1), 4);
            empInDepTable.Borders.InsideLineStyle = empInDepTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            empInDepTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

            foreach (var empDep in employeeInDepartments)
            {
                Word.Range cellRange;

                cellRange = empInDepTable.Cell(1, 1).Range;
                cellRange.Text = "Название отдела";
                cellRange = empInDepTable.Cell(1, 2).Range;
                cellRange.Text = "Количество сотрудников";
                cellRange = empInDepTable.Cell(1, 3).Range;
                cellRange.Text = "Количество рабочих ставок";
                cellRange = empInDepTable.Cell(1, 4).Range;
                cellRange.Text = "Количество вакантных ставок";

                empInDepTable.Rows[1].Range.Bold = 1;
                empInDepTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                for (int i = 0; i < employeeInDepartments.Count(); i++)
                {
                    cellRange = empInDepTable.Cell(i + 2, 1).Range;
                    cellRange.Text = employeeInDepartments[i].Title;
                    cellRange = empInDepTable.Cell(i + 2, 2).Range;
                    cellRange.Text = employeeInDepartments[i].Count_Employee.ToString();
                    cellRange = empInDepTable.Cell(i + 2, 3).Range;
                    cellRange.Text = employeeInDepartments[i].Number_business_rates.ToString();
                    cellRange = empInDepTable.Cell(i + 2, 4).Range;
                    cellRange.Text = (employeeInDepartments[i].Number_business_rates - employeeInDepartments[i].Count_Employee).ToString();
                }
            }

            try
            {
                OpenFileDialog openFile = new OpenFileDialog();


                SaveFileDialog saveFileDialog = new SaveFileDialog();

                if (saveFileDialog.ShowDialog() == true)
                {
                    document.SaveAs2(saveFileDialog.FileName);
                }
                MessageBox.Show("Успешно!");
            }
            catch
            {
                MessageBox.Show("Вы не выбрали путь для сохранения");
            }
        }

        private void btn_EmployeeInDep_Click(object sender, RoutedEventArgs e)
        {

        }

        public class EmployeeInDepartment
        {
            public int ID { get; set; }
            public string Title { get; set; }
            public int Number_business_rates { get; set; }
            public int Count_Employee { get; set; }
            public string DepartFull { get; set; }
        }
    }
}

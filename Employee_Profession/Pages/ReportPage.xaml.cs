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
        public ReportPage()
        {
            InitializeComponent();
        }

        private void btn_EmployeeInDepartment_Click(object sender, RoutedEventArgs e)
        {
            List<Employee> employees = bd_connection.connection.Employee.Where(x => x.Date_end_service == null).ToList();
            List<Department> departments = bd_connection.connection.Department.ToList();
            List<EmployeeInDepartment> employeeInDepartments = new List<EmployeeInDepartment>();

            foreach(Department dep in departments)
            {
                EmployeeInDepartment newDep = new EmployeeInDepartment();
                newDep.ID = dep.ID;
                newDep.Title = dep.Title;
                newDep.Number_business_rates = dep.Number_business_rates;
                newDep.Count_Employee = 0;

                foreach (Employee emp in employees)
                {
                    if(emp.ID_Department == newDep.ID)
                    {
                        newDep.Count_Employee++;
                    }
                }

                employeeInDepartments.Add(newDep);
            }

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
                    cellRange.Text = employeeInDepartments[i].Count_Employee.ToString(); ;

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

        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MenuPage());
        }

        private void btn_EmployeeInWork_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Vacant_Click(object sender, RoutedEventArgs e)
        {

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
        }
    }
}

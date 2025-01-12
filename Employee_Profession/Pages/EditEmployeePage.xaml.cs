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

namespace Employee_Profession.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditEmployeePage.xaml
    /// </summary>
    public partial class EditEmployeePage : Page
    {
        public Employee editEmployee { get; set; }
        public List<Gender> gender { get; set; }
        public List<Profession> professions { get; set; }
        public List<Department> departments { get; set; }
        public EditEmployeePage(Employee employee)
        {
            InitializeComponent();
            editEmployee = employee;

            gender = bd_connection.connection.Gender.ToList();
            professions = bd_connection.connection.Profession.ToList();
            departments = bd_connection.connection.Department.ToList();

            cb_Gender.ItemsSource = gender;
            cb_Gender.DisplayMemberPath = "Title";

            cb_Department.ItemsSource = departments;
            cb_Department.DisplayMemberPath = "Title";

            cb_Profession.ItemsSource = professions;
            cb_Profession.DisplayMemberPath = "Title";

            if(editEmployee.Name == null)
            {
                btn_delete.Visibility = Visibility.Hidden;
                btn_save.Visibility = Visibility.Hidden;
                btn_save_new.Visibility = Visibility.Visible;
            }
            else
            {
                btn_delete.Visibility = Visibility.Visible;
                btn_save.Visibility = Visibility.Visible;
                btn_save_new.Visibility = Visibility.Hidden;
            }
            DataContext = this;
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Сохрнаить изменения?", "Редактировать", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                try
                {
                    editEmployee.Surname = tb_Surname.Text.Trim();
                    editEmployee.Name = tb_Name.Text.Trim();
                    editEmployee.Patronymic = tb_Patronymic.Text.Trim();
                    editEmployee.Date_of_birth = (DateTime)dp_Birth.SelectedDate;
                    editEmployee.ID_Gender = (cb_Gender.SelectedItem as Gender).ID;
                    editEmployee.Date_joining_service = (DateTime)dp_Joing.SelectedDate;
                    editEmployee.ID_Profession = (cb_Profession.SelectedItem as Profession).ID;
                    editEmployee.ID_Department = (cb_Department.SelectedItem as Department).ID;
                    try
                    {
                        editEmployee.Date_end_service = (DateTime)dp_End.SelectedDate;
                    }
                    catch
                    {
                        editEmployee.Date_end_service = null;
                    }

                    bd_connection.connection.SaveChanges();
                    MessageBox.Show("Успешно!");
                    NavigationService.Navigate(new EmployeePage());
                }
                catch
                {
                    MessageBox.Show("Заполните все обязательные поля");
                }
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeePage());
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
                MessageBoxResult dialogResult = MessageBox.Show("Вы действительно хотите безвозвратно удалить запись и все связанные с ней данные?", "Удаление", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    bd_connection.connection.Employee.Remove(editEmployee);
                    bd_connection.connection.SaveChanges();
                    MessageBox.Show("Успешно!");
                    NavigationService.Navigate(new EmployeePage());
                }
            }

        private void btn_save_new_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Сохрнаить?", "Добавление нового сотрудника", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                try
                {
                    editEmployee.Surname = tb_Surname.Text.Trim();
                    editEmployee.Name = tb_Name.Text.Trim();
                    editEmployee.Patronymic = tb_Patronymic.Text.Trim();
                    editEmployee.Date_of_birth = (DateTime)dp_Birth.SelectedDate;
                    editEmployee.ID_Gender = (cb_Gender.SelectedItem as Gender).ID;
                    editEmployee.Date_joining_service = (DateTime)dp_Joing.SelectedDate;
                    editEmployee.ID_Profession = (cb_Profession.SelectedItem as Profession).ID;
                    editEmployee.ID_Department = (cb_Department.SelectedItem as Department).ID;

                    try
                    {
                        editEmployee.Date_end_service = (DateTime)dp_End.SelectedDate;
                    }
                    catch
                    {
                        editEmployee.Date_end_service = null;
                    }

                    bd_connection.connection.Employee.Add(editEmployee);
                    bd_connection.connection.SaveChanges();
                    MessageBox.Show("Успешно!");
                    NavigationService.Navigate(new EmployeePage());
                }
                catch
                {
                    MessageBox.Show("Заполните все обязательные поля");
                }
            }
        }
    }
}

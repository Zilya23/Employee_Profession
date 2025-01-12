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
            DataContext = this;
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeePage());
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

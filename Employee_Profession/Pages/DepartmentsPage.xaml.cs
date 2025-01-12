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
    /// Логика взаимодействия для DepartmentsPage.xaml
    /// </summary>
    public partial class DepartmentsPage : Page
    {
        public List<Department> departments { get; set; }
        public DepartmentsPage()
        {
            InitializeComponent();
            departments = bd_connection.connection.Department.ToList();
            DataContext = this;
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            Department newDepartment = new Department();
            NavigationService.Navigate(new EditDepartmentPage(newDepartment));
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MenuPage());
        }

        private void lvEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvEmployee.SelectedItem != null)
            {
                var selectDepartment = lvEmployee.SelectedItem as Department;
                NavigationService.Navigate(new EditDepartmentPage(selectDepartment));
            }
        }
    }
}

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
    /// Логика взаимодействия для EditDepartmentPage.xaml
    /// </summary>
    public partial class EditDepartmentPage : Page
    {
        public Department editDepartment { get; set; }
        public EditDepartmentPage(Department department)
        {
            InitializeComponent();
            editDepartment = department;
            DataContext = this;

            if (editDepartment.ID == 0)
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
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Вы действительно хотите безвозвратно удалить запись и все связанные с ней данные?", "Удаление", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                bd_connection.connection.Department.Remove(editDepartment);
                bd_connection.connection.SaveChanges();
                MessageBox.Show("Успешно!");
                NavigationService.Navigate(new DepartmentsPage());
            }
        }

        private void btn_save_new_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Сохрнаить?", "Добавление нового отдела", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                try
                {
                    editDepartment.Title = tb_Title.Text.Trim();
                    try
                    {
                        editDepartment.Number_business_rates = Convert.ToInt32(tb_Number_business_rates.Text.Trim());
                        bd_connection.connection.Department.Add(editDepartment);
                        bd_connection.connection.SaveChanges();
                        MessageBox.Show("Успешно!");
                        NavigationService.Navigate(new DepartmentsPage());
                    }
                    catch
                    {
                        MessageBox.Show("В поле \"Количество рабочих ставок\" введите только числа");
                    }
                }
                catch
                {
                    MessageBox.Show("Заполните все обязательные поля");
                }
            }
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Сохрнаить изменения?", "Редактировать", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                try
                {
                    editDepartment.Title = tb_Title.Text.Trim();
                    try
                    {
                        editDepartment.Number_business_rates = Convert.ToInt32(tb_Number_business_rates.Text.Trim());
                        bd_connection.connection.SaveChanges();
                        MessageBox.Show("Успешно!");
                        NavigationService.Navigate(new DepartmentsPage());
                    }
                    catch
                    {
                        MessageBox.Show("В поле \"Количество рабочих ставок\" введите только числа");
                    }                                       
                }
                catch
                {
                    MessageBox.Show("Заполните все обязательные поля");
                }
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DepartmentsPage());
        }
    }
}

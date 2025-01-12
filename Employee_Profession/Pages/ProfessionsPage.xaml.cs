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
    /// Логика взаимодействия для ProfessionsPage.xaml
    /// </summary>
    public partial class ProfessionsPage : Page
    {
        public List<Profession> professions { get; set; }
        public ProfessionsPage()
        {
            InitializeComponent();
            professions = bd_connection.connection.Profession.ToList();
            DataContext = this;
        }

        private void lvEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvEmployee.SelectedItem != null)
            {
                var selectProfession = lvEmployee.SelectedItem as Profession;
                NavigationService.Navigate(new EditProfessionPage(selectProfession));
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MenuPage());
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            Profession newProfession = new Profession();
            NavigationService.Navigate(new EditProfessionPage(newProfession));
        }
    }
}

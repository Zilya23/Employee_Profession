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

namespace Employee_Profession.Pages
{
    /// <summary>
    /// Логика взаимодействия для DirMainPage.xaml
    /// </summary>
    public partial class DirMainPage : Page
    {
        public DirMainPage()
        {
            InitializeComponent();
        }

        private void btn_Report_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ReportPage());
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RolePage());
        }
    }
}

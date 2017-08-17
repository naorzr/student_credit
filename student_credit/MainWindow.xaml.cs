using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace student_credit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AllCreditsDataGrid.ItemsSource = CoursesCredit.GetAllCoursesCredit();
            Console.WriteLine();
        }

        private void DG_Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = e.OriginalSource as Hyperlink;
            Process.Start(link.NavigateUri.OriginalString);
        }

        private void UpdateIt_Click(object sender, RoutedEventArgs e)
        {
            CoursesCredit.UpdateCourseCreditFile();
            AllCreditsDataGrid.ItemsSource = CoursesCredit.GetAllCoursesCredit();
        }

      

        private void LoadData_Click(object sender, RoutedEventArgs e)
        {
            AllCreditsDataGrid.ItemsSource = CoursesCredit.GetAllCoursesCredit();
        }
    }
}

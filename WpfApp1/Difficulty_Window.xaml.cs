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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Difficulty_Window.xaml
    /// </summary>
    public partial class Difficulty_Window : Window
    {

        public Difficulty_Window()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void Easy_btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow("Easy");
            main.Show();
            this.Close();
        }

        private void Medium_btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow("Medium");
            main.Show();
            this.Close();
        }

        private void Hard_btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow("Hard");
            main.Show();
            this.Close();
        }
    }
}

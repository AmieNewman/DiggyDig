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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string difficulty = "Easy";
        private string backgroundpath = System.IO.Path.Combine(Environment.CurrentDirectory, @"images\background1.jpg");
        public MainWindow()
        {
            InitializeComponent();
            //setting properties of image
            ImageBrush image = new ImageBrush(new BitmapImage(new Uri(backgroundpath)));
            image.Stretch = Stretch.Fill;
            this.Background = image;
                
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        public MainWindow(string given_diff)
        {
            difficulty = given_diff;
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void Start_btn_Click(object sender, RoutedEventArgs e)
        {
            Game_Window game = new Game_Window(difficulty);
            game.Show();
            this.Close();
        }

        private void About_btn_Click(object sender, RoutedEventArgs e)
        {
            About_Window about = new About_Window();
            about.Show();
            this.Close();
        }

        private void Difficulty_btn_Click(object sender, RoutedEventArgs e)
        {
            Difficulty_Window diff = new Difficulty_Window();
            diff.Show();
            this.Close();
        }
    }
}

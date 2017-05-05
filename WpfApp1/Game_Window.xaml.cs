using System;
using System.Windows.Threading;
using System.IO;
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
using System.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Game_Window.xaml
    /// </summary>
    public partial class Game_Window : Window
        //pathing to the images for the buttons. 
    {
        public int mine_count = 3;
        //minecount used for lives too
        public int goldcount = 0;
        public int time_left = 30;
        private DispatcherTimer timer = new DispatcherTimer();
        private string rockPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"images\rock.PNG");
        private string backgroundPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"images\background2.png");
        private string GoldPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"images\Gold.png");
        private string boomPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"images\boom.png");
        private string DirtPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"images\Dirt.png");
        private string boom_soundPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"sounds\boom.wav");
        private string woosh_soundPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"sounds\woosh.wav");
        private string money_soundPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"sounds\money.wav");


        public Game_Window(string given_diff)
        {
            InitializeComponent();
            ImageBrush image = new ImageBrush(new BitmapImage(new Uri(backgroundPath)));
            image.Stretch = Stretch.Fill;
            this.Background = image;
            //centers the window based on the size of the screen ;divided by two so it finds the center, both ways
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);

            scoreandlives.Text = "lives: " + mine_count.ToString() + " score: " + goldcount.ToString();
            //creates grid of buttons
            Create_Grid(given_diff);
        }

        private void Default_Clicked_State(object sender, EventArgs e)
        {
            var button = (sender as Button);
            if (button != null)
            {
                SoundPlayer woosh = new SoundPlayer(woosh_soundPath);
                woosh.Play();
                button.Content = new Image
                {
                    //default click is just smashed rock image- where the button equals the image(smashed rock) 
                    Source = new BitmapImage(new Uri(rockPath)),
                    Stretch = Stretch.UniformToFill
                };
            }
        }

        private void Boat_Clicked_State(object sender, EventArgs e)
        {
            var button = (sender as Button);
            if (button != null)
            {
                SoundPlayer money = new SoundPlayer(money_soundPath);
                money.Play();
                goldcount+=10;
                scoreandlives.Text = "lives: " + mine_count.ToString() + " score: " + goldcount.ToString();
                button.Content = new Image
                {
                    Source = new BitmapImage(new Uri(GoldPath)),
                    Stretch = Stretch.UniformToFill
                };
            }
        }

        private void Mine_Clicked_State(object sender, EventArgs e)
            //game over when you hit 3 "mines" (coal rock things)
        {
            var button = (sender as Button);
            mine_count--;
            if (button != null)
            {
                SoundPlayer boom = new SoundPlayer(boom_soundPath);
                boom.Play();
                scoreandlives.Text = "lives: " + mine_count.ToString() + " score: " + goldcount.ToString();
                if (mine_count != 0)
                {
                    button.Content = new Image
                    {
                        Source = new BitmapImage(new Uri(boomPath)),
                        Stretch = Stretch.UniformToFill
                    };
                }
                else
                {
                    timer.Stop();
                    Game_Over_Window gameOver = new Game_Over_Window();
                    gameOver.Show();
                    this.Close();
                }
            }
        }

        private void timerCount(object sender, EventArgs e)
        {
            if(time_left > 0)
            {
                time_left--;
                countdown.Text = time_left.ToString() + "s";
            }
            else
            {
                timer.Stop();
                Game_Over_Window gameOver = new Game_Over_Window();
                gameOver.Show();
                this.Close();
            }
        }

        void Create_Grid(string difficulty)
            //difficulty settings that change size of board based on the level. given the difficulty, it makes the game board 
            //never changed variable names, but the ships and mines are...mines...and gold
        {
            if (difficulty == "Easy")
            {
                Random easy_random = new Random();
                int Mine_rand;
                int Ship_rand;
                for (int i = 0; i < 10; i++)
                {
                    //so, 10x10
                    Mine_rand = easy_random.Next(0, 11);
                    Ship_rand = easy_random.Next(0, 11);
                    for (int j = 0; j < 10; j++)
                    {
                        Button b = new Button();
                        b.Height = 20;
                        b.Width = 20;
                        b.Margin = new Thickness(0, 0, (40 * j), (40 * i));
                        b.Content = new Image
                        {
                            Source = new BitmapImage(new Uri(DirtPath)),
                            Stretch = Stretch.Fill
                        };
                        if (Mine_rand == j)
                        {
                            b.Click += new RoutedEventHandler(Mine_Clicked_State);
                        }
                        else if (Ship_rand == j)
                        {
                            b.Click += new RoutedEventHandler(Boat_Clicked_State);
                        }
                        else
                        {
                            b.Click += new RoutedEventHandler(Default_Clicked_State);
                        }
                        buttons.Children.Add(b);
                    }
                }
                timer.Tick += new EventHandler(timerCount);
                timer.Interval = new TimeSpan(0, 0, 1);

                timer.Start();
            }

            else if (difficulty == "Medium")
            {
                this.Height = 600;
                this.Width = 600;
                Random Medium_random = new Random();
                int Mine_rand;
                int Ship_rand;
                for (int i = 0; i < 15; i++)
                {
                    Mine_rand = Medium_random.Next(0, 16);
                    Ship_rand = Medium_random.Next(0, 16);
                    for (int j = 0; j < 15; j++)
                    {
                        Button b = new Button();
                        b.Height = 20;
                        b.Width = 20;
                        b.HorizontalAlignment = HorizontalAlignment.Center;
                        b.VerticalAlignment = VerticalAlignment.Center;
                        b.Margin = new Thickness(0, 0, (40 * j), (40 * i));
                        b.Background = Brushes.Brown;
                        b.Content = new Image
                        {
                            Source = new BitmapImage(new Uri(DirtPath)),
                            Stretch = Stretch.Fill
                        };
                        if (Mine_rand == j)
                        {
                            b.Click += new RoutedEventHandler(Mine_Clicked_State);
                        }
                        else if (Ship_rand == j)
                        {
                            b.Click += new RoutedEventHandler(Boat_Clicked_State);
                        }
                        else
                        {
                            b.Click += new RoutedEventHandler(Default_Clicked_State);
                        }
                        buttons.Children.Add(b);
                    }
                }
                //timer again
                timer.Tick += new EventHandler(timerCount);
                timer.Interval = new TimeSpan(0, 0, 1);

                timer.Start();
            }

            else
            { 
                this.Height = 820;
                this.Width= 820;
                Random hard_random = new Random();
                int Mine_rand;
                int Ship_rand;
                for (int i = 0; i < 20; i++)
                {
                    Mine_rand = hard_random.Next(0, 21);
                    Ship_rand = hard_random.Next(0, 21);
                    for (int j = 0; j < 20; j++)
                    {
                        Button b = new Button();
                        b.Height = 20;
                        b.Width = 20;
                        b.HorizontalAlignment = HorizontalAlignment.Center;
                        b.VerticalAlignment = VerticalAlignment.Center;
                        b.Margin = new Thickness(0, 0, (40 * j), (40 * i));
                        b.Background = Brushes.Brown;
                        b.Content = new Image
                        {
                            Source = new BitmapImage(new Uri(DirtPath)),
                            Stretch = Stretch.Fill
                        };
                        if (Mine_rand == j)
                        {
                            b.Click += new RoutedEventHandler(Mine_Clicked_State);
                        }
                        else if (Ship_rand == j)
                        {
                            b.Click += new RoutedEventHandler(Boat_Clicked_State);
                        }
                        else
                        {
                            b.Click += new RoutedEventHandler(Default_Clicked_State);
                        }
                        buttons.Children.Add(b);
                    }
                }
                //timer again ( had to do it multiple times to work in code for each difficulty/instance 
                timer.Tick += new EventHandler(timerCount);
                timer.Interval = new TimeSpan(0, 0, 1);

                timer.Start();

            }
        }    

    }
}

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
using System.Windows.Threading;
using System.Timers;
using System.Diagnostics;

namespace Pac_man
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Stopwatch sw;
        Dispatcher h;
        DispatcherTimer stopwatch;
        Timer game_Timer;
        Timer pacman_Timer;
        Timer pink_Timer;
        Timer ghosts_timer;
        
        Walls walls;
        Constraints constraints;
        Controller control;
        Food food;

        Player pMan;
        Ghost_Blinky Blinky;
        Ghost_Controller ghost_control;
         
        public MainWindow()
        {
            InitializeComponent();
            instatiate_Timer();
            GUI_and_KeyBoard_Responses();
            walls = new Walls(Board);
            
        }

        void GUI_and_KeyBoard_Responses()
        {
            if (this.WindowState == System.Windows.WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
            }
            Closing += Window_Closing;
            KeyDown += Windows_KeyDown;
            score_500.Visibility = Visibility.Hidden;
            game_Over.Visibility = Visibility.Hidden;
        }

        void instatiate_Timer()
        {
            sw = new Stopwatch();
            h = Dispatcher.CurrentDispatcher;
            stopwatch = new DispatcherTimer();
            pink_Timer = new Timer();
            ghosts_timer = new Timer();
            pacman_Timer = new Timer();

            game_Timer = new Timer();
            game_Timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            game_Timer.Interval = 100;
            game_Timer.Start();
            game_Timer.Enabled = false;  
        }
        
        void instatiate_Ghosts()
        {
            Blinky = new Ghost_Blinky(Board, control, constraints, walls);
            ghost_control = new Ghost_Controller(Blinky,stopwatch,pink_Timer,ghosts_timer);
        }

        void initialize_GUIs()
        {
            List<Label> lbs = new List<Label> { score_label, Score, Time_label, Time, Level_label, Level,player };
            for (int i = 0; i < lbs.Count; i++) Canvas.SetLeft(lbs[i], Board.ActualWidth + 20);
            Canvas.SetLeft(player_1, Board.ActualWidth + 20);
            Canvas.SetLeft(game_Over, Board.ActualWidth / 3.6);
            Canvas.SetLeft(player_2, Canvas.GetLeft(player_1) + player_1.ActualWidth+10);
            Canvas.SetLeft(player_3, Canvas.GetLeft(player_2) + player_2.ActualWidth+10);
            Canvas.SetLeft(button1, Board.ActualWidth + 20);
            Canvas.SetLeft(restart, Board.ActualWidth + 20);
            Canvas.SetLeft(Quit, Board.ActualWidth + 20);
        }

        void clean_Board()
        {
            for(int i=0;i<Blinky.Gs.Count;i++)
            {
                Board.Children.Remove(Blinky.Gs[i]);
            }
            for(int i=0;i<walls.theWall.Count;i++)
            {
                Board.Children.Remove(walls.theWall[i]);
            }
            food.food_cleanUp();
            Blinky.clear_blinky_path();
            Blinky.clear_pinky_path();
            Blinky.clear_inky_path();
            Blinky.clear_clyde_path();
            Board.Children.Remove(pMan.p_man);
        }

        void update_GUIs()
        {
            Score.Content = control.score;
            Time.Content = sw.Elapsed;
            if (pMan.pacman_lives == 2) player_3.Visibility = Visibility.Hidden;
            if (pMan.pacman_lives == 1) player_2.Visibility = Visibility.Hidden;
            if (pMan.pacman_lives == 0)
            {
                clean_Board();
                sw.Stop();
                game_Over.Visibility = Visibility.Visible;
                player_1.Visibility = Visibility.Hidden;
                /*turn_timers_off();
                stopwatch.IsEnabled = true;
                game_Timer.Enabled = true;*/
            }
            if (ghost_control.game_Over_seconds == 0) welcome_Screen.Visibility = Visibility.Visible;
            if (ghost_control.score_500_seconds == 0) score_500.Visibility = Visibility.Hidden;
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                h.Invoke(new Action(() =>
                  {
                      if (Blinky.ghost_Is_Eaten)
                      {
                          pMan.pacman_eat_ghost = true;
                          Blinky.ghost_Is_Eaten = false;
                          score_500.Visibility = Visibility.Visible;
                          Canvas.SetLeft(score_500, Blinky.gs_eaten_left);
                          Canvas.SetTop(score_500, Blinky.gs_eaten_top-15);
                      }
                      control.Advance();
                      update_GUIs();
                      GC.Collect();
                  }));
            }
            catch(Exception)
            {
                return;
            }
        }

        private void Windows_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.L:
                    food.fff();
                    break;
                case Key.Up:
                    if (game_Timer.Enabled)
                    {
                        pMan.direction = 0;
                        control.rePostion_Sprite_wallHit(pMan.p_man);
                    }
                    break;
                case Key.Right:
                    if (game_Timer.Enabled)
                    {
                        pMan.direction = 1;
                        control.rePostion_Sprite_wallHit(pMan.p_man);
                    }
                    break;
                case Key.Down:
                    if (game_Timer.Enabled)
                    {
                        pMan.direction = 2;
                        control.rePostion_Sprite_wallHit(pMan.p_man);
                    }
                    break;
                case Key.Left:
                    if (game_Timer.Enabled)
                    {
                        pMan.direction = 3;
                        control.rePostion_Sprite_wallHit(pMan.p_man);
                    }
                    break;
                case Key.Escape:
                    QuitGame();
                    break;
                case Key.A:
                    startGame();
                    break;
                case Key.P:
                    pauseGame();
                    break;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            QuitGame();
        }

        void turn_timers_on()
        {
            pacman_Timer.Enabled = true;
            ghosts_timer.Enabled = true;
            pink_Timer.Enabled = true;
            stopwatch.IsEnabled = true;
            game_Timer.Enabled = true;
        }
        void turn_timers_off()
        {
            pacman_Timer.Enabled = false;
            ghosts_timer.Enabled = false;
            pink_Timer.Enabled = false;
            stopwatch.IsEnabled = false;
            game_Timer.Enabled = false;
        }

        int p = 0;
        void startGame()
        {
            //Instatiate clases
            if(p==1)clean_Board();
            food = new Food(Board, walls);
            food.create_Food();
            pMan = new Player(Board);
            constraints = new Constraints(Board, pMan, walls.theWall, food.theFood);
            control = new Controller(Board, pMan, pacman_Timer, walls.theWall, food);
            instatiate_Ghosts();
            control.k = 0;
            p = 1;

            //Game components
            walls.walls_Build_Up();
            initialize_GUIs();

            //Activate timers
            turn_timers_on();
            sw.Reset();
            sw.Start();
            
            //Positioning of the ghosts and turning points layouts
            pMan.pMan_starting_Pos();
            Blinky.ghosts_initial_Pos();
            Blinky.show_Ghosts();
            Blinky.Clyde_path();
            Blinky.Inky_path();
            Blinky.Pinky_path();
            Blinky.Blinky_path();

            game_Over.Visibility = Visibility.Hidden;
            welcome_Screen.Visibility = Visibility.Hidden;
            List<Image> t = new List<Image> { player_1, player_2, player_3 };
            for (int i = 0; i < t.Count; i++) t[i].Visibility = Visibility.Visible;
        }

        void QuitGame()
        {
            turn_timers_off();
            sw.Stop();

            if (MessageBox.Show("Are you sure you want to quit?", "Quit game", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
            else
            {
                turn_timers_on();
                sw.Start();
            }
        }

        int k = 0;
        void pauseGame()
        {
            game_Timer.Enabled = !game_Timer.Enabled;
            pink_Timer.Enabled = !pink_Timer.Enabled;
            ghosts_timer.Enabled = !ghosts_timer.Enabled;
            stopwatch.IsEnabled = !stopwatch.IsEnabled;
            if (k % 2 == 0)
            {
                button1.Content = "Resume";
                pacman_Timer.Enabled = false;
            }
            else
            {
                button1.Content = "Pause";
                pacman_Timer.Enabled = true;
            }
            k++;
        }
        
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            pauseGame();
        }

        private void restart_Click(object sender, RoutedEventArgs e)
        {
            turn_timers_off();
            sw.Stop();

            if (MessageBox.Show("Are you sure you want to restart game?", "Restart game", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                clean_Board();
                startGame();
            }
            else
            {
                turn_timers_on();
                sw.Start();
            }
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            QuitGame();
        }
    }
}

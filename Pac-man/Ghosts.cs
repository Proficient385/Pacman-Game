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

namespace Pac_man
{
    class Ghosts:Window
    {
        public int inky_direction { get; set; }
        public Image Inky { get; set; }

        public int p_direction { get; set; }
        public Image Pinky { get; set; }

        public int b_direction { get; set; }
        public Image Blinky { get; set; }

        public int c_direction { get;set; }
        public Image Clyde { get; set; }

        bool top;
        bool left;
        bool LEFT;
        bool RIGHT;
        bool DOWN;
        bool TOP;

        public List<int> which_ghosts_eaten { get; set; }
        public BitmapImage[] myGhosts { get; private set; }
        public List<Image> Gs { get; private set; }

        public bool ghost_Is_Eaten { get; set; }
        public double gs_eaten_top { get; private set; }
        public double gs_eaten_left { get; private set; }


        public Canvas Board { get; private set; }
        public Controller control { get; private set; }
        public Constraints constraints { get; private set; }

        public Ghosts(Canvas Board, Controller control, Constraints constraints)
        {
            this.control = control;
            this.constraints = constraints;
            this.Board = Board;

            which_ghosts_eaten = new List<int>();
            load_images();
            instantiate_Ghosts();
            Gs = new List<Image> { Inky, Pinky, Blinky, Clyde };

            top = false;
            left = false;
            LEFT = false;
            RIGHT = false;
            DOWN = false;
            TOP = false;

            ghost_Is_Eaten = false;
            gs_eaten_top = 0;
            gs_eaten_left = 0;
        }

        void load_images()
        {
            string path = "pack://application:,,,/";
            myGhosts = new BitmapImage[] { new BitmapImage(new Uri(path + "INKY.jpg")),
                                           new BitmapImage(new Uri(path+"PINKY.jpg")),
                                           new BitmapImage(new Uri(path+"BLINKY.jpg")),
                                           new BitmapImage(new Uri(path+"CLYDE.jpg")),
                                           new BitmapImage(new Uri(path+"FOOD.jpg"))};
        }

        void ghost_Dimensions(List<Image> r)
        {
            foreach(Image t in r)
            {
                t.Width = (int)Board.ActualWidth / 22;
                t.Height = (int)Board.ActualHeight / 22;
            }
        }

        void instatiate_ghosts(List<Image> xs)
        {
            for(int i=0;i<xs.Count;i++)
            {
                xs[i].Source = myGhosts[i];
                Board.Children.Add(xs[i]);
                xs[i].Visibility = Visibility.Hidden;
            }
        }

        void instantiate_Ghosts()
        {
            Inky = new Image();
            Pinky = new Image();
            Blinky = new Image();
            Clyde = new Image();
            instatiate_ghosts(new List<Image> { Inky, Pinky, Blinky, Clyde });     
        }

        public void set_Left_Top(double left, double top, Image r)
        {
            Canvas.SetLeft(r, Board.ActualWidth / left);
            Canvas.SetTop(r, Board.ActualHeight / top);
        }

        public void ghosts_initial_Pos()
        {
            ghost_Dimensions(new List<Image> { Inky, Pinky, Blinky, Clyde});
            set_Left_Top(2.3, 2.15, Inky);
            set_Left_Top(2, 2.15, Pinky);
            set_Left_Top(1.8, 2.15, Blinky);
            set_Left_Top(1.85, 2.87, Clyde);
            inky_direction = 0;
            p_direction = 0;
            b_direction = 0;
            c_direction = 1;
        }

        public void show_Ghosts()
        {
            Inky.Visibility = Visibility.Visible;
            Pinky.Visibility = Visibility.Visible;
            Blinky.Visibility = Visibility.Visible;
            Clyde.Visibility = Visibility.Visible;
        }
        
        public void special_Food_Eaten()
        {
            if (which_ghosts_eaten.IndexOf(1) == -1) Pinky.Source = myGhosts[4];
            if(which_ghosts_eaten.IndexOf(0)==-1) Inky.Source = myGhosts[4];
            if (which_ghosts_eaten.IndexOf(3)==-1) Clyde.Source = myGhosts[4];
            if (which_ghosts_eaten.IndexOf(2)==-1) Blinky.Source = myGhosts[4];
        }

        public void reset_ghosts()
        {
            List<Image> Gs = new List<Image> { Inky, Pinky, Blinky, Clyde };
            for (int i = 0; i < Gs.Count; i++) Gs[i].Source = myGhosts[i];
        }
        
        void update_Bools(Image ghost)
        {
            top = Math.Abs(constraints.top(control.P.p_man) - constraints.top(ghost)) < ghost.ActualHeight;
            left = Math.Abs(constraints.left(control.P.p_man) - constraints.left(ghost)) < ghost.ActualWidth;
            
            LEFT = constraints.left(ghost) >= constraints.left(ghost) && top && left;
            RIGHT = constraints.left(ghost) <= constraints.left(ghost) && top && left;
            TOP = constraints.top(ghost) >= constraints.top(ghost) && top && left;
            DOWN = constraints.top(ghost) <= constraints.top(ghost) && top && left;
        }
        
        public bool ghost_Hit_Pacman()
        {
            for(int i=0;i<Gs.Count;i++)
            {
                update_Bools(Gs[i]);
                if (TOP || DOWN || LEFT || RIGHT)
                {
                    if (Gs[i].Source == myGhosts[4])
                    {
                        which_ghosts_eaten.Add(i);
                        ghost_Is_Eaten = true;
                        gs_eaten_left = Canvas.GetLeft(Gs[i]);
                        gs_eaten_top = Canvas.GetTop(Gs[i]);
                    }
                    return true;
                }
            }
            return false;
        }

    }
}

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

namespace Pac_man
{
    class Player:Window
    {
        private BitmapImage[] pMan;
        private BitmapImage[][] pMan_Pieces;
        public Image p_man { get; private set; } 
        public int direction { get; set; }
        public bool pacman_eat_ghost { get; set; }
        public int pacman_lives { get; set; }
        Canvas Board;
        Walls w;
        
        public Player(Canvas Board)
        {
            direction = 3;
            pacman_eat_ghost = false;
            pacman_lives = 3;
            this.Board = Board;
            player_All_Pieces();
            player_Build_Up();
            pMan = pMan_Pieces[direction];
            w = new Walls(Board);
        }

        void player_All_Pieces()
        {
            string inThisProject = "pack://application:,,,/";
            pMan_Pieces = new BitmapImage[][] {new BitmapImage[] {
                                                new BitmapImage(new Uri(inThisProject+ "pMan_Up1.jpg")),
                                                new BitmapImage(new Uri(inThisProject+ "pMan_Up2.png"))},
                                               new BitmapImage[] {
                                                new BitmapImage(new Uri(inThisProject+ "pMan_Right1.jpg")),
                                                new BitmapImage(new Uri(inThisProject+ "pMan_Right2.png"))},
                                               new BitmapImage[] {
                                                new BitmapImage(new Uri(inThisProject+ "pMan_Down1.jpg")),
                                                new BitmapImage(new Uri(inThisProject+ "pMan_Down2.png"))},
                                               new BitmapImage[] {
                                                new BitmapImage(new Uri(inThisProject+ "pMan_Left1.jpg")),
                                                new BitmapImage(new Uri(inThisProject+ "pMan_Left2.png"))},
                                               new BitmapImage[] {
                                                new BitmapImage(new Uri(inThisProject+ "pman_dead_1.png")),
                                                new BitmapImage(new Uri(inThisProject+ "pman_dead_2.png")),
                                                new BitmapImage(new Uri(inThisProject+ "pman_dead_3.png")),
                                                new BitmapImage(new Uri(inThisProject+ "pman_dead_4.png"))} };
        }

        void player_Build_Up()
        {
            p_man = new Image();
            Board.Children.Add(p_man);
        }

        void player_Dimensions()
        {
            p_man.Width = (int)Board.ActualWidth / 25;
            p_man.Height = (int)Board.ActualHeight / 25;
        }

        int y = 0;
        public void pacman_dead()
        {
            pMan = pMan_Pieces[4];
            p_man.Source = pMan[y];
            y = (y + 1) % 4;
        }

        public void pMan_starting_Pos()
        {
            player_Dimensions();
            p_man.Source = new BitmapImage(new Uri("pack://application:,,,/pMan_start.png"));
            Canvas.SetLeft(p_man, (int)(Board.ActualWidth / 2.13));
            Canvas.SetTop(p_man, (int)(Board.ActualHeight / 1.83));
            direction = 1;
            p_man.Visibility = Visibility.Visible;
        }

        int k = 0;
        public void Advance()
        {   
            pMan = pMan_Pieces[direction];
            p_man.Source = pMan[k];
            k = (k + 1) % 2;
        }
    }
}

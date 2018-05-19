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
    class Ghost_Blinky:Ghost_Pinky
    {
        List<Button> path;
        Walls wall;

        bool top;
        bool left;
        bool LEFT;
        bool RIGHT;
        bool DOWN;
        bool TOP;

        public Ghost_Blinky(Canvas Board, Controller control, Constraints constraints, Walls wall) : base(Board, control, constraints,wall)
        {
            this.wall = wall;
            path = new List<Button>();
            b_direction = 0;
            top = false;
            left = false;
            LEFT = false;
            RIGHT = false;
            DOWN = false;
            TOP = false;
        }

        public void blinky_starting_Pos()
        {
            set_Left_Top(1.8, 2.15, Blinky);
            b_direction = 0;
            Blinky.Source = myGhosts[2];
        }

        public void move_Blinky()
        {
            control.move_Sprite(b_direction, Blinky);
        }
        
        void update_Bools(int i)
        {
            top = Math.Abs(constraints.top(Blinky) - constraints.top2(path[i])) < Blinky.ActualHeight;
            left = Math.Abs(constraints.left(Blinky) - constraints.left2(path[i])) < Blinky.ActualWidth;
            LEFT = constraints.left(Blinky) >= constraints.left2(path[i]) && top && left;
            RIGHT = constraints.left(Blinky) <= constraints.left2(path[i]) && top && left;
            TOP = constraints.top(Blinky) >= constraints.top2(path[i]) && top && left;
            DOWN = constraints.top(Blinky) <= constraints.top2(path[i]) && top && left;
        }

        public void Blinky_path()
        {
            //TURNING POINT CO_ORDINATES
            wall.path_Layout(2.35,1.8, path);
            wall.path_Layout(2.35, 2.06, path);
            wall.path_Layout(2.84, 2.06, path);
            wall.path_Layout(2.84, 1.85, path);
            wall.path_Layout(3.7, 1.85, path);//i==4
            wall.path_Layout(3.7, 1.565, path);
            wall.path_Layout(5.8, 1.565, path);
            wall.path_Layout(5.8, 1.35, path);
            wall.path_Layout(2.22, 1.35, path);
            wall.path_Layout(2.22, 1.565, path);//i==9
            wall.path_Layout(1.84, 1.565, path);
            wall.path_Layout(1.84, 3, path);
            wall.path_Layout(1.58, 3, path);
            wall.path_Layout(1.58, 2.3, path);
            wall.path_Layout(1.37, 2.3, path);//i==14
            wall.path_Layout(1.37, 1.85, path);
            wall.path_Layout(1.56, 1.85, path);
            wall.path_Layout(1.56, 1.35, path);
            wall.path_Layout(1.21, 1.35, path);
            wall.path_Layout(1.21, 1.09, path);
            wall.path_Layout(1.09, 1.09, path);
            wall.path_Layout(1.09, 2.35, path);
            wall.path_Layout(1.21, 2.35, path);
            wall.path_Layout(1.21, 3, path);//i==23
            wall.path_Layout(1.36, 3, path);
            wall.path_Layout(1.36, 4.5, path);
            wall.path_Layout(1.21, 4.5, path);
            wall.path_Layout(1.21, 7.8, path);
            wall.path_Layout(1.36, 7.8, path);
            wall.path_Layout(1.36, 21, path);
            wall.path_Layout(1.56, 21, path);
            wall.path_Layout(1.56, 4.5, path);//i==31
            wall.path_Layout(3.7, 4.5, path);
            wall.path_Layout(3.7, 21, path);
            wall.path_Layout(5.8, 21, path);
            wall.path_Layout(5.8, 4.5, path);
            wall.path_Layout(20, 4.5, path);
            wall.path_Layout(20, 2.35, path);
            wall.path_Layout(6, 2.35, path);
            wall.path_Layout(6, 3, path);
            wall.path_Layout(3.8, 3, path);
            wall.path_Layout(3.8, 2.35, path);
            wall.path_Layout(2.84, 2.35, path);//i==42
        }

        void direct_Blinky()
        {
            for (int i = 0; i < path.Count; i++)
            {
                update_Bools(i);
                bool i0 = (i == 0 || i == 22 || i == 24 || i == 28 || i == 32);
                bool i1 = (i == 1 || i == 21 || i == 23 || i == 27 || i == 29 || i == 33);
                bool i2 = (i == 2 || i == 4 || i == 4 || i == 6 || i == 16 || i == 30 || i == 34 || i == 36);
                bool i3 = (i == 3 || i == 5 || i == 15 || i == 31 || i == 35);
                bool i7 = (i == 7 || i == 13 || i == 17 || i == 19 || i == 37 || i == 41);
                bool i8 = (i == 8 || i == 10 || i == 20 || i == 26 || i == 38);
                bool i9 = (i == 9 || i == 11 || i == 25 || i == 39);
                bool i12 = (i == 12 || i == 14 || i == 18 || i == 40 || i == 42);

                if (DOWN && i0) b_direction = 3;
                if (RIGHT && i1) b_direction = 0;
                if (DOWN && i2) b_direction = 1;
                if (LEFT && i3) b_direction = 0;
                if (LEFT && i7) b_direction = 2;
                if (TOP && i8) b_direction = 3;
                if (RIGHT && i9) b_direction = 2;
                if (TOP && i12) b_direction = 1;
            }
        }

        public void clear_blinky_path()
        {
            for (int i = 0; i < path.Count; i++)
            {
                Board.Children.Remove(path[i]);
            }
        }

        public void Blinky_Advance()
        {
            //color_Blinky_path();
            direct_Blinky();
            move_Blinky();
            control.reset_Sprite_Exit_Wall(Blinky);
        }
    }
}

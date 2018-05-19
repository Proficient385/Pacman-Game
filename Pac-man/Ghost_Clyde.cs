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
    class Ghost_Clyde:Ghosts
    {
        List<Button> path;
        Walls wall;

        bool top;
        bool left;
        bool LEFT;
        bool RIGHT;
        bool DOWN;
        bool TOP;

        public Ghost_Clyde(Canvas Board, Controller control, Constraints constraints, Walls wall) : base(Board, control, constraints)
        {
            this.wall = wall;
            path = new List<Button>();
            c_direction = 1;
            top = false;
            left = false;
            LEFT = false;
            RIGHT = false;
            DOWN = false;
            TOP = false;
        }

        public void clyde_starting_Pos()
        {
            set_Left_Top(1.85, 2.87, Clyde);
            c_direction = 1;
            Clyde.Source = myGhosts[3];
        }

        public void move_Clyde()
        {
            control.move_Sprite(c_direction, Clyde);
        }
        
        void update_Bools(int i)
        {
            top = Math.Abs(constraints.top(Clyde) - constraints.top2(path[i])) < Clyde.ActualHeight;
            left = Math.Abs(constraints.left(Clyde) - constraints.left2(path[i])) < Clyde.ActualWidth;
            LEFT = constraints.left(Clyde) >= constraints.left2(path[i]) && top && left;
            RIGHT = constraints.left(Clyde) <= constraints.left2(path[i]) && top && left;
            TOP = constraints.top(Clyde) >= constraints.top2(path[i]) && top && left;
            DOWN = constraints.top(Clyde) <= constraints.top2(path[i]) && top && left;
        }

        public void Clyde_path()
        {
            //Turning Points
            wall.path_Layout(2.87, 1.55, path);
            wall.path_Layout(2.23, 1.55, path);
            wall.path_Layout(2.23, 1.35, path);
            wall.path_Layout(6, 1.35, path);
            wall.path_Layout(6, 1.085, path);
            wall.path_Layout(20, 1.085, path);
            wall.path_Layout(20, 1.85, path);
            wall.path_Layout(6, 1.85, path);
            wall.path_Layout(6, 2.35, path);
            wall.path_Layout(20, 2.35, path);
            wall.path_Layout(20, 20, path);
            wall.path_Layout(3.8, 20, path);
            wall.path_Layout(3.8, 4.65, path);
            wall.path_Layout(1.375, 4.65, path);
            wall.path_Layout(1.375, 3.1, path);
            wall.path_Layout(1.22, 3.1, path);
            wall.path_Layout(1.22, 2.35, path);
            wall.path_Layout(1.09, 2.35, path);
            wall.path_Layout(1.09, 1.85, path);
            wall.path_Layout(1.22, 1.85, path);
            wall.path_Layout(1.22, 1.565, path);
            wall.path_Layout(1.375, 1.565, path);
            wall.path_Layout(1.375, 1.35, path); //Turning Points
        }
        
        void direct_Clyde()
        {
            for (int i = 0; i < path.Count; i++)
            {
                update_Bools(i);
                bool i0 = (i == 0 || i == 12 || i == 14 || i == 16);
                bool i1 = (i == 1 || i == 11 || i == 13 || i == 15 || i == 17);
                bool i2 = (i == 2 || i == 18 || i == 20 || i == 22);
                bool i3 = (i == 3 || i == 19 || i == 21);
                bool i5 = (i == 5 || i == 9);
                bool i6 = (i == 6 || i == 10);
                
                if (LEFT && i0) c_direction = 2;
                if (TOP && i1) c_direction = 1;
                if (LEFT && i2) c_direction = 0;
                if (DOWN && i3) c_direction = 1;
                if (LEFT && i == 4) c_direction = 0;
                if (DOWN && i5) c_direction = 3;
                if (RIGHT && i6) c_direction = 2;
                if (TOP && i == 7) c_direction = 3;
                if (RIGHT && i == 8) c_direction = 0;
            }
        }

        public void clear_clyde_path()
        {
            for (int i = 0; i < path.Count; i++)
            {
                Board.Children.Remove(path[i]);
            }
        }

        public void clyde_Advance()
        {
            //color_Clyde_path();
            direct_Clyde();
            move_Clyde();
        }

    }
}

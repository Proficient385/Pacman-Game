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
    class Walls:Window
    {
        Canvas Board;
        public List<Button> theWall { get; private set; }
        public List<Button> theWall2 { get; private set; }

        public Walls(Canvas x)
        {
            Board = x;
            theWall = new List<Button>();
            theWall2 = new List<Button>();
        }

        public Button wall_Build()
        {
            Button Wall = new Button();
            Wall.Focusable = false;
            Wall.Background = null;
            Wall.BorderBrush = null;
            Board.Children.Add(Wall);
            return Wall;
        }

        void wall_layout(double top, double left, double width, double height)
        {
            Button Wall = wall_Build();
            theWall.Add(Wall);
            Canvas.SetTop(Wall, Board.ActualHeight / top);
            Canvas.SetLeft(Wall, (int)(Board.ActualWidth / left));
            Wall.Width = (int)(Board.ActualWidth / width);
            Wall.Height = (int) (Board.ActualHeight / height);
        }
        
        /// <summary>
        /// Takes top first and then left followed by list
        /// </summary>
        /// <param name="top"> Sets the top function of the path-wall</param>
        /// <param name="left"> Sets the left function of the path-wall</param>
        /// <param name="xs"> Update the list to contain the paths-walls</param>
        /// 

        public void path_Layout(double top, double left, List<Button> xs)
        {
            Button path = wall_Build();
            path.Background = null;
            Canvas.SetTop(path, Board.ActualHeight / top);
            Canvas.SetLeft(path, Board.ActualWidth / left);
            path.Width = (int)Board.ActualWidth / 25;
            path.Height = (int)Board.ActualHeight / 25;
            xs.Add(path);
        }

        void fakeWall_Layout(double width, double height, double top, double left)
        {
            Button f = wall_Build();
            f.Width = Board.ActualWidth / width;
            f.Height = Board.ActualHeight / height;
            Canvas.SetTop(f, Board.ActualHeight / top);
            Canvas.SetLeft(f, (int)(Board.ActualWidth / left));
            f.Background = null;
            theWall2.Add(f);
        }

        void make_fake_Walls()
        {
            fakeWall_Layout(2.5, 2.8, 3.44, 3.3);
            fakeWall_Layout(8, 12, 3.44, 25);
            fakeWall_Layout(8, 12, 3.44, 1.2);
            fakeWall_Layout(8, 20, 8, 15);
            fakeWall_Layout(8, 20, 8, 4);
            fakeWall_Layout(8, 20, 8, 1.65);
            fakeWall_Layout(8, 20, 8, 1.3);
            fakeWall_Layout(8, 14, 1.5, 14);
            fakeWall_Layout(8, 14, 1.5, 4);
            fakeWall_Layout(8, 14, 1.5, 1.65);
            fakeWall_Layout(8, 14, 1.5, 1.3);
            fakeWall_Layout(14, 15, 1.4, 1.31);
            fakeWall_Layout(14, 15, 1.4, 6.1);
            fakeWall_Layout(8, 12, 2.35, 30);
            fakeWall_Layout(8, 12, 2.35, 1.2);
            fakeWall_Layout(3, 14, 1.17, 12);
            fakeWall_Layout(3, 14, 1.17, 1.7);
            fakeWall_Layout(10, 14, 1.2, 2.2);
            fakeWall_Layout(9, 14, 1.56, 2.25);
            fakeWall_Layout(9, 14, 20, 2.25);
            fakeWall_Layout(12, 14, 3.7, 4.1);
            fakeWall_Layout(12, 14, 3.7, 1.5);
            fakeWall_Layout(12, 14, 1.2, 1.5);
            fakeWall_Layout(12, 14, 1.2, 4.1);
            fakeWall_Layout(20, 10.5, 1.335, 50);
            fakeWall_Layout(20, 10.5, 1.335, 1.09);
            fakeWall_Layout(1, 50, 1.05, 50);
        }

        void edge_Walls()
        {
            wall_layout(Board.ActualHeight, Board.ActualWidth, 1, 28); //Top
            wall_layout(1.033, Board.ActualWidth, 1, 28); //Bottom
            wall_layout(Board.ActualHeight, Board.ActualWidth, 28, 2.27); //Left1
            wall_layout(2, Board.ActualWidth, 28, 2); //Left2
            wall_layout(Board.ActualHeight, 1.033, 28, 2.27); //Right1
            wall_layout(2, 1.033, 28, 2); //Right2
        }
       
        void centre_Walls()
        {
            wall_layout(Board.ActualHeight, 2.07, 25, 6.3);
            wall_layout(4.6, 2.62, 4.16, 28);
            wall_layout(4.6, 2.07, 25, 7.8);
            wall_layout(1.68, 2.62, 4.16, 28);
            wall_layout(1.68, 2.07, 25, 7.8);
            wall_layout(1.28, 2.62, 4.16, 28);
            wall_layout(1.28, 2.07, 25, 7.8);
            wall_layout(2.47, 2.63, 14, 28);
            wall_layout(2.47, 2.63, 52, 7.78);
            wall_layout(2.47, 1.65, 52, 7.78);
            wall_layout(2.47, 1.814, 14, 28);
            wall_layout(1.98, 2.63, 4.15, 36);
        }
        
        void topLeft_Walls()
        {
            wall_layout(11, 10, 9, 15);
            wall_layout(11, 3.65, 7, 15);
            wall_layout(4.6, 10, 9, 25);
            wall_layout(4.6, 3.65, 25, 4.5);
            wall_layout(3.25, 3.65, 7, 25);
            wall_layout(3.20, Board.ActualWidth, 4.8, 7.8);
        }
        
        void bottomLeft_Walls()
        {
            wall_layout(2, Board.ActualWidth, 4.8, 7.8);
            wall_layout(1.46, 10, 9, 25);
            wall_layout(1.46, 5.78, 27, 7.6);
            wall_layout(1.285, Board.ActualWidth, 9.5, 25);
            wall_layout(1.145, 9.7, 3.2, 25);
            wall_layout(1.28, 3.65, 25, 7.8);
            wall_layout(2, 3.65, 25, 7.8);
            wall_layout(1.465, 3.65, 7, 25);
        }
        
        void topRight_Walls()
        {
            wall_layout(11, 1.265, 9, 15);
            wall_layout(11, 1.71, 7, 15);
            wall_layout(4.6, 1.265, 9, 25);
            wall_layout(4.6, 1.453, 25, 4.5);
            wall_layout(3.25, 1.708, 7, 25);
            wall_layout(3.20, 1.26, 4.8, 7.8);
        }
        
        void bottomRight_Walls()
        {
            wall_layout(2, 1.26, 4.8, 7.8);
            wall_layout(1.46, 1.265, 9, 25);
            wall_layout(1.46, 1.262, 27, 7.6);
            wall_layout(1.285, 1.114, 9.5, 25);
            wall_layout(1.145, 1.705, 3.2, 25);
            wall_layout(1.28, 1.45, 25, 7.8);
            wall_layout(2, 1.45, 25, 7.8);
            wall_layout(1.465, 1.708, 7, 25);
        }
        
        public void walls_Build_Up()
        {
            edge_Walls();
            centre_Walls();
            topLeft_Walls();
            bottomLeft_Walls();
            topRight_Walls();
            bottomRight_Walls();
            make_fake_Walls();
        }

    }
}

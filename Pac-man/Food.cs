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
    class Food:Window
    {
        Canvas Board;
        public List<Ellipse> theFood;
        private List<Ellipse> vert_Food;
        private List<List<Ellipse>> horr_food;
        private List<Button> theWall;
        private List<Button> theWall2;
        private Walls w;

        public Food(Canvas Board, Walls wall)
        {
            theWall2 = wall.theWall2;
            this.Board = Board;
            w = wall;
            theWall = wall.theWall;
            theFood = new List<Ellipse>();
            vert_Food = new List<Ellipse>();
            horr_food = new List<List<Ellipse>>();
        }

        public double top2(Button r)
        {
            return Canvas.GetTop(r);
        }
        double top3(Ellipse r)
        {
            return Canvas.GetTop(r);
        }
        
        public double bottom2(Button r)
        {
            return (Canvas.GetTop(r) + r.ActualHeight);
        }
        double bottom3(Ellipse r)
        {
            return (Canvas.GetTop(r) + r.ActualHeight);
        }

        public double left2(Button r)
        {
            return Canvas.GetLeft(r);
        }
        double left3(Ellipse r)
        {
            return Canvas.GetLeft(r);
        }
        
        public double right2(Button r)
        {
            return (Canvas.GetLeft(r) + r.ActualWidth);
        }
        double right3(Ellipse r)
        {
            return (Canvas.GetLeft(r) + r.ActualWidth);
        }
        
        void food_dimensions1(Ellipse food)
        {
            food.Width = (int)Board.ActualWidth / 75;
            food.Height = (int)Board.ActualHeight / 70;
        }
        void food_dimensions2(Ellipse food)
        {
            food.Width = (int)(Board.ActualWidth / 50);
            food.Height = (int)Board.ActualHeight / 40;
        }

        Ellipse food_Creation(int size)
        {
            Ellipse food = new Ellipse();
            food.Fill = Brushes.Yellow;
            if (size == 0) food_dimensions1(food);
            if (size == 1) food_dimensions2(food);
            Board.Children.Add(food);
            theFood.Add(food);
            return food;
        }
        Ellipse food_Layout(double left, double top, int size)
        {
            Ellipse food = food_Creation(size);
            Canvas.SetLeft(food, left);
            Canvas.SetTop(food, top);
            return food;
        }
        
        double advance_Top(int k)
        {
            switch (k)
            {
                case 1: return 5.5;
                case 2: return 3.6;
                case 3: return 1.535;
                case 4: return 1.345;
                case 5: return 1.19;
                case 6: return 1.07;
                default: return 17;
            }

        }
        void horizontal_Food()
        {
            int k = 0;
            double T = 17;

            while (k < 7)
            {
                List<Ellipse> row = new List<Ellipse>();
                T = advance_Top(k);
                row.Add(food_Layout((int)Board.ActualWidth/20 , (int)(Board.ActualHeight / T), 0));
                bool r = right3(theFood[theFood.Count - 1]) < Board.ActualWidth;
                
                while (r)
                {
                    int left = (int)(right3(theFood[theFood.Count - 1])+ (int)(Board.ActualWidth / 40));
                    int top = (int)(Board.ActualHeight / T);
                    row.Add(food_Layout(left, top, 0));
                    r = right3(theFood[theFood.Count - 1]) < Board.ActualWidth;
                }
                k++;
                horr_food.Add(row);
            }
        }
        
        double advance_Left(int k)
        {
            switch (k)
            {
                case 1: return 6.7;
                case 2: return 4.25;
                case 3: return 3;
                case 4: return 2.3;
                case 5: return 1.79;
                case 6: return 1.525;
                case 7: return 1.325;
                case 8: return 1.188;
                case 9: return 1.062;
                default: return 20;
            }

        }
        void vertical_Food()
        {
            int k = 0;
            double L = 20;
            
            while (k < 10)
            {
                L = advance_Left(k);
                vert_Food.Add(food_Layout((int)(Board.ActualWidth/L), (int)Board.ActualHeight/17, 0));
                bool r = bottom3(theFood[theFood.Count - 1])< Board.ActualHeight;
         
                while (r)
                {
                    int top = (int)(bottom3(theFood[theFood.Count - 1]) + (int)(Board.ActualWidth / 40));
                    int left = (int)(Board.ActualWidth / L);
                    vert_Food.Add(food_Layout(left, top, 0));
                    r = bottom3(theFood[theFood.Count - 1])< Board.ActualHeight;
                }
                k++;
            }
        }
        
        void special_food_create(double left, double top)
        {
            double left1 = Board.ActualWidth / left;
            double top1 = Board.ActualHeight / top;
            food_Layout(left1, top1, 1);
        }
        void special_Food()
        {
            special_food_create(21, 8);
            special_food_create(21, 1.35);
            special_food_create(1.065, 8);
            special_food_create(1.065, 1.35);
            special_food_create(22, 1.085);
            special_food_create(1.067, 1.085);
        }   
          
        public void create_Food()
        {
            horizontal_Food();
            vertical_Food();
            special_Food();
            overlapping_food_fix();
        }

        void myFood_fixer1()
        {

            for (int k = 0; k < theFood.Count; k++)
            {
                for (int i = 0; i < theWall.Count; i++)
                {
                    if ((top3(theFood[k]) > top2(theWall[i]) && bottom3(theFood[k]) < bottom2(theWall[i])))
                    {
                        if ((left3(theFood[k]) > left2(theWall[i]) && right3(theFood[k]) < right2(theWall[i])))
                        {
                            Board.Children.Remove(theFood[k]);
                            theFood.RemoveAt(k);
                            k--;
                        }
                    }
                }
                for (int i = 0; i < theWall2.Count; i++)
                {
                    if ((top3(theFood[k]) > top2(theWall2[i]) && bottom3(theFood[k]) < bottom2(theWall2[i])))
                    {
                        if ((left3(theFood[k]) > left2(theWall2[i]) && right3(theFood[k]) < right2(theWall2[i])))
                        {
                            Board.Children.Remove(theFood[k]);
                            theFood.RemoveAt(k);
                            k--;
                        }
                    }
                }

            }
        }
        void myFood_fixer2()
        {

            for (int k = 0; k < theFood.Count; k++)
            {
                for (int i = 0; i < theWall.Count; i++)
                {
                    if (top3(theFood[k]) > top2(theWall[i]) && bottom3(theFood[k]) > bottom2(theWall[i]) && Math.Abs(bottom3(theFood[k]) - bottom2(theWall[i])) < theFood[k].Height)
                    {
                        if ((left3(theFood[k]) > left2(theWall[i]) && right3(theFood[k]) < right2(theWall[i])))
                        {
                            Board.Children.Remove(theFood[k]);
                            theFood.RemoveAt(k);
                            k--;
                        }
                    }
                }
                for (int i = 0; i < theWall2.Count; i++)
                {
                    if (top3(theFood[k]) > top2(theWall2[i]) && bottom3(theFood[k]) > bottom2(theWall2[i]) && Math.Abs(bottom3(theFood[k]) - bottom2(theWall2[i])) < theFood[k].Height && theFood[k].ActualWidth!=(int)(Board.ActualWidth/50))
                    {
                        if ((left3(theFood[k]) > left2(theWall2[i]) && right3(theFood[k]) < right2(theWall2[i])))
                        {
                            Board.Children.Remove(theFood[k]);
                            theFood.RemoveAt(k);
                            k--;
                        }
                    }
                }
            }
        }
        void myFood_fixer3()
        {
            for (int k = 0; k < theFood.Count; k++)
            {
                for (int i = 0; i < theWall.Count; i++)
                {
                    if (top3(theFood[k]) < top2(theWall[i]) && bottom3(theFood[k]) < bottom2(theWall[i]) && Math.Abs(top3(theFood[k]) - top2(theWall[i])) < theFood[k].Height)
                    {
                        if ((left3(theFood[k]) > left2(theWall[i]) && right3(theFood[k]) < right2(theWall[i])))
                        {
                            Board.Children.Remove(theFood[k]);
                            theFood.RemoveAt(k);
                            k--;
                        }
                    }
                }
                for (int i = 0; i < theWall2.Count; i++)
                {
                    if (top3(theFood[k]) < top2(theWall2[i]) && bottom3(theFood[k]) < bottom2(theWall2[i]) && Math.Abs(top3(theFood[k]) - top2(theWall2[i])) < theFood[k].Height && theFood[k].ActualWidth != (int)(Board.ActualWidth / 50))
                    {
                        if ((left3(theFood[k]) > left2(theWall2[i]) && right3(theFood[k]) < right2(theWall2[i])))
                        {
                            Board.Children.Remove(theFood[k]);
                            theFood.RemoveAt(k);
                            k--;
                        }
                    }
                }
            }
        }
        void myFood_fixer4()
        {
            for (int k = 0; k < theFood.Count; k++)
            {
                for (int i = 0; i < theWall.Count; i++)
                {
                    if ((top3(theFood[k]) > top2(theWall[i]) && bottom3(theFood[k]) < bottom2(theWall[i])))
                    {
                        if (left3(theFood[k]) > left2(theWall[i]) && right3(theFood[k]) > right2(theWall[i]) && Math.Abs(right3(theFood[k]) - right2(theWall[i])) < theFood[k].ActualWidth)
                        {
                            Board.Children.Remove(theFood[k]);
                            theFood.RemoveAt(k);
                            k--;
                        }
                    }
                }
                for (int i = 0; i < theWall2.Count; i++)
                {
                    if ((top3(theFood[k]) > top2(theWall2[i]) && bottom3(theFood[k]) < bottom2(theWall2[i])))
                    {
                        if (left3(theFood[k]) > left2(theWall2[i]) && right3(theFood[k]) > right2(theWall2[i]) && Math.Abs(right3(theFood[k]) - right2(theWall2[i])) < theFood[k].ActualWidth)
                        {
                            Board.Children.Remove(theFood[k]);
                            theFood.RemoveAt(k);
                            k--;
                        }
                    }
                }
            }
        }
        void myFood_fixer5()
        {
            for (int k = 0; k < theFood.Count; k++)
            {
                for (int i = 0; i < theWall.Count; i++)
                {
                    if ((top3(theFood[k]) > top2(theWall[i]) && bottom3(theFood[k]) < bottom2(theWall[i])))
                    {
                        if (left3(theFood[k]) < left2(theWall[i]) && right3(theFood[k]) < right2(theWall[i]) && Math.Abs(left3(theFood[k]) - left2(theWall[i])) < theFood[k].ActualWidth)
                        {
                            Board.Children.Remove(theFood[k]);
                            theFood.RemoveAt(k);
                            k--;
                        }
                    }
                }
                for (int i = 0; i < theWall2.Count; i++)
                {
                    if ((top3(theFood[k]) > top2(theWall2[i]) && bottom3(theFood[k]) < bottom2(theWall2[i])))
                    {
                        if (left3(theFood[k]) < left2(theWall2[i]) && right3(theFood[k]) < right2(theWall[i]) && Math.Abs(left3(theFood[k]) - left2(theWall2[i])) < theFood[k].ActualWidth)
                        {
                            Board.Children.Remove(theFood[k]);
                            theFood.RemoveAt(k);
                            k--;
                        }
                    }
                }
            }
        }
        void myFood_fixer6()
        {
            for (int i = 1; i < vert_Food.Count; i++)
            {
                if (top3(vert_Food[i]) == top3(vert_Food[0]))
                {
                    Board.Children.Remove(vert_Food[i]);
                    vert_Food.RemoveAt(i);
                    i--;
                }
            }
        }
        void myFood_fixer7()
        {
            for (int i = 0; i < horr_food.Count; i++)
            {
                for (int k = 0; k < horr_food[i].Count; k++)
                {
                    if (k == 0)
                    {
                        Board.Children.Remove(horr_food[i][k]);
                        horr_food[i].RemoveAt(k);
                    }
                    else break;
                }
            }
        }
        void overlapping_food_fix()
        {
            for (int i = 0; i < theFood.Count; i++)
            {
                if (Canvas.GetLeft(theFood[i]) + theFood[i].ActualWidth > Board.ActualWidth - Board.ActualWidth / 20)
                {
                    Board.Children.Remove(theFood[i]);
                    theFood.Remove(theFood[i]);
                    i--;
                }
                if (Canvas.GetTop(theFood[i]) + theFood[i].ActualHeight > Board.ActualHeight - Board.ActualWidth / 40)
                {
                    Board.Children.Remove(theFood[i]);
                    theFood.Remove(theFood[i]);
                    i--;
                }
            }

        }
        
        public void food_fix()
        {
            myFood_fixer1();
            myFood_fixer2();
            myFood_fixer3();
            myFood_fixer4();
            myFood_fixer5();
            myFood_fixer6();
            myFood_fixer7();
        }

        public void food_cleanUp()
        {
            for (int i = 0; i < theFood.Count; i++)
            {
                Board.Children.Remove(theFood[i]);
            }
            for(int i=0;i<theWall2.Count;i++)
            {
                Board.Children.Remove(theWall2[i]);
            }
            theFood.Clear();
            horr_food.Clear();
            vert_Food.Clear();
            theWall2.Clear();
        }

        public void fff()
        {
            MessageBox.Show(string.Format("theWall 2 count : {0}", theWall2.Count));
        }
    }
}

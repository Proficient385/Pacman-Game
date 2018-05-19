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
    class Constraints:Window
    {
        Canvas board;
        Image p;
        Player pMan;
        List<Button> walls;
        List<Ellipse> food;

        public bool isSpecial { get; set; }
        public bool isTopWall { get; set; }
        public bool isBotWall { get; set; }
        public bool isLeftWall { get; set; }
        public bool isRightWall { get; set; }

        public int Score { get; set; }
        public string SCORE { get; set; }

        public Constraints(Canvas board, Player pMan, List<Button> walls, List<Ellipse> food)
        {
            this.board = board;
            this.pMan = pMan;
            p = pMan.p_man;
            this.walls = walls;
            this.food = food;

            isSpecial = false;
            isTopWall = false;
            isBotWall = false;
            isLeftWall = false;
            isRightWall = false;

            Score = 0;
            SCORE = "000000000";
        }

        public double top(Image r)
        {
            return Canvas.GetTop(r);
        }
        public double top2(Button r)
        {
            return Canvas.GetTop(r);
        }
        double top3(Ellipse r)
        {
            return Canvas.GetTop(r);
        }

        public double bottom(Image r)
        {
            return (Canvas.GetTop(r) + r.ActualHeight);
        }
        public double bottom2(Button r)
        {
            return (Canvas.GetTop(r) + r.ActualHeight);
        }
        double bottom3(Ellipse r)
        {
            return (Canvas.GetTop(r) + r.ActualHeight);
        }

        public double left(Image r)
        {
            return Canvas.GetLeft(r);
        }
        public double left2(Button r)
        {
            return Canvas.GetLeft(r);
        }
        double left3(Ellipse r)
        {
            return Canvas.GetLeft(r);
        }

        public double right(Image r)
        {
            return (Canvas.GetLeft(r) + r.ActualWidth);
        }
        public double right2(Button r)
        {
            return (Canvas.GetLeft(r) + r.ActualWidth);
        }
        double right3(Ellipse r)
        {
            return (Canvas.GetLeft(r) + r.ActualWidth);
        }

        public bool Is_Right_Wall_Hit(Image sprite)
        {
            for (int i=0;i<walls.Count; i++)
            {
                if(top(sprite)>=top2(walls[i]) && bottom(sprite)<=bottom2(walls[i]))
                {
                    if ( right(sprite)>=left2(walls[i])-10 && right(sprite) < right2(walls[i]))
                    {
                        isRightWall = true;
                        return true;
                    }
                }
                if (Math.Abs(top(sprite) - top2(walls[i])) < sprite.ActualHeight || Math.Abs(bottom(sprite) - bottom2(walls[i])) < sprite.ActualHeight)
                {
                    if (right(sprite)>=left2(walls[i])-10 && right(sprite) < right2(walls[i]))
                    {
                        if (Math.Abs(top(sprite) - bottom2(walls[i])) < 10) isTopWall = true;
                        if (Math.Abs(bottom(sprite) - top2(walls[i])) < 10) isBotWall = true;
                        if (Math.Abs(right(sprite) - left2(walls[i])) < 10) isRightWall = true;
                        if (Math.Abs(left(sprite) - right2(walls[i])) < 10) isLeftWall = true;
                        return true;
                    }
                    
                }
            }
            return false;
        }

        public bool Is_Left_Wall_Hit(Image sprite)
        {
            for (int i = 0; i < walls.Count; i++)
            {
                if (top(sprite) >= top2(walls[i]) && bottom(sprite) <= bottom2(walls[i]))
                {
                    if (left(sprite) <= right2(walls[i]) && right(sprite) > left2(walls[i]))
                    {
                        isLeftWall = true;
                        return true;
                    }
                }
                if (Math.Abs(top(sprite) - top2(walls[i])) < p.ActualHeight || Math.Abs(bottom(sprite) - bottom2(walls[i])) < p.ActualHeight)
                {
                    if (left(sprite) <= right2(walls[i]) && left(sprite) > left2(walls[i]))
                    {
                        if (Math.Abs(top(sprite) - bottom2(walls[i])) < 10) isTopWall = true;
                        if (Math.Abs(bottom(sprite) - top2(walls[i])) < 10) isBotWall = true;
                        if (Math.Abs(right(sprite) - left2(walls[i])) < 10) isRightWall = true;
                        if (Math.Abs(left(sprite) - right2(walls[i])) < 10) isLeftWall = true;
                        return true;
                    }
                }
            }
            return false;
        }
        
        public string update_Score()
        {
            if (isSpecial) Score += 80;
            string t = "00000000" + "" + Score;
            string t1 = "" + Score;
            SCORE = t.Substring(t1.Length - 1);
            return SCORE;
        }

        private void food_eaten(int i)
        {
            int special_food = (int)(board.ActualWidth / 50);
            isSpecial = (food[i].Width == special_food ? true : false);
            board.Children.Remove(food[i]);
            food.Remove(food[i]);
            Score += 20;
            update_Score();
        }
        public bool Is_Food_Eaten()
        {
            // Ghost is food :)
            if (pMan.pacman_eat_ghost)
            {
                Score += 500;
                pMan.pacman_eat_ghost = false;
                update_Score();
            }
            // Regular food
            for (int i=0;i<food.Count;i++)
            {
                if(pMan.direction==1 && (right(p) >= left3(food[i]) && left(p)<=right3(food[i])))
                {
                    if(top(p)<=top3(food[i]) && bottom(p)>= bottom3(food[i]))
                    {
                        food_eaten(i);
                        i--;
                    }
                }
                else if (pMan.direction==3 && (left(p)<=right3(food[i])-10 && right(p)>=left3(food[i])))
                {
                    if (top(p) <= top3(food[i]) && bottom(p) >= bottom3(food[i]))
                    {
                        food_eaten(i);
                        i--;
                    }
                }
                else if (pMan.direction==0 && (top(p)<=bottom3(food[i]) && bottom(p)>= top3(food[i])))
                {
                    if(left(p)<=left3(food[i])-5 && right(p)>=right3(food[i])-5)
                    {
                        food_eaten(i);
                        i--;
                    }
                }
                else if (pMan.direction == 2 && (bottom(p) >= top3(food[i]) && top(p) <= bottom3(food[i])))
                {
                    if (left(p) <= left3(food[i])-5 && right(p) >= right3(food[i])-5)
                    {
                        food_eaten(i);
                        i--;
                    }
                }
            }
            //if (isSpecial) MessageBox.Show("OK");
            return isSpecial;
        }
    }
}

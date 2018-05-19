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

namespace Pac_man
{
    class Ghost_Controller
    {
        public DispatcherTimer stopwatch { get; set; }
        Dispatcher h;
        public Timer pink_Speed { get; set; }
        public Timer ghosts { get; set; }
        Ghost_Blinky Blinky;
        public int inky_seconds { get; set; }
        public int pinky_seconds { get; set; }
        public int blinky_seconds { get; set; }
        public int food_Seconds { get; set; }
        public int dead_pacman_seconds { get; set; }
        public int score_500_seconds { get; set; }
        public int game_Over_seconds { get; set; }
        public Ghost_Controller(Ghost_Blinky Blinky, DispatcherTimer stopwatch, Timer pink_Speed, Timer ghosts)
        {
            inky_seconds = 0;
            pinky_seconds = 0;
            blinky_seconds = 0;
            food_Seconds = 0;
            dead_pacman_seconds = -1;
            score_500_seconds = -1;
            game_Over_seconds = -1;
            this.Blinky = Blinky;

            this.ghosts = ghosts;
            this.pink_Speed = pink_Speed;
            this.stopwatch = stopwatch;
            Timers_Properties();
        }

        void Timers_Properties()
        {
            stopwatch.IsEnabled = false;
            stopwatch.Interval = TimeSpan.FromSeconds(1);
            stopwatch.Tick += Stopwatch_Tick;

            h = Dispatcher.CurrentDispatcher;
            pink_Speed.Elapsed += new ElapsedEventHandler(pink_Speed_Elapsed);
            pink_Speed.Interval = 80;
            pink_Speed.Enabled = false;

            ghosts.Elapsed += new ElapsedEventHandler(ghosts_Elapsed);
            ghosts.Interval = 90;
            ghosts.Enabled = false;
        }

        private void ghosts_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                h.Invoke(new Action(() =>
                {
                    Blinky.clyde_Advance();
                    if (inky_seconds == 5) Blinky.Inky_Advance();
                    if (blinky_seconds == 5) Blinky.Blinky_Advance();

                    if (Blinky.control.special_Food || Blinky.constraints.Is_Food_Eaten())
                    {
                        Blinky.special_Food_Eaten();
                        food_Seconds = 20;
                        Blinky.control.special_Food = false;
                        ghosts.Interval = 160;
                        pink_Speed.Interval = 160;
                    }

                    if (Blinky.ghost_Hit_Pacman() && food_Seconds==0)
                    {
                        Blinky.control.timer2.Enabled = false;
                        ghosts.Enabled = false;
                        pink_Speed.Enabled = false;
                        dead_pacman_seconds = 4;
                    }
                    if(Blinky.ghost_Hit_Pacman() && food_Seconds>0)
                    {
                        for (int i = 0; i < Blinky.which_ghosts_eaten.Count; i++)
                        {
                            Blinky.Gs[Blinky.which_ghosts_eaten[i]].Visibility = Visibility.Hidden;
                        }
                        if (Blinky.which_ghosts_eaten.IndexOf(0) != -1) Blinky.inky_starting_Pos();
                        if (Blinky.which_ghosts_eaten.IndexOf(1) != -1) Blinky.pinky_starting_Pos();
                        if (Blinky.which_ghosts_eaten.IndexOf(2) != -1) Blinky.blinky_starting_Pos();
                        if (Blinky.which_ghosts_eaten.IndexOf(3) != -1) Blinky.clyde_starting_Pos();
                    }
                    if(Blinky.ghost_Is_Eaten)
                    {
                        score_500_seconds = 4;
                    }
                    if(Blinky.control.P.pacman_lives==0)
                    {
                        game_Over_seconds = 5;
                        Blinky.control.k = 0;
                        Blinky.control.P.pacman_lives = -3;
                    }
                }));
            }
            catch(Exception)
            {
                return;
            }
        }

        private void pink_Speed_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                h.Invoke(new Action(() =>
                {
                    if (pinky_seconds == 5) Blinky.Pinky_Advance();
                }));
            }
            catch(Exception)
            {
                return;
            }
            
        }

        private void Stopwatch_Tick(object sender, EventArgs e)
        {
            //Introducing ghosts
            if(inky_seconds<5)inky_seconds++;
            if (inky_seconds == 5 && pinky_seconds < 5) pinky_seconds++;
            if (pinky_seconds == 5 && blinky_seconds < 5) blinky_seconds++;
            
            //Special food eaten i.e food that turn ghosts into pacman's food
            if (food_Seconds > 0) food_Seconds--;
            if (food_Seconds > 10) Blinky.special_Food_Eaten();
            if (food_Seconds <= 10 && food_Seconds % 2 == 0) Blinky.reset_ghosts();
            if (food_Seconds < 10 && food_Seconds % 2 == 1) Blinky.special_Food_Eaten();
            if (food_Seconds == 0)
            {
                Blinky.reset_ghosts();
                ghosts.Interval = 90;
                pink_Speed.Interval = 80;

                for (int i=0; i< Blinky.which_ghosts_eaten.Count;i++)
                {
                    Blinky.Gs[Blinky.which_ghosts_eaten[i]].Visibility = Visibility.Visible;
                }
                if (Blinky.which_ghosts_eaten.IndexOf(0)!=-1) Blinky.inky_starting_Pos();
                if (Blinky.which_ghosts_eaten.IndexOf(1) != -1) Blinky.pinky_starting_Pos();
                if (Blinky.which_ghosts_eaten.IndexOf(2) != -1) Blinky.blinky_starting_Pos();
                if (Blinky.which_ghosts_eaten.IndexOf(3) != -1) Blinky.clyde_starting_Pos();
                Blinky.which_ghosts_eaten.Clear();
            }

            //Pacman hit by ghost--restarting if lives remaining
            if (dead_pacman_seconds > 0)
            {
                Blinky.control.P.pacman_dead();
                Blinky.control.timer2.Enabled = false;
                dead_pacman_seconds--;
            }
            if (dead_pacman_seconds == 0)
            {
                Blinky.control.P.pMan_starting_Pos();
                Blinky.control.timer2.Enabled = true;
                Blinky.control.P.pacman_lives--;
                ghosts.Enabled = true;
                pink_Speed.Enabled = true;
                dead_pacman_seconds = -1;

            }
            
            if (score_500_seconds > 0) score_500_seconds--; //score 500 label countdown
            if (game_Over_seconds > 0) game_Over_seconds--; //game_Over label countdown   
        }
        
    }
}

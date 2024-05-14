using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TheCMDgame
{
    class Program
    {
        public static void Main()
        {
            //caricamento del gioco
            Game.Setup();

            //menù del gioco (tolto per debug veloce)
            //Game.Menu();

            //selezione del capitolo
            switch (Game.Capitolo)
            {
                case 1:
                    Scenari.Intro();
                    break;

                case 2:
                    Scenari.PrimiPassi1();
                    break;

                case 3:
                    Scenari.PrimiPassi2();
                    break;

                case 4:
                    Scenari.IlRisveglio();
                    break;

                case 50:
                    Scenari.CMDtest();
                    break;

                default:
                    Game.Capitolo = 1;
                    Scenari.Intro();
                    break;
            }
        }
    }
}

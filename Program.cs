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
            Game.Menu();

            //selezione del capitolo
            switch (Game.Capitolo)
            {
                case 1:
                    Scenari.Intro();
                    break;

                case 2:
                    Scenari.PrimoComando();
                    break;

                case 3:
                    Scenari.SecondoComando();
                    break;

                default:
                    Scenari.Intro();
                    break;
            }
        }
    }
}

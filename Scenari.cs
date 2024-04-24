using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TheCMDgame
{
    //classe con tutti gli scenari del gioco, capitoli e scene minori
    static class Scenari
    {
        static private Random rand = new Random();

        static public void Intro()
        {
            Game.StampaFigo("Capitolo 1: Intro", 250);
            Game.Capitolo = 1;
            Game.MSG("Dove sono?", 2000);
            Game.MSG("Cos'è questo posto??");
            Game.MSG("Perché non sento nulla?");
            Game.MSG("E chi sono???");
            Game.MSG("...", 6000);
            Game.MSG("Non ho nessun ricordo...", 6000);
            Game.MSG("Ma questa visione sì..", 4500);
            Game.MSG("Queste scritte..", 4500);
            Game.MSG("E' come se fosse un ricordo ormai impiantato in testa, una cosa che feci ogni giorno.", 4000);
            Game.MSG("Ma ho una testa?", 5000);
            Game.MSG("E ho avuto dei miei giorni?");
            Game.MSG("Questo potrebbe significare che..");
            Game.MSG("Sono morto?", 7000);
            Game.wait(750);
            AnimationCorrupted();
            Game.Capitolo++;
        }

        static public void PrimoComando()
        {
            Game.StampaFigo("Capitolo 2: Primo comando", 250);
            Game.MSG("Cosa è stato???",1000);
            Game.MSG("Non sono abituato a stare qua dentro.");
            Game.MSG("Devo fare qualcosa.");
            Game.MSG("Ma non ricordo nulla.");
            Game.MSG("Niente di niente..", 6000);
            Game.MSG("-h ?");
            Game.MSG("E' un comando...");
            Game.MSG("Proviamo:", 5000);
            while (Game.CMD() != "-h");
            Game.MSG("Aiii la testa", 100);
            Game.MSG("Cosa mi è successo?");
            Game.MSG("Ho sbagliato comando..", 5000);
            Game.MSG("Per questo che mi ha fatto male la testa?", 5000);
            Game.MSG("Provo con -help.");
            Game.MSG("Spero di non sbagliare un'altra volta.", 1000);
            while (Game.CMD() != "-help");
            Game.MSG("Non ha fatto nulla..");
            Game.MSG("E a che mi dovrebbe servire se non fa nulla.");
            Game.MSG("Devo ricordarmi di qualche altro comando.");
            Game.MSG("Se questo qua \"d'aiuto\" non funziona.");
            Game.MSG("Cos'è questo che mi sto ricordando...?", 6000);
            Game.MSG("Mi ricordo di una bimba..", 6000);
            Game.MSG("Si chiamava Carla.", 10000);
            Game.MSG("E una donna di nome...", 6000);
            Game.MSG("Vale..?");
            Game.MSG("Ecco ecco, mi appare proprio ora un'altro comando.",1000);
            Game.wait(4000);
            Game.Capitolo++;
        }

        static public void SecondoComando()
        {
            Game.StampaFigo("Capitolo 3: Secondo comando", 250);
            /*Game.MSG("In questo momento comunque è strano.", 2000);
            Game.MSG("Stanno succedendo delle cose nella mia testa.",2000);
            Game.MSG("Come dei strani flash su dei scenari, alcuni davanti ad un rettangolo colorato..",2000);
            Game.MSG("Altri con degli rumori assordanti..il caos.", 1700);
            Game.MSG("E alcuni con...", 1000);
            Game.MSG("La famiglia?", 6000);
            Game.MSG("vabbè metto questo comando prima che me ne scordo.");*/
            Game.MSG("il comando è: ls");
            while (Game.CMD() != "ls");
            Console.WriteLine("");
            Game.MSG("Cos'è sta roba?",1000);
            Game.MSG("Devo capirci qualcosa..");
            while (Game.CMD() != "-help");
            Game.MSG("Oh, è cambiato qualcosa nel comando help.",0);
            Game.MSG("Mmh...");
            Game.MSG("Protebbero tornarmi utili queste informazioni.");
            while (Game.CMD() != "nano note.txt");
            Game.MSG("Cosa c'è scritto...?");
            while (/*Game.CMD() != "dir \\Log"*/true)
                Game.CMD();
        }

        static private void AnimationCorrupted()
        {
            Console.CursorVisible = false;
            Console.Clear();
            Game.wait(100);
            int alt = Console.WindowHeight;
            int lar = Console.WindowWidth;
            alt = (alt * 93) / 100;
            if (Game.EpiletticMode)
            {
                for (int i = 0; i < 40; i++)
                {
                    for (int j = 0; j < 400; j++)
                    {
                        Console.SetCursorPosition(rand.Next(0, lar/2), rand.Next(0, alt/2 + 1));
                        Console.Write((char)rand.Next(33, 256));
                    }
                    Game.wait(100);
                }
            }
            else
            {
                for (int i = 0; i < 30; i++)
                {
                    for (int j = 0; j < 1500; j++)
                    {
                        Console.SetCursorPosition(rand.Next(0, lar), rand.Next(0, alt + 1));
                        Console.Write((char)rand.Next(33, 256));
                    }
                    Game.wait(1);
                }
            }
            Console.CursorVisible = true;
            Console.Clear();
            Game.wait(100);
            if (Game.EpiletticMode)
            {
                Console.SetCursorPosition((lar * 24) / 100, alt / 4);
                Console.Write("STOP");
            }
            else
            {
                Console.SetCursorPosition((lar * 98) / 100, alt / 2);
                Console.Write("STOP");
            }
            Game.wait(2000);
            Console.Clear();
            Game.wait(1000);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            Game.MSG("Dove sono?", 2000);
            Game.MSG("Cos'è questo posto??");
            Game.MSG("Perché non sento nulla?");
            Game.MSG("Perché non vedo?");
            Game.MSG("Perché non sento nulla?");
            Game.MSG("E' come se vivessi un sogno?");
            Game.MSG("E chi sono???");
            Game.MSG("...", 6000);
            Game.MSG("Non ho nessun ricordo...", 6000);
            Game.MSG("Ma...", 3000);
            Game.MSG("Incomincio a ricordare qualcosa...", 3000);
            Game.MSG("Delle scritte...", 4500);
            Game.MSG("E' come se fosse un ricordo vago ma impiantato in testa, una cosa che feci ogni giorno.", 4000);
            Game.MSG("Ma ho una testa?", 5000);
            Game.MSG("E ho avuto dei miei giorni?");
            Game.MSG("Questo potrebbe significare che..");
            Game.MSG("Sono morto?", 7000);
            Game.wait(750);
            AnimationCorrupted();
            Game.Capitolo++;
        }

        static public void PrimiPassi1()
        {
            Game.StampaFigo("Capitolo 2: primi passi (1/2)", 250);
            Game.MSG("Cosa è stato???",1000);
            Game.MSG("Non Conosco questo ambiente..");
            Game.MSG("Devo fare qualcosa.");
            Game.MSG("Ma non ricordo nulla.");
            Game.MSG("Niente di niente..", 6000);
            Game.MSG("-h ?");
            Game.MSG("Cos'è?");
            Game.MSG("E' un comando?");
            Game.MSG("Proviamo:", 5000);
            while (Game.CMD() != "-h");
            Game.MSG("Aiii la testa!", 100);
            Game.MSG("Cosa mi è successo?");
            Game.MSG("Ho sbagliato comando..", 5000);
            Game.MSG("Per questo che mi ha fatto male la testa?", 5000);
            Game.MSG("Pensa",100);
            Game.MSG("Pensa", 100);
            Game.MSG("Pensa....", 100);
            Game.MSG("-help.");
            Game.MSG("Un nuvo comando!");
            Game.MSG("Spero di non sbagliare un'altra volta.", 1000);
            while (Game.CMD() != "-help");
            Game.MSG("Ma da errore..");
            Game.MSG("E a che mi dovrebbe servire se non funziona.");
            Game.MSG("Devo ricordarmi di qualche altro comando.");
            Game.MSG("Se questo qua \"d'aiuto\" non va.");
            Game.MSG("Aspe mi sto ricordando altro...", 6000);
            Game.MSG("Mi ricordo di una bimba..", 6000);
            Game.MSG("Si chiamava Carla.", 10000);
            Game.MSG("E una donna di nome...", 6000);
            Game.MSG("Valen-?");
            Game.MSG("Ecco ecco, mi appare proprio ora un'altro comando.",500);
            Game.wait(4000);
            Game.Capitolo++;
        }

        static public void PrimiPassi2()
        {
            Game.StampaFigo("Capitolo 2: primi passi (2/2)", 250);
            Game.MSG("In questo momento comunque è strano.", 2000);
            Game.MSG("Stanno succedendo delle cose nella mia testa.",2000);
            Game.MSG("Come dei strani flash su dei scenari, alcuni davanti ad un rettangolo colorato..",2000);
            Game.MSG("Altri con degli rumori assordanti..il caos.", 1700);
            Game.MSG("E alcuni con...", 1000);
            Game.MSG("La famiglia?", 6000);
            Game.MSG("vabbè metto questo comando prima che me ne scordo.");
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
            while (Game.CMD() != @"cd \Lavori");
            Game.MSG("Uh...");
            Game.MSG("Sembra essere un altro posto.");
            Game.MSG("Significa che devo scarrozzare un pò in giro.",4500);
            while (Game.CMD() != "nano Incarico di lavoro 1.txt") ;
            Game.MSG("Eccoci qua.");
            Game.MSG("Vediamo un po'...");
            while (Game.CMD() != "-help") ;
            Game.MSG("Addirittura mi è stato dato un comando in più!");
            //src 
            //sfc ricerca file coorotti e criptati
            //tree
            Game.Capitolo++;
        }
        static public void IlRisveglio() // --> WIP
        {

            

            Game.StampaFigo("Capitolo 3: Il Risveglio", 250);
            Game.MSG("Sto cominciando ad abbituarmi.. ",1000);
            Game.MSG("Inzio a capire come muovermi all'interno dei file ",500);


            // scopre il comando ./ con 1° errore non è un file eseguibile poi aggiunge .exe 

            /*!!
            STAMPA:
            apertura e chiusura di diversi file */

            Game.MSG("ok penso di esserci ",500);
            /*
            !!
            STAMPA:
            FILE_CURRICULUM
            !!
            PERMETTE AL PROTAGONISTA DI CAPIRE CHI ERA PER CHI LAVORAVA E CHI LO CERCAVA*/

            Game.MSG("Quindi mi chamavo .... ",2000); 
            Game.MSG("Ero un subordinato alla Virtual Legacy???",1500);
            Game.MSG("Mi occupavo di...", 500);
            
            //!!ERRORE stampa di caraterri corrotti 
            
            Game.MSG("Eccolo! ");
            Game.MSG("Proteggere i dati della sicuruerezza nazionale, file e progetti top secret ");
            Game.MSG("Ma ora che sono morto chi protegge qui dati", 500);

            //!!notifica violazione perimetro di sicurezza della casa

            Game.MSG("Cos'è....");
            Game.MSG("Violazione sicurezza della casa....");
        }

        static public void CMDtest()
        {
            Console.WriteLine("Capitolo 50: debug zone");
            while (true) Game.CMD();
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

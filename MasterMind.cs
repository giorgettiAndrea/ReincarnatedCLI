using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCMDgame
{
    //classe del mitico gioco, per dei vari rompicapi durante il percorso
    class MasterMind
    {
        public readonly int Cifre = 4;
        public readonly int Tentativi = 10;
        private int tentativo = 0;
        readonly private Random rand;
        private Boolean vittoria = false;

        //public int Cifre { get { return cifre; } }
        //public int Tentativi { get { return tentativi; } }
        public int Tentativo { get { return tentativo; } }
        public Boolean Vittoria { get { return vittoria; } }

        public MasterMind(int c, int t)
        {
            if (c>0 && c<=10)
                Cifre = c;
            if (t > 0)
                Tentativi = t;
            rand = new Random();
            Run();
        }

        private void Run()
        {
            Console.WriteLine($"\nISTRUZIONI: codice a {Cifre} cifre\n\n  - ! <-- numero presente alla posizione azzeccata\n\n  - # <-- numero presente ma non in quella posizione\n\n  - x <-- numero non presente\n\n\n>>L'ORDINE DEI SIMBOLI NON COMBACIA CON L'ORDINE DELLE CIFRE<<\n");
            int[] Codice = GeneraCodice();
            for(tentativo = 0; tentativo<Tentativi || !vittoria; tentativo++)
            {
                String input;
                int[] inputC = new int[Cifre];
                do
                {
                    Console.Write($"\ntentativo {tentativo + 1}/{Tentativi}: inserisci il codice: ");
                    input = Game.NoSpace(Console.ReadLine());
                    for (int i=0; i<input.Length; i++)
                    {
                        try
                        {
                            inputC[i] = Convert.ToInt32(input[i].ToString());
                        }
                        catch (Exception) { input = ""; }
                    }
                    if (input.Length != Cifre)
                    {
                        Console.WriteLine("ATTENZIONE: codice errato!");
                        tentativo++;
                    }
                } while (input.Length != Cifre);
                vittoria = ControllaCodice(inputC, Codice, out String ris);
                Console.WriteLine(ris);
            }
            if (Vittoria)
                Console.WriteLine("\n\nhai beccato il codice giusto!");
        }

        //funzione che genera un codice casuale
        private int[] GeneraCodice()
        {
            int[] c = new int[Cifre];
            for(int i=0; i<c.Length; i++)
            {
                int n;
                do
                    n = rand.Next(0, 10);
                while (c.Contains<int>(n));
                c[i] = n;
            }
            return c;
        }

        private Boolean ControllaCodice(int[] cin, int[] vin, out String ris)
        {
            int pr = 0; //numero e posizione beccati
            int pb = 0; //numero presente
            int pn = 0; //numero non presente
            for (int i = 0; i < cin.Length; i++)
            {
                if (cin[i] == vin[i])
                    pr++;
                else if (vin.Contains<int>(cin[i]))
                    pb++;
                else
                    pn++;
            }
            ris = "";
            
            for(int i=0; i<pr; i++)
                ris += "!";
            for (int i=0; i<pb; i++)
                ris += "#";
            for (int i=0; i<pn; i++)
                ris += "x";

            if (pr == Cifre)
                return true;
            return false;
        }
    }
}

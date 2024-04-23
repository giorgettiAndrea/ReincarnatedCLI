using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace TheCMDgame
{
    //la classe del gioco
    static class Game
    {
        //il percorso del gioco
        static private String percorsoGioco
        {
            get
            {
                //Cartella Debug
                string workingDirectory = Environment.CurrentDirectory;
                //Cartella sopra quella del progretto
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
                //entro nel progetto e ritorno il percorso
                return projectDirectory + @"\ReincarnatedCLI";
            }
        }


        //lo scenario in cui deve ancora finire in forma numerale
        static private int Scenario = 1;
        static public int Capitolo
        {
            get { return Scenario; }
            set
            {
                Console.Clear();

                if (value > 0)
                    Scenario = value;
                else
                    return;

                //in base al capitolo verrà modificata la lista di file presenti
                switch (value)
                {
                    case 3:
                        Directory.CreateDirectory($@"{percorsoGioco}\AmbienteDiGioco\Log");
                        File.WriteAllText($@"{percorsoGioco}\AmbienteDiGioco\note.txt", "[ATTENZIONE FILE CORROTTO]\n\n\ncd: òér aprire le diR°§à-");

                        listaCMD.Add("ls: permette di vedere gli elementi presenti nella propria posizione, elementi col simbolo \"\\\" sono cartelle.");
                        listaCMD.Add("nano: permette di aprire file di testo.");
                        listaCMD.Add("exit: esci dal gioco");
                        break;
                }
            }
        }
        //lista di comandi che verranno stampati col comando d'aiuto
        static private List<String> listaCMD = new List<string>();
        //lista di comandi senza descrizione
        static private List<String> listOnlyCMD
        {
            get {
                List<String> ris = new List<String>();
                foreach(String c in listaCMD)
                {
                    String e = "";
                    foreach(char l in c)
                    {
                        if (l == ':')
                            break;
                        e += l;
                    }
                    ris.Add(e);
                }
                if (Capitolo >= 2)
                    ris.Add("-help");
                return ris;
            }
        }
        static private List<String> Files
        {
            get
            {
                String[] getDirs = Directory.GetDirectories($@"{percorsoGioco}\AmbienteDiGioco");
                String[] getFiles = Directory.GetFiles($@"{percorsoGioco}\AmbienteDiGioco");
                String[] files = getDirs.Concat(getFiles).ToArray();

                List<String> ris = new List<String>();
                foreach(String f in files)
                    ris.Add(OnlyName(f));
                return ris;
            }
        }

        //la salute del processo diminuito ad ogni errore nella scrittura dei comandi
        static private int salute = 6;
        //la proprietà "read only" della salute
        static public int Salute { get { return salute; } }

        //modalità che i giocatori con problemi con le luci abbaglianti e molto veloci deve attivare nel menù per poter giocare
        static public Boolean EpiletticMode = false;

        //numero di messaggi
        static private int nmsg = 0;

        //il menù del gioco con pizze rosse e bianche, primi secondi e.. ah no il menù del gioco :)
        static public void Menu()
        {
            //animazione apparizione titolo
            Console.CursorVisible = false;
            //prendo le posizioni
            int alt = Console.WindowHeight;
            int lar = Console.WindowWidth;
            alt = (alt * 93) / 100;
            //setto la posizione e stampo il gran bel titolo
            Console.SetCursorPosition((lar*43)/100, alt / 2);
            StampaFigo("Reincarnated CLI", 400);
            Console.SetCursorPosition(((lar * 46) / 100)+16, (alt * 60) / 100);
            wait(4000);
            //poi stampo il resto
            Console.SetCursorPosition(0, (alt * 60) / 100);
            Console.Write(">>> ");
            wait(1500);
            StampaFigo("-menu", 150);
            Console.WriteLine("Lista dei comandi:\n");
            Console.WriteLine("play: avvia gioco");
            if (EpiletticMode)
                Console.WriteLine("EpMode: switch modalità epilettica (attivata)");
            else
                Console.WriteLine("EpMode: switch modalità epilettica (disattivata)");
            Console.WriteLine("exit: esci dal gioco\n");
            Console.CursorVisible = true;
            String cmd = "";
            do
            {
                Console.Write(">>> ");
                cmd = Console.ReadLine();

                switch (cmd)
                {
                    case "-help":
                        Console.WriteLine("Lista dei comandi:\n");
                        Console.WriteLine("play: avvia gioco");
                        if (EpiletticMode)
                            Console.WriteLine("EpMode: switch modalità epilettica (attivata)");
                        else
                            Console.WriteLine("EpMode: switch modalità epilettica (disattivata)");
                        Console.WriteLine("exit: esci dal gioco\n");
                        break;

                    case "EpMode":
                        EpiletticMode = !EpiletticMode;
                        Console.WriteLine("\nATTENZIONE: il gioco si sta riavviando fra pochi secondi per aggiornare le modifiche");
                        wait(4000);
                        Console.Clear();
                        wait(1000);
                        Menu();
                        return;

                    case "exit":
                        Console.Write("\nChiusura in corso");
                        StampaFigo("...",1000);
                        Close();
                        break;

                    case "":
                        break;

                    default:
                        Console.WriteLine($"\n{cmd}\n/\\E' stata immessa una riga non riconoscibile\n");
                        break;
                }
            } while (cmd != "play");
            Console.Clear();
            Console.CursorVisible = false;
            wait(3000);
        }


        //un metodo che data una stringa la stampa in un modo assai figo
        static public void StampaFigo(String m, int ms)
        {
            for (int i = 0; i < m.Length; i++)
            {
                wait(ms);
                Console.Write(m[i]);
            }
            Console.Write("\n");
        }

        //trasforma una stringa in un tipo di stringa corrotta
        static public String CorruptString(String m)
        {
            String risultato = "";
            Random r = new Random();
            int lv;
            int lm = 4;//limite
            if (salute >= lm)
                lv = (m.Length * (salute * 6)) / 20;
            else
                lv = (m.Length * (lm*6)) / 20;
            lv = (lv * 70) / 100;
            foreach (char c in m)
            {
                int u = r.Next(m.Length);
                if(c == ' ')
                    risultato += c;
                else if (lv < u)
                    risultato += (char)r.Next(33, 256);
                else
                    risultato += c;
            }
            return risultato;
        }

        //stessa funzione di prima ma con la scelta dell'intervallo di tempo
        static public void MSG(String m, int ms = 3000)
        {
            //incremento
            nmsg++;
            //nascondo il cursore
            Console.CursorVisible = false;
            //aspetta un determinato lasso di tempo
            wait(ms);
            //in base alla salute corrompe il messaggio
            m = CorruptString(m);
            //stampa il messaggio
            Console.WriteLine("> " + m);
            //distanzia i messaggi fra loro in caso di giocatori con problemi epilettici
            if (EpiletticMode)
            {
                Console.WriteLine("");
                //controllo per giocatori affetti da epilessia
                ControllaNMSG();
            }
        }

        //un metodo che permette di fermare il processo per un lasso di tempo
        static public void wait(int ms)
        {
            Thread.Sleep(ms);
        }

        static private void ControllaNMSG()
        {
            int alt = Console.WindowHeight;
            if(nmsg >= (alt * 67) / 100)
            {
                Console.WriteLine("\nATTENZIONE: x per condizioni di epilessia i messaggi stanno per cancellarsi");
                wait(7500);
                Console.Clear();
                wait(3000);
            }
        }

        //procedura in cui l'utente dovrà inserire un comando
        static public String CMD(String dir = "")
        {
            //mostro il cursore
            Console.CursorVisible = true;
            //stampa per far capire che è un comando
            Console.Write(">>>{0} ", dir);
            //comando dell'utente
            String cmd = Console.ReadLine();
            //se non c'è fa sapere dell'errore
            if (cmd == "")
            {
                Console.CursorVisible = false;
                return cmd;
            }
            if (!listOnlyCMD.Contains(soloCMD(cmd)))
            {
                Dead();
                Console.WriteLine(CorruptString($"\n{cmd}\n/\\E' stata immessa una riga non riconoscibile, digitare \"-help\" per vedere i comandi\n"));
                salute--;
                Console.CursorVisible = false;
                return cmd;
            }
            switch (soloCMD(cmd))
            {
                case "-help":
                    Console.WriteLine(CorruptString("Lista dei comandi:\n"));
                    for (int i = 0; i < listaCMD.Count; i++)
                        Console.WriteLine(listaCMD[i]);
                    wait(4000);
                    Console.WriteLine(CorruptString("\n404: Errore di caricamento\n"));
                    break;

                case "ls":
                    Console.WriteLine("");
                    foreach (String f in Files)
                        Console.Write(f + " ");
                    Console.WriteLine("");
                    break;

                case "exit":
                    Console.Write(CorruptString("\nChiusura in corso"));
                    StampaFigo("...", 1000);
                    Close();
                    break;

                case "nano":
                    if (!Files.Contains(soloArgs(cmd)))
                        Console.WriteLine($"\n{soloArgs(cmd)}\n/\\\nIl file che si sta cercando di aprire non esiste\n");
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine(File.ReadAllText($@"{percorsoGioco}\AmbienteDiGioco\{soloArgs(cmd)}"));
                        Console.WriteLine("");
                    }
                    break;
            }
            Console.CursorVisible = false;
            return soloCMD(cmd);
        }

        //un metodo privato che dato un percorso restituisce solo il nome
        static private String OnlyName(String path)
        {
            String ris = "";
            for(int i = path.Length-1; path[i] != '\\'; i--)
                ris += path[i];
            ris = Reverse(ris);
            if (!ris.Contains('.'))
                ris = "\\" + ris;
            return ris;
        }
        //metodo privato per escludere i parametri da un comando
        static private String soloCMD(String cmd)
        {
            String ris = "";
            foreach(char c in cmd)
            {
                if (c == ' ')
                    return ris;
                else
                    ris += c;
            }
            return cmd;
        }
        //metodo uguale a prima ma prendendo solo i parametri
        static private String soloArgs(String cmd)
        {
            String ris = "";
            foreach (char c in cmd)
            {
                if (c == ' ' && ris == soloCMD(cmd))
                    ris = "";
                else
                    ris += c;
            }
            return ris;
        }
        //altro metodo privato per rovesciare una stringa
        static private String Reverse(String s)
        {
            String ris = "";
            for (int i = s.Length - 1; i>=0; i--)
                ris += s[i];
            return ris;
        }

        //per chiudere
        static public void Close()
        {
            Environment.Exit(0);
        }



        static private void Dead()
        {
            if(Salute <= 0)
            {
                Console.Write("\n"+":( sono stati riscontrati dei problemi, il sistema sta per avviarsi");
                StampaFigo("...", 2000);
                wait(2000);
                Console.Clear();
                salute = 6;
                listaCMD = new List<string>();
                nmsg = 0;
                wait(4000);
                Program.Main();
                Close();
            }
        }

        //todo --> salvataggi DB con SQL

        //in futuro qua verranno caricati i salvataggi
        static public void Setup()
        {
            Capitolo = 1;
        }
    }
}

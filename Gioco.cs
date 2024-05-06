using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace TheCMDgame
{
    //la struct del comando
    struct Comando
    {
        public String Nome;
        public String Desc;
    }


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

        //funzione che crea un comando in modo compatto
        static private Comando CreaComando(String n, String d)
        {
            Comando c;
            c.Nome = n;
            c.Desc = d;
            return c;
        }
        //funzione che verifica l'esistenza di un comando
        static private bool EsisteComando(String n)
        {
            foreach(Comando c in listaCMD)
            {
                if (c.Nome == n)
                    return true;
            }
            return false;
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
                    Scenario = 0;

                //in base al capitolo verrà modificata la lista di file presenti
                if(value >= 3)
                {
                    listaCMD.Add(CreaComando("ls", "permette di vedere gli elementi presenti nella propria posizione"));
                    listaCMD.Add(CreaComando("nano", "permette di aprire file di testo."));
                    listaCMD.Add(CreaComando("clear","schiarisce la mente"));
                }
                if(value >= 4)
                {
                    listaCMD.Add(CreaComando("cd", "per entrare nelle cartelle, \"..\" per uscire."));
                    listaCMD.Add(CreaComando("ssh", "per usare il teriminale di un altro host"));
                    listaCMD.Add(CreaComando("systeminfo", "info sull'host corrente"));
                    listaCMD.Add(CreaComando("wireshark", "rileva gli host vicini"));
                }

                //salva ogni volta che cambia capitolo
                Save();
            }
        }
        //lista di comandi
        static private List<Comando> listaCMD = new List<Comando>();
        //la località in cui si trova il protagonista, il computer in cui si trova
        static private String LocalHost = "PCR";
        //la lista di file dell'ambiente di gioco
        static private List<String> Files
        {
            get
            {
                String[] getDirs = Directory.GetDirectories($@"{percorsoGioco}\AmbienteDiGioco\{LocalHost}{dirpos}");
                String[] getFiles = Directory.GetFiles($@"{percorsoGioco}\AmbienteDiGioco\{LocalHost}{dirpos}");
                String[] files = getDirs.Concat(getFiles).ToArray();

                List<String> ris = new List<String>();
                foreach(String f in files)
                {
                    if(OnlyName(f)[0] != '[')
                        ris.Add(OnlyName(f));
                }
                return ris;
            }
        }
        //lista di host remoti
        static private List<String> Hosts
        {
            get
            {
                String[] getDirs = Directory.GetDirectories($@"{percorsoGioco}\AmbienteDiGioco");
                List<String> ris = new List<String>();
                foreach (String h in getDirs)
                    ris.Add(OnlyName(h).Substring(1));
                return ris;
            }
        }
        //un metodo privato che dato un percorso restituisce solo il nome
        static private String OnlyName(String path)
        {
            String ris = "";
            for (int i = path.Length - 1; path[i] != '\\'; i--)
                ris += path[i];
            ris = Reverse(ris);
            if (!ris.Contains('.'))
                ris = "\\" + ris;
            return ris;
        }

        //la posizione corrente nell'ambiente di gioco
        static private String dirpos = "";
        static public String DirectoryPos { get { return dirpos; } }

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
            if (dirpos == "")
                Console.WriteLine("> " + m);
            else
                Console.WriteLine($">{dirpos}: " + m);
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
        static public String CMD()
        {
            //mostro il cursore
            Console.CursorVisible = true;
            //stampa per far capire che è un comando
            if(dirpos == "")
                Console.Write($"@{LocalHost}>>> ");
            else
                Console.Write($"@{LocalHost}>>> {dirpos}: ");
            //comando dell'utente
            String cmd = Console.ReadLine();
            cmd = NoSpace(cmd);
            //tutti i controlli neccessari per il comando messo dal giocatore
            if (cmd == "")
            {
                Console.CursorVisible = false;
                return cmd;
            }
            if (!EsisteComando(soloCMD(cmd)))
            {
                if (soloCMD(cmd) == "-help")
                {
                    Console.WriteLine(CorruptString("Lista dei comandi:\n"));
                    for (int i = 0; i < listaCMD.Count; i++)
                        Console.WriteLine(listaCMD[i].Nome + ": " + listaCMD[i].Desc);
                    wait(4000);
                    Console.WriteLine(CorruptString("\n404: Errore di caricamento\n"));
                    if(!EsisteComando("exit"))
                        listaCMD.Add(CreaComando("exit", "esce dal gioco"));
                }
                else
                {
                    Dead();
                    Console.WriteLine(CorruptString($"\n{cmd}\n/\\E' stata immessa una riga non riconoscibile, digitare \"-help\" per vedere i comandi\n"));
                    salute--;
                    Console.CursorVisible = false;
                    return cmd;
                }
            }
            switch (soloCMD(cmd))
            {
                case "ls":
                    Console.WriteLine("");
                    if (Files.Count == 0)
                        Console.WriteLine("[Cartella vuota]");
                    foreach (String f in Files)
                        Console.Write(f + " ");
                    Console.WriteLine("");
                    break;

                case "clear":
                    Console.Clear();
                    break;

                case "exit":
                    Console.Write(CorruptString("\nChiusura in corso"));
                    StampaFigo("...", 1000);
                    Close();
                    break;

                case "nano":
                    if (soloArgs(cmd) == "")
                        Console.WriteLine("\nAndrebbe messo il nome di un file come parametro");
                    else if (!Files.Contains(soloArgs(cmd)))
                        Console.WriteLine($"\n{soloArgs(cmd)}\n/\\\nIl file che si sta cercando di aprire non esiste\n");
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine(File.ReadAllText($@"{percorsoGioco}\AmbienteDiGioco\{LocalHost}{dirpos}\{soloArgs(cmd)}"));
                        Console.WriteLine("");
                        if (cmd == "nano note.txt" && !EsisteComando("cd"))
                            listaCMD.Add(CreaComando("cd","per entrare nelle cartelle, \"..\" per uscire."));
                        if (cmd == "nano Incarico di lavoro 1.txt" && !EsisteComando("ssh"))
                        {
                            listaCMD.Add(CreaComando("ssh", "per usare il teriminale di un altro host"));
                            listaCMD.Add(CreaComando("systeminfo", "info sull'host corrente"));
                            listaCMD.Add(CreaComando("wireshark", "rileva gli host vicini"));
                        }
                    }
                    break;

                case "cd":
                    if (soloArgs(cmd) == "")
                        Console.WriteLine("\nAndrebbe messo il nome di una cartella come parametro");
                    else if (soloArgs(cmd) == ".." && dirpos != "")
                    {
                        int index = 0;
                        for (int i = dirpos.Length-1; i >= 0; i--)
                        {
                            if (dirpos[i] == '\\')
                            {
                                index = i;
                                i = -1;
                            }
                        }
                        string ris = "";
                        for (int i = 0; i < index; i++)
                            ris += dirpos[i];
                        dirpos = ris;
                    }
                    else if (!Files.Contains(soloArgs(cmd)))
                        Console.WriteLine($"\n{soloArgs(cmd)}\n/\\\nLa cartella in cui si sta cercando di entrare non esiste\n");
                    else
                        dirpos += soloArgs(cmd);
                    break;

                case "systeminfo":
                    Console.WriteLine("");
                    Console.WriteLine(File.ReadAllText($@"{percorsoGioco}\AmbienteDiGioco\{LocalHost}\[systeminfo].txt"));
                    Console.WriteLine("");
                    break;

                case "ssh":
                    if (!Hosts.Contains(soloArgs(cmd)))
                    {
                        wait(5000);
                        Console.WriteLine("\nERRORE: host non trovato\n");
                    }
                    else
                    {
                        Console.WriteLine("Connessione...");
                        Random r = new Random();
                        wait(r.Next(500,6000));
                        Console.WriteLine("Connessione stabilita, trasferimento in corso");
                        wait(4000);
                        Console.Clear();
                        LocalHost = soloArgs(cmd);
                    }
                    break;

                case "wireshark":
                    int pos = Hosts.IndexOf(LocalHost);
                    int ping = Convert.ToInt32(File.ReadAllText($@"{percorsoGioco}\AmbienteDiGioco\{LocalHost}\[ping].txt"));
                    Console.WriteLine("Hosts: \n");
                    for(int i = 0; i < Hosts.Count; i++)
                    {
                        if (i >= pos - ping && i <= pos + ping && i != pos)
                        {
                            try
                            {
                                Console.WriteLine($"{Hosts[i]}\n");
                            }
                            catch (IndexOutOfRangeException) {; ; ; ;}
                        }
                    }
                    break;
            }
            Console.CursorVisible = false;
            return cmd;
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
        //data una stringa toglie gli spazi attorno
        static private String NoSpace(String s)
        {
            string ris = "";
            bool a = false;
            for(int i = 0; i < s.Length; i++)
            {
                if (s[i] != ' ')
                    a = true;
                if (a)
                    ris += s[i];
            }
            s = ris;
            ris = "";
            a = false;
            for (int i = s.Length-1; i >= 0; i--)
            {
                if (s[i] != ' ')
                    a = true;
                if (a)
                    ris += s[i];
            }
            return Reverse(ris);
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
                listaCMD = new List<Comando>();
                nmsg = 0;
                wait(4000);
                Program.Main();
                Close();
            }
        }

        //caricamento dei salvataggi
        static public void Setup()
        {
            String[] salvataggio = File.ReadAllLines($@"{percorsoGioco}\Salvataggi\Salvataggio.txt");
            
            try { salute = Convert.ToInt32(salvataggio[1]); }
            catch (FormatException) { salute = 10; }
            
            try { Capitolo = Convert.ToInt32(salvataggio[0]); }
            catch (FormatException) { Capitolo = 1; }
        }

        //salvataggio dei dati
        static public void Save()
        {
            String salvataggio = $"{Capitolo}\n{salute}\n\nordine attributi:\n>capitolo\n>salute";
            File.WriteAllText($@"{percorsoGioco}\Salvataggi\Salvataggio.txt", salvataggio);
        }
    }
}

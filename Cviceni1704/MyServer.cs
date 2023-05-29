using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cviceni1704
{
    internal class MyServer
    {

        private TcpListener myServer;
        private bool isRunning;
        private List<string> jmena = new List<string>();
        private List<string> hesla = new List<string>();
        private bool prihlaseno;
        private int index;
        private Client prihlasenyClient;
        private DateTime datumSpusteni;
        private DateTime datumKonce;
        private int pocetPrihlasenychOsob;
        private int chybnaPrihlaseni;
        private int zadanyPrikazy;
        private List<string> list;

        public List<string> List
        {
            get { return list; }
            set { list = value; }
        }

        public int ZadanyPrikazy
        {
            get { return zadanyPrikazy; }
            set { zadanyPrikazy = value; }
        }

        public int ChybnaPrihlaseni
        {
            get { return chybnaPrihlaseni; }
            set { chybnaPrihlaseni = value; }
        }

        public int PocetPrihlasenychOsob
        {
            get { return pocetPrihlasenychOsob; }
            set { pocetPrihlasenychOsob = value; }
        }

        public Client PrihlasenyClient
        {
            get { return prihlasenyClient; }
            set { prihlasenyClient = value; }
        }

        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        public bool Prihlaseno
        {
            get { return prihlaseno; }
            set { prihlaseno = value; }
        }

        public DateTime DatumSpusteni
        {
            get { return datumSpusteni; }
            set { datumSpusteni = value; }
        }

        public DateTime DatumKonce
        {
            get { return datumKonce; }
            set { datumKonce = value; }
        }

        public MyServer(int port)
        {
            myServer = new TcpListener(System.Net.IPAddress.Any, port);
            myServer.Start();
            isRunning = true;
            Prihlaseno = false;
            Index = 1;
            DatumSpusteni = DateTime.Now;
            PocetPrihlasenychOsob = 0;
            ChybnaPrihlaseni = 0;
            ZadanyPrikazy = 0;
            list = new List<string>();
            ServerLoop();
        }

        private void ServerLoop()
        {
            Console.WriteLine("Server byl spusten");
            
            while (isRunning)
            {
                TcpClient client = myServer.AcceptTcpClient();
                ClientLoop(client);
            }
        }

        private void Login(TcpClient client)
        {
            StreamReader reader = new StreamReader(client.GetStream(), Encoding.UTF8);
            StreamWriter writer = new StreamWriter(client.GetStream(), Encoding.UTF8);


            while (!Prihlaseno)
            {

                writer.Write("Create account(1) / Sign up(2): ");
                writer.Flush();
                int choice = Int32.Parse(reader.ReadLine());
                switch (choice)
                {
                    case 1:
                        writer.Write("Prihlasovaci jmeno: ");
                        writer.Flush();
                        string jmeno = reader.ReadLine();
                        writer.Write("Heslo: ");
                        writer.Flush();
                        string heslo = reader.ReadLine();
                        Client c = new Client(jmeno, heslo);
                        jmena.Add(jmeno);
                        hesla.Add(heslo);
                        TextWriter txt = new StreamWriter("data.txt");
                        txt.WriteLine(c.ToString());
                        break;

                    case 2:
                        while (!Prihlaseno && Index <= 3)
                        {

                            writer.Write("Prihlasovaci jmeno: ");
                            writer.Flush();
                            string prihlasovaciJmeno = reader.ReadLine();
                            writer.Write("Heslo: ");
                            writer.Flush();
                            string prihlasovaciHeslo = reader.ReadLine();
                            Client checkovaciClient = new Client(prihlasovaciJmeno, prihlasovaciHeslo);
                            for (int j = 0; j < jmena.Count; j++)
                            {
                                if (jmena[j].Equals(prihlasovaciJmeno) && hesla[j].Equals(prihlasovaciHeslo))
                                {
                                    Prihlaseno = true;
                                    PrihlasenyClient = new Client(jmena[j], hesla[j]);
                                    PocetPrihlasenychOsob++;
                                    writer.WriteLine("Prihlaseno");
                                    writer.Flush();
                                    break;
                                }
                            }
                            Index++;
                            ChybnaPrihlaseni++;
                        }
                        break;

                }



            }
        }

        private void ClientLoop(TcpClient client)
        {
            StreamReader reader = new StreamReader(client.GetStream(), Encoding.UTF8);
            StreamWriter writer = new StreamWriter(client.GetStream(), Encoding.UTF8);

            writer.WriteLine("Byl jsi pripojen");
            writer.Flush();
            bool clientConnect = true;
            string? data = null;
            string? dataRecive = null;

            while(!Prihlaseno) {

                writer.Write("Create account(1) / Sign up(2): ");
                writer.Flush();
                int choice = Int32.Parse(reader.ReadLine());
                switch (choice)
                {
                    case 1:
                        writer.Write("Prihlasovaci jmeno: ");
                        writer.Flush();
                        string jmeno = reader.ReadLine();
                        writer.Write("Heslo: ");
                        writer.Flush();
                        string heslo = reader.ReadLine();
                        Client c = new Client(jmeno, heslo);
                        jmena.Add(jmeno);
                        hesla.Add(heslo);
                        TextWriter txt = new StreamWriter("data.txt");
                        txt.WriteLine(c.ToString());
                        break;

                    case 2:
                        while (!Prihlaseno && Index <= 3)
                        {

                            writer.Write("Prihlasovaci jmeno: ");
                            writer.Flush();
                            string prihlasovaciJmeno = reader.ReadLine();
                            writer.Write("Heslo: ");
                            writer.Flush();
                            string prihlasovaciHeslo = reader.ReadLine();
                            Client checkovaciClient = new Client(prihlasovaciJmeno, prihlasovaciHeslo);
                            for (int j = 0; j < jmena.Count; j++)
                            {
                                if (jmena[j].Equals(prihlasovaciJmeno) && hesla[j].Equals(prihlasovaciHeslo))
                                {
                                    Prihlaseno = true;
                                    PrihlasenyClient = new Client(jmena[j], hesla[j]);
                                    PocetPrihlasenychOsob++;
                                    writer.WriteLine("Prihlaseno");
                                    writer.Flush();
                                    break;
                                }
                            }
                            Index++;
                            ChybnaPrihlaseni++;
                        }
                        break;

                }

            }


            while (clientConnect)
            {

                data = reader.ReadLine();
                data = data.ToLower();


                if (data == "end")
                {
                    ZadanyPrikazy++;
                    clientConnect = false;
                }
                if (data == "who" && Prihlaseno)
                {
                    ZadanyPrikazy++;
                    writer.WriteLine("Prihlaseny client: " + PrihlasenyClient);
                    writer.Flush();
                }
                if (data == "uptime")
                {
                    ZadanyPrikazy++;
                    DatumKonce = DateTime.Now;

                    writer.WriteLine("Zacatek: " + DatumSpusteni);
                    writer.Flush();
                    writer.WriteLine("Soucasny cas: " + datumKonce);
                    writer.Flush();

                }
                if (data == "stats")
                {
                    ZadanyPrikazy++;
                    writer.WriteLine("Pocet prihlasenych uzivatelu: " + PocetPrihlasenychOsob);
                    writer.Flush();
                    writer.WriteLine("Pocet chbnych prihlaseni: " + ChybnaPrihlaseni);
                    writer.Flush();
                    writer.WriteLine("Pocet zadanych prikazu: " + ZadanyPrikazy);
                    writer.Flush();
                }
                if (data == "last")
                {
                    ZadanyPrikazy++;
                }
                if (data == "exit")
                {
                    ZadanyPrikazy++;
                    Prihlaseno = false;
                    writer.WriteLine("Odhlaseno");
                    writer.Flush();
                    ClientLoop(client);
                }
                if (data == "ohm")
                {
                    writer.WriteLine("Vyberte vzorec: I = U / R (1) || U = I * R (2) || R = U / I (3)");
                    writer.Flush();
                    int vzorec = Int32.Parse(reader.ReadLine());

                    switch (vzorec)
                    {
                        case 1:
                            writer.Write("U = ");
                            writer.Flush();
                            int napeti1 = Int32.Parse(reader.ReadLine());
                            writer.Write("R = ");
                            writer.Flush();
                            int odpor1 = Int32.Parse(reader.ReadLine());
                            int proud1 = napeti1 / odpor1;
                            writer.WriteLine("I = " + napeti1 + " / " + odpor1 + " = " + proud1 + " A");
                            break;

                        case 2:
                            writer.Write("I = ");
                            writer.Flush();
                            int proud2 = Int32.Parse(reader.ReadLine());
                            writer.Write("R = ");
                            writer.Flush();
                            int odpor2 = Int32.Parse(reader.ReadLine());
                            int napeti2 = proud2 * odpor2;
                            writer.WriteLine("U = " + proud2 + " * " + odpor2 + " = " + napeti2 + " V");
                            break;

                        case 3:
                            writer.Write("U = ");
                            writer.Flush();
                            int napeti3 = Int32.Parse(reader.ReadLine());
                            writer.Write("I = ");
                            writer.Flush();
                            int proud3 = Int32.Parse(reader.ReadLine());
                            int odpor3 = napeti3 / proud3;
                            writer.WriteLine("R = " + napeti3 + " / " + proud3 + " = " + odpor3 + " Ohm");
                            break;


                    }

                }

                if(data == "chat")
                {
                    if (Prihlaseno)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            writer.WriteLine(list[i]+", ");
                            writer.Flush();
                        }
                        writer.Write("Pis sem: ");
                        writer.Flush();
                        string slovo = reader.ReadLine();
                        List.Add(slovo);

                    }
                }

                
                writer.WriteLine("-----------------------------------");
                writer.Flush();
            }
            writer.WriteLine("Byl jsi odpojen");
            writer.Flush();
        }
    }
}

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

        public MyServer(int port)
        {
            myServer = new TcpListener(System.Net.IPAddress.Any, port);
            myServer.Start();
            isRunning = true;
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


            writer.WriteLine("Create account(1) / Sign up(2): ");
            int choice = int.Parse(reader.ReadLine());
            switch (choice)
            {
                case 1:
                    writer.WriteLine("Prihlasovaci jmeno: ");
                    string jmeno = reader.ReadLine();
                    writer.WriteLine("Heslo: ");
                    string heslo = reader.ReadLine();
                    Client c = new Client(jmeno, heslo);
                    jmena.Add(jmeno);
                    hesla.Add(heslo);
                    TextWriter txt = new StreamWriter("data.txt");
                    txt.WriteLine(c.ToString());
                    break;

                case 2:
                    for (int i = 0; i < 3; i++)
                    {
                        writer.WriteLine("Prihlasovaci jmeno: ");
                        string prihlasovaciJmeno = reader.ReadLine();
                        writer.WriteLine("Heslo: ");
                        string prihlasovaciHeslo = reader.ReadLine();
                        Client checkovaciClient = new Client(prihlasovaciJmeno, prihlasovaciHeslo);
                        for (int j = 0; j < jmena.Count; j++)
                        {
                            if (jmena[j].Equals(prihlasovaciJmeno) && hesla[j].Equals(prihlasovaciHeslo))
                            {
                                writer.WriteLine("Prihlaseno");
                                writer.Flush();
                            }
                        }
                    }
                    writer.WriteLine("Moc pokusu");
                    writer.Flush();
                    break;

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

            for (int a = 0; a < 100; a++) {

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
                        for (int i = 0; i < 3; i++)
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
                                    writer.WriteLine("Prihlaseno");
                                    writer.Flush();
                                    break;
                                    
                                }
                            }
                            break;
                        }
                        writer.WriteLine("Moc pokusu");
                        writer.Flush();
                        break;

                }

            }


            while (clientConnect)
            {
               
                data = reader.ReadLine();
                data = data.ToLower();
                

                if (data == "end")
                {
                    clientConnect = false;
                }
                if (data == "who")
                {
                    
                }
                if (data == "uptime")
                {

                }
                if (data == "stats")
                {

                }
                if (data == "last")
                {

                }
                if (data == "exit")
                {

                }

                dataRecive = data + " prijato";
                writer.WriteLine(dataRecive);
                writer.Flush();
            }
            writer.WriteLine("Byl jsi odpojen");
            writer.Flush();
        }
    }
}

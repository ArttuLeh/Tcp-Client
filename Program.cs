using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            bool end = false;
            TcpClient client;
            NetworkStream s;
            StreamReader rdr;
            StreamWriter wtr;
            string msg;
            string fname;

            while (!end)
            {
                Console.Write("Give a line: ");
                msg = Console.ReadLine();

                client = new TcpClient();
                client.Connect("localhost", 12345);
                s = client.GetStream();
                rdr = new StreamReader(s);
                wtr = new StreamWriter(s);

                if (msg == "__file__")
                {
                    Console.Write("Give a file name: ");
                    fname = Console.ReadLine();

                    if (!File.Exists(fname))
                    {
                        using (StreamWriter sw = File.CreateText(fname))
                        {
                            sw.WriteLine("Lahden ammattikorkeakoulu (LAMK) sijaitsee Lahdessa.");
                            sw.WriteLine("Saimaan ammattikorkeakoulu (Saimia) sijaitsee Lappeenrannassa.");
                            sw.WriteLine("Nyt Lahden ammattikorkeakoulu (LAMK) sijaitsee sekä Lahdessa että Lappeenrannassa.");
                        }
                    }
                    using (StreamReader file = new StreamReader(fname))
                    {
                        int counter = 0;
                        string ln;
                        while ((ln = file.ReadLine()) != null)
                        {
                            wtr.WriteLine(ln);
                            wtr.Flush();
                            Console.WriteLine($"{rdr.ReadLine()}");
                            Console.WriteLine($"{rdr.ReadLine()}");
                            counter++;
                        }
                        file.Close();
                    }
                }
                else
                {
                    wtr.WriteLine(msg);
                    wtr.Flush();
                    Console.WriteLine($"{rdr.ReadLine()}");
                    Console.WriteLine($"{rdr.ReadLine()}");
                }
                if (msg == "" || msg == "__stop__")
                {
                    end = true;
                    Console.Write($"{rdr.ReadLine()}");
                }

                client.Close();
            }
        }
    }
}



//string text = File.ReadAllText(fname);
//Console.WriteLine(text);
//wtr.WriteLine(text);
//wtr.Flush();
//Console.WriteLine($"{rdr.ReadLine()}");
//Console.WriteLine($"{rdr.ReadLine()}");

//if (File.Exists(fname))
//{
//    string[] lines = File.ReadAllLines(fname);
//    foreach (string line in lines)
//    {
//        wtr.WriteLine(line);
//        wtr.Flush();
//        Console.WriteLine($"{rdr.ReadLine()}");
//        Console.WriteLine($"{rdr.ReadLine()}");
//    }
//}

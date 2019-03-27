using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace Cliente
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient cliente = new TcpClient();
            Console.WriteLine("Conectando al servidor...");
            cliente.Connect("127.0.0.1", 50000);
            Console.WriteLine("Cliente conectado");


            //Escribir el mensaje que se quiere enviar
            Console.Write("Escriba el mensaje a enviar: ");
            string mensaje = Console.ReadLine();

            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(mensaje);
            Stream st = cliente.GetStream();

            st.Write(buffer, 0, buffer.Length);

            st.Flush();
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;

namespace Servidor
{
    class Program
    {
        private static TcpListener servidor;
        private static Thread escucha;
        private static TcpClient client;
        static void Main(string[] args)
        {
            servidor = new TcpListener(IPAddress.Parse("127.0.0.1"), 50000);
            escucha = new Thread(new ThreadStart(clientes));
            escucha.Start();
        }

        public static void clientes()
        {
            Console.WriteLine("Conectando servidor...");
            servidor.Start();
            Console.WriteLine("Servidor conectado. Esperando clientes...");
            int contador = 1;
            while (true)
            {
                client = servidor.AcceptTcpClient();
                Console.WriteLine("Cliente {0} conectado", contador++);
                Thread clienteThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clienteThread.Start(client);
            }
        }

        public static void HandleClientComm(object client)
        {
            TcpClient tcpCliente = (TcpClient)client;
            NetworkStream clientStream = tcpCliente.GetStream();
            byte[] buffer = new byte[4096];

            string responseData = String.Empty;

            int bytes = clientStream.Read(buffer, 0, buffer.Length);
            responseData = System.Text.Encoding.ASCII.GetString(buffer,0, bytes);
            Console.WriteLine("Texto recibido: {0}", responseData);
            tcpCliente.Close();
        }
    }
}
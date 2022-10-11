using ClienteServidor.BLL.Servidor.BLL;
using ClienteServidor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClienteServidor
{
    class Servidor
    {
        
            private TcpListener Server;
            private IPEndPoint Puerto = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7000);
            private TcpClient Cliente = new TcpClient();
            private List<Connection> ListaClientes = new List<Connection>();

            Connection Conexion;

            private struct Connection
            {
                public NetworkStream streamConector;
                public StreamWriter streamEscritor;
                public StreamReader streamLector;
            }

            public Servidor()
            {
                Inicio();
            }

            public void Inicio()
            {

                Console.WriteLine("Servidor Activo");
                Server = new TcpListener(Puerto);
                Server.Start();

                while (true)
                {
                    Cliente = Server.AcceptTcpClient();

                    EstablecerConexion();

                    ListaClientes.Add(Conexion);
                    Console.WriteLine("Un cliente se ha conectado.");

                    Thread hilo = new Thread(Escuchar_conexion);

                    hilo.Start();
                }

            }

            void EstablecerConexion()
            {
                Conexion = new Connection();
                Conexion.streamConector = Cliente.GetStream();
                Conexion.streamLector = new StreamReader(Conexion.streamConector);
                Conexion.streamEscritor = new StreamWriter(Conexion.streamConector);
            }

            void Escuchar_conexion()
            {
                Connection hcon = Conexion;
                string[] agregar;
                do
                {
                    try
                    {
                        string tmp = hcon.streamLector.ReadLine();

                        if (tmp.Contains("-ReservarMesa-"))
                        {
                            agregar = tmp.Split(',');
                            AgregarCita(hcon, agregar);

                        }
                        
                    }
                    catch
                    {
                        ListaClientes.Remove(hcon);
                        Console.WriteLine("Un cliente se ha desconectado.");
                        break;
                    }
                } while (true);
            }

            private void AgregarCita(Connection hcon, string[] agregar)
            {
                Apartado apartado = new Apartado()
                {
                    Id = 0,
                    Cedula = agregar[2],
                    Nombre = agregar[1],
                    Fecha = DateTime.Parse(agregar[3] + " " + agregar[4]),
                };

                try
                {
                    bool guardo = ApartadoBLL.Guardar(apartado);
                    if (guardo)
                    {
                        Console.WriteLine("La reservacion de " + apartado.Nombre + " se ha realizado exitosamente");
                        hcon.streamEscritor.WriteLine("Estimado/a " + apartado.Nombre + " A la fecha de: " + apartado.Fecha.ToString() + " su reservacion se ha realizado de manera exitosa.");
                        hcon.streamEscritor.Flush();

                    }
                    else
                    {
                        Console.WriteLine("No se pudo guardar su reservacion...");
                        hcon.streamEscritor.WriteLine("Estimado/a " + apartado.Nombre + " A la fecha de: " + apartado.Fecha.ToString() + " su reservacion no se pudo completar...");
                        hcon.streamEscritor.Flush();

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("No se pudo guardar");
                }

            }

        }
    }



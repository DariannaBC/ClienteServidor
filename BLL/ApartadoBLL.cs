using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteServidor.BLL
{

    using System;

    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using ClienteServidor.Models;
    using ClienteServidor.DAL;

    namespace Servidor.BLL
    {
        public class ApartadoBLL
        {
            public static Apartado Buscar(int id)
            {
                Contexto contexto = new Contexto();
                Apartado apartado;

                try
                {
                    apartado = contexto.Apartados.Find(id);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    contexto.Dispose();
                }
                return apartado;
            }
            public static List<Apartado> GetApartados()
            {
                Contexto contexto = new Contexto();
                List<Apartado> lista = new List<Apartado>();
                try
                {
                    lista = contexto.Apartados.ToList();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    contexto.Dispose();
                }
                return lista;
            }
            public static bool Existe(int id)
            {
                Contexto contexto = new Contexto();
                bool paso = false;

                try
                {
                    paso = contexto.Apartados.Any(e => e.Id == id);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    contexto.Dispose();
                }
                return paso;
            }
            public static bool Insertar(Apartado apartado)
            {
                bool paso = false;
                Contexto contexto = new Contexto();
                try
                {
                    contexto.Apartados.Add(apartado);
                    paso = contexto.SaveChanges() > 0;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    contexto.Dispose();
                }

                return paso;
            }

            public static bool Modificar(Apartado apartado)
            {
                bool paso = false;
                Contexto contexto = new Contexto();

                try
                {
                    contexto.Entry(apartado).State = EntityState.Modified;
                    paso = contexto.SaveChanges() > 0;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    contexto.Dispose();
                }

                return paso;

            }
            public static bool Guardar(Apartado apartado)
            {
                if (!Existe(apartado.Id))
                    return Insertar(apartado);
                else
                    return Modificar(apartado);
            }


        }
    }
}


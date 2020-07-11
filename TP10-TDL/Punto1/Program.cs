using System;
using System.IO;
using System.Collections.Generic;

namespace Punto1
{
    class Program
    {
        static void Main(string[] args)
        {
            string ruta = @"C:\RepoGit\tpn10-PavonEvelin\TP10-TDL\Punto1\Archivos\";
            string nombreArchivo = "archivoCSV.csv";

            Propiedad propiedad = new Propiedad();
            List<string[]> FilasDelArchivo = propiedad.ListarInformacionDeArchivo(ruta, nombreArchivo);

            List<Propiedad> Propiedades = new List<Propiedad>();
            int i = 0;
            foreach (string[] fila in FilasDelArchivo)
            {
                int j = 0;
                Array valores = Enum.GetValues(typeof(TipoDeOper));
                Random rand = new Random();
                TipoDeOper tipoOperacion = (TipoDeOper)valores.GetValue(rand.Next(valores.Length));

                float tamanio = rand.Next(30, 201);
                int cantBanios = rand.Next(1, 4);
                int cantHabitaciones = rand.Next(1, 6);
                string domicilio = fila[j];
                int precio = rand.Next(200000, 1000001);
                string[] valorEstado = { "true", "false" };
                bool estado = bool.Parse(valorEstado[rand.Next(0, 2)]);
                string tipoDePropiedad = fila[j++];
                string tipoDeOperacion = tipoOperacion.ToString();

                Propiedad nuevaProp = new Propiedad(i++, tamanio, cantBanios, cantHabitaciones,
                                      domicilio, precio, estado, tipoDePropiedad, tipoDeOperacion);
                i++;
                Propiedades.Add(nuevaProp);
            }

            propiedad.GuardarInformacion(Propiedades, ruta);
        }
    }
}

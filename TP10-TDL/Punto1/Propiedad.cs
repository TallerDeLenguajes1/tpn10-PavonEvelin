using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Punto1
{
    enum TipoDeOper
    {
        Venta,
        Alquiler
    }
    enum TipoDeProp
    {
        Departamento,
        Casa,
        Duplex,
        Penhouse,
        Terreno
    }
    class Propiedad
    {
        private int id; //representa el número de propiedad ingresada
        private string tipoDePropiedad;
        private string tipoDeOperacion;
        private float tamanio; // punto flotante
        private int cantidadDeBaños;
        private int cantidadDeHabitaciones;
        private string domicilio; // string
        private int precio; // Valor Entero
        private bool estado; // bool activo / inactivo

        public Propiedad()
        {

        }
        public Propiedad(int id, float tamanio, int cantidadDeBaños, int cantidadDeHabitaciones, string domicilio, int precio, bool estado, string tipoDePropiedad, string tipoDeOperacion)
        {
            Id = id;
            Tamanio = tamanio;
            CantidadDeBaños = cantidadDeBaños;
            CantidadDeHabitaciones = cantidadDeHabitaciones;
            Domicilio = domicilio;
            Precio = precio;
            Estado = estado;
            TipoDePropiedad = tipoDePropiedad;
            TipoDeOperacion = tipoDeOperacion;
        }

        public int Id { get => id; set => id = value; }
        public float Tamanio { get => tamanio; set => tamanio = value; }
        public int CantidadDeBaños { get => cantidadDeBaños; set => cantidadDeBaños = value; }
        public int CantidadDeHabitaciones { get => cantidadDeHabitaciones; set => cantidadDeHabitaciones = value; }
        public string Domicilio { get => domicilio; set => domicilio = value; }
        public int Precio { get => precio; set => precio = value; }
        public bool Estado { get => estado; set => estado = value; }
        public string TipoDePropiedad { get => tipoDePropiedad; set => tipoDePropiedad = value; }
        public string TipoDeOperacion { get => tipoDeOperacion; set => tipoDeOperacion = value; }

        public float ValorDelInmueble()
        {
            float valorf = 0;
            if (tipoDeOperacion == TipoDeOper.Venta.ToString())
            {
                float precioConIvaf = precio * 0.21f;
                int costoTransferencia = 10000;
                valorf = precio + precioConIvaf;
                float ingresosBrutosf = valorf * 0.1f;

                valorf += ingresosBrutosf + costoTransferencia;
            }
            else
            {
                valorf = precio * 0.02f;
                float otrosCostosf = valorf * 0.005f;

                valorf += otrosCostosf;
            }

            return valorf;
        }

        public List<string[]> ListarInformacionDeArchivo(string ruta, string nombreDeArchivo)
        {
            FileStream archivo = new FileStream(ruta + nombreDeArchivo, FileMode.Open);
            StreamReader lectorDeArchivo = new StreamReader(archivo);
            string linea;

            List<string[]> FilasDelArchivo = new List<string[]>();
            while ((linea = lectorDeArchivo.ReadLine()) != null)
            {
                string[] fila = linea.Split(";");
                FilasDelArchivo.Add(fila);
            }
            lectorDeArchivo.Close();
            return FilasDelArchivo;
        }

        public void GuardarInformacion(List<Propiedad> Propiedades, string ruta)
        {
            string nombreArchi = "Propiedades.csv";

            foreach (Propiedad propiedad in Propiedades)
            {

                if (!File.Exists(ruta + nombreArchi))
                {
                    FileStream Archivo = File.Create(ruta + nombreArchi);
                }

                string estado;
                if (propiedad.Estado)
                {
                    estado = "activo";
                }
                else
                {
                    estado = "inactivo";
                }

                using (StreamWriter escritorDeArchi = new StreamWriter(nombreArchi, true))
                {

                    string Linea = propiedad.Id.ToString() + ";" + propiedad.Tamanio.ToString() + "m2" + ";" + propiedad.CantidadDeBaños.ToString()
                    + ";" + propiedad.CantidadDeHabitaciones.ToString() + ";" + propiedad.Domicilio + ";" + "$" + propiedad.Precio.ToString()
                    + ";" + estado + ";" + propiedad.TipoDePropiedad + ";" + propiedad.TipoDeOperacion + "$" +
                    propiedad.ValorDelInmueble().ToString();
                    escritorDeArchi.WriteLine(Linea);
                    escritorDeArchi.Close();
                }
            }
        }
    }
}

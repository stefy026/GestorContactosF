using System;
using System.Collections.Generic;
using System.IO;

namespace GestorContactos
{
    public class GestorContactos
    {
        private List<Contacto> listaContactos;
        private int contadorId;
        private const string ARCHIVO_DATOS = "contactos.txt";

        public GestorContactos()
        {
            listaContactos = new List<Contacto>();
            contadorId = 1;
            CargarContactos();
        }

        public List<Contacto> ObtenerContactos()
        {
            List<Contacto> copia = new List<Contacto>();

            foreach (Contacto contacto in listaContactos)
            {
                copia.Add(contacto);
            }

            return copia;
        }

        public void AgregarContacto(string nombre, string telefono, string email, string direccion)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(telefono))
            {
                throw new ArgumentException("El número de teléfono no puede estar vacío.");
            }

            Contacto nuevoContacto = new Contacto(contadorId, nombre, telefono, email, direccion);
            contadorId++;

            listaContactos.Add(nuevoContacto);
            GuardarContactos();
        }

        public List<Contacto> BuscarContacto(string nombreBuscar)
        {
            List<Contacto> resultados = new List<Contacto>();

            if (string.IsNullOrWhiteSpace(nombreBuscar))
            {
                return ObtenerContactos();
            }

            string textoBuscar = nombreBuscar.ToLower();

            foreach (Contacto contacto in listaContactos)
            {
                if (contacto.Nombre != null)
                {
                    string nombreContacto = contacto.Nombre.ToLower();

                    if (nombreContacto.Contains(textoBuscar))
                    {
                        resultados.Add(contacto);
                    }
                }
            }

            return resultados;
        }

        public void EditarContacto(int id, string nombre, string telefono, string email, string direccion)
        {
            Contacto contacto = BuscarPorId(id);

            if (contacto == null)
            {
                throw new Exception("Contacto no encontrado.");
            }

            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(telefono))
            {
                throw new ArgumentException("El teléfono no puede estar vacío.");
            }

            contacto.Nombre = nombre;
            contacto.NumeroTelefono = telefono;
            contacto.CorreoElectronico = email;
            contacto.Direccion = direccion;

            GuardarContactos();
        }

        public void EliminarContacto(int id)
        {
            Contacto contacto = BuscarPorId(id);

            if (contacto == null)
            {
                throw new Exception("Contacto no encontrado.");
            }

            listaContactos.Remove(contacto);
            GuardarContactos();
        }

        private Contacto BuscarPorId(int id)
        {
            foreach (Contacto contacto in listaContactos)
            {
                if (contacto.Id == id)
                {
                    return contacto;
                }
            }

            return null;
        }

        private void GuardarContactos()
        {
            using (StreamWriter sw = new StreamWriter(ARCHIVO_DATOS))
            {
                foreach (Contacto contacto in listaContactos)
                {
                    sw.WriteLine(contacto.ToString());
                }
            }
        }

        private void CargarContactos()
        {
            try
            {
                if (File.Exists(ARCHIVO_DATOS))
                {
                    string[] lineas = File.ReadAllLines(ARCHIVO_DATOS);

                    foreach (string linea in lineas)
                    {
                        string[] datos = linea.Split('|');

                        if (datos.Length == 5)
                        {
                            Contacto contacto = new Contacto();

                            contacto.Id = int.Parse(datos[0]);
                            contacto.Nombre = datos[1];
                            contacto.NumeroTelefono = datos[2];
                            contacto.CorreoElectronico = datos[3];
                            contacto.Direccion = datos[4];

                            listaContactos.Add(contacto);

                            if (contacto.Id >= contadorId)
                            {
                                contadorId = contacto.Id + 1;
                            }
                        }
                    }
                }
            }
            catch
            {
                listaContactos = new List<Contacto>();
                contadorId = 1;
            }
        }
    }
}
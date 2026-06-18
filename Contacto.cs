using System;

namespace GestorContactos
{
    public class Contacto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NumeroTelefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string Direccion { get; set; }

        public Contacto()
        {
        }

        public Contacto(int id, string nombre, string numeroTelefono, string correoElectronico, string direccion)
        {
            Id = id;
            Nombre = nombre;
            NumeroTelefono = numeroTelefono;
            CorreoElectronico = correoElectronico;
            Direccion = direccion;
        }

        public override string ToString()
        {
            return Id + "|" + Nombre + "|" + NumeroTelefono + "|" + CorreoElectronico + "|" + Direccion;
        }
    }
}

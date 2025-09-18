using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio.ValueObjects
{
    [Owned]
    public class Email
    {
        static string dominio = "@laEmpresa.com";
        public string EmailUsuario { get; private set; }
        public Email(NombreCompleto nombreCompleto)
        {
            string uno;
            string dos;
            if(nombreCompleto.Nombre.Length <= 3)
            {
                uno = nombreCompleto.Nombre;
            }
            else
            {
                uno = nombreCompleto.Nombre.Substring(0, 3);
            }
            if (nombreCompleto.Apellido.Length <= 3)
            {
                dos = nombreCompleto.Apellido;
            }
            else
            {
                dos = nombreCompleto.Apellido.Substring(0, 3);
            }
            EmailUsuario = uno + dos + dominio;
        }
        public Email() { }

    }
}

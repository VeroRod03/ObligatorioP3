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
        public static string dominio = "@laEmpresa.com";
        public string EmailUsuario { get; private set; }
        public Email() { }
        public Email(string emailUsuario)
        {
            EmailUsuario = emailUsuario;
        }
        public Email(NombreCompleto nombreCompleto)
        {
            string uno;
            string dos;
            if (nombreCompleto.Nombre.Length <= 3)
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
            EmailUsuario = (uno + dos + dominio).ToLower();
            EmailUsuario = EmailUsuario
                .Replace("á", "a")
                .Replace("é", "e")
                .Replace("í", "i")
                .Replace("ó", "o")
                .Replace("ú", "u")
                .Replace("ü", "u")
                .Replace("ñ", "n");
        }

        public void AgregarNumeroRandom()
        {
            Random numRandom = new Random();
            int secuenciaRandom = numRandom.Next(0,9);
            string[] emailSplit = EmailUsuario.Split("@");
            emailSplit[0] += secuenciaRandom;
            EmailUsuario = emailSplit[0] + "@" + emailSplit[1];
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || !(obj is Email)) return false;
            Email email = (Email)obj;
            return email.EmailUsuario.Equals(EmailUsuario);
        }

    }
}

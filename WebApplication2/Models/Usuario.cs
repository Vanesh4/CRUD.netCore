﻿using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Usuario
    {
        public string? nombre { get; set; }
        public string? userName { get; set; }
        public string? clave { get; set; }
        public string? correo { get; set; }
        

        [NotMapped]
        public bool MantenerActivo { get; set; }

        //public Usuario(string nombre, string userName, string correo, string clave)
        //{
        //    this.nombre = nombre;
        //    this.userName = userName;
        //    this.clave = clave;
        //    this.correo = correo;
        //}

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public class Client
    {
        public int Id { get; set; }
        public int Dni { get; set; }
        public int Ruc { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool ActiveFlag { get; set; }
        public char ClientType { get; set; }
    }
}
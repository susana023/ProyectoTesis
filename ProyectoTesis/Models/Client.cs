﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTesis.Models
{
    public enum ClientType
    {
        A, B, C, D, F
    }
    public class Client
    {
        public int ID { get; set; }
        public int Dni { get; set; }
        public int Ruc { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool ActiveFlag { get; set; }
        public ClientType? ClientType { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
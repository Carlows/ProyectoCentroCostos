using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CentroCostos.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
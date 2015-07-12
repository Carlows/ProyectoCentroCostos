﻿using CentroCostos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CentroCostos.Infrastructure.Repositorios
{
    public interface IModeloRepository : IRepository<Modelo, int>
    {
        Modelo Find(string codigo);
        string UploadImage(string codigo, HttpPostedFileBase imagen, string serverPath);
    }
}
using CentroCostos.Helpers;
using CentroCostos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCostos.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private ApplicationContext _context { get; set; }

        public UserService(ApplicationContext context)
        {
            _context = context;
        }

        public bool Authenticate(string username, string password)
        {
            string HashedPassword = PasswordHelpers.Hash(password);

            var usuario = _context.Usuarios
                        .Where(user => user.UserName == username && user.Password == HashedPassword)
                        .SingleOrDefault();

            return usuario != null;
        }

        public void CreateUser(string username, string password)
        {
            string HashedPassword = PasswordHelpers.Hash(password);

            Usuario user = new Usuario
            {
                UserName = username,
                Password = HashedPassword
            };

            _context.Usuarios.Add(user);
            _context.SaveChanges();
        }
    }
}
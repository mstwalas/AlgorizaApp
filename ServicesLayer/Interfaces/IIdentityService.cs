using CoreLayer.Model;
using RepositryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Interfaces
{
    public interface IIdentityService : IRepositry<Identity>
    {
        void AddUser(Identity User);
        public bool verifyPasswordHash(string Password, byte[] PasswordHash, byte[] PasswordSalt);
        public void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt);
        public string CreateToken(Identity UserIdentity);
    }
}

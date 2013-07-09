using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Alicargo.Core.Security
{    
    public sealed class PasswordConverter : IPasswordConverter
    {
        private static readonly Lazy<SHA256> Lazy = new Lazy<SHA256>(SHA256.Create);

        public byte[] GetPasswordHash(string password, byte[] salt)
        {
            var bytes = Encoding.UTF8.GetBytes(password).Concat(salt).ToArray();

            return Lazy.Value.ComputeHash(bytes);
        }

	    public byte[] GenerateSalt()
	    {
		    return Guid.NewGuid().ToByteArray();
	    }
    }
}
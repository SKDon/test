using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Alicargo.Utilities
{    
    public sealed class PasswordConverter : IPasswordConverter
    {
        private static readonly Lazy<SHA256> SHA256 = new Lazy<SHA256>(System.Security.Cryptography.SHA256.Create);

        public byte[] GetPasswordHash(string password, byte[] salt)
        {
            var bytes = Encoding.UTF8.GetBytes(password).Concat(salt).ToArray();

            return SHA256.Value.ComputeHash(bytes);
        }

	    public byte[] GenerateSalt()
	    {
		    return Guid.NewGuid().ToByteArray();
	    }
    }
}
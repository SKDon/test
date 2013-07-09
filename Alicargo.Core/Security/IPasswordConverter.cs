namespace Alicargo.Core.Security
{
    public interface IPasswordConverter
    {
        byte[] GetPasswordHash(string password, byte[] salt);
	    byte[] GenerateSalt();
    }
}

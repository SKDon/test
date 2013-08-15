namespace Alicargo.Contracts.Helpers
{
    public interface IPasswordConverter
    {
        byte[] GetPasswordHash(string password, byte[] salt);
	    byte[] GenerateSalt();
    }
}

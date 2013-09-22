using System.IO;

namespace Alicargo.Services.Abstract
{
	public  interface IExcelGenerator
	{
		MemoryStream Applications(long clientId);
	}
}

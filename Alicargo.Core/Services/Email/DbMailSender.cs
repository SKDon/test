using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Services.Abstract;

namespace Alicargo.Core.Services.Email
{
	public sealed class DbMailSender : IMailSender
	{
		private readonly int _partitionId;
		private readonly IEmailMessageRepository _repository;
		private readonly ISerializer _serializer;

		public DbMailSender(int partitionId, IEmailMessageRepository repository, ISerializer serializer)
		{
			_partitionId = partitionId;
			_repository = repository;
			_serializer = serializer;
		}

		public void Send(params EmailMessage[] messages)
		{
			foreach (var message in messages)
			{
				_repository.Add(_partitionId, message.From, message.To, message.CopyTo, message.Subject, message.Body,
					message.IsBodyHtml, _serializer.Serialize(message.Files));
			}
		}
	}
}
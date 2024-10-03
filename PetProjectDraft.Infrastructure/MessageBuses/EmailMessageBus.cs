using Microsoft.Extensions.Logging;
using PetProjectDraft.Application.Interfaces.MessageBus;
using PetProjectDraft.Application.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Infrastructure.MessageBuses
{
    public class EmailMessageBus(
   EmailMessageChannel messageChannel,
   ILogger<EmailMessageChannel> logger) : IMessageBus
    {
        public async Task PublishAsync(EmailNotification emailNotification, CancellationToken ct)
        {
            await messageChannel.Writer.WriteAsync(emailNotification, ct);
            logger.LogInformation("Email message successfully delivered");
        }
    }
}

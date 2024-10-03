using PetProjectDraft.Application.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Application.Interfaces.MessageBus
{
    public interface IMessageBus
    {
        Task PublishAsync(EmailNotification emailNotification, CancellationToken ct);
    }
}

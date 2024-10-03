using PetProjectDraft.Application.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace PetProjectDraft.Infrastructure.MessageBuses
{
    public class EmailMessageChannel
    {
        private readonly Channel<EmailNotification> _channel = Channel.CreateUnbounded<EmailNotification>();

        public ChannelWriter<EmailNotification> Writer => _channel.Writer;

        public ChannelReader<EmailNotification> Reader => _channel.Reader;
    }
}

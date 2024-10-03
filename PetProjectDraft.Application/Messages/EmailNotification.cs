using PetProjectDraft.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Application.Messages
{
    public record EmailNotification(string Message, Email Email);
}

using PetProjectDraft.Application.Models;
using PetProjectDraft.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Application.Interfaces.Services
{
    public interface INotificationService
    {
        public Task<Result> Notify(Notification notification, CancellationToken ct);
    }
}

using Microsoft.AspNetCore.Http.HttpResults;
using Minio.DataModel;
using PetProjectDraft.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Application.Interfaces.Providers
{
    public interface IMinioProvider
    {
        Task<Result<string>> UploadPhoto(Stream stream, string path, CancellationToken ct);
        Task<Result<bool>> RemovePhoto(string path, CancellationToken ct);
        Task<Result<IReadOnlyList<string>>> GetPhotos(IEnumerable<string> pathes, CancellationToken ct);
        IObservable<Item> GetObjectsList(CancellationToken ct);
        Task<Result<bool>> RemovePhotos(List<string> paths, CancellationToken ct);
    }
}

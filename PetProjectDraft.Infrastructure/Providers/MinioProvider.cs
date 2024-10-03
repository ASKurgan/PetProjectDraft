using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Minio.DataModel.Args;
using Minio.DataModel;
using Minio;
using PetProjectDraft.Application.Interfaces.Providers;
using PetProjectDraft.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minio.ApiEndpoints;

namespace PetProjectDraft.Infrastructure.Providers
{
    public class MinioProvider : IMinioProvider
    {
        private const string PhotoBucket = "images";

        private readonly IMinioClient _minioClient;
        private readonly ILogger<MinioProvider> _logger;

        public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
        {
            _minioClient = minioClient;
            _logger = logger;
        }

        public async Task<Result<string>> UploadPhoto(Stream stream, string path, CancellationToken ct)
        {
            try
            {
                // Создаём баккеты

                var bucketExistArgs = new BucketExistsArgs()
                    .WithBucket(PhotoBucket);

                var bucketExist = await _minioClient.BucketExistsAsync(bucketExistArgs, ct);

                if (bucketExist == false)
                {
                    //MakeBucketArgs makeBucket = new MakeBucketArgs();
                    //var bucketArgs = makeBucket.WithBucket(PhotoBucket);
                    
                    var makeBucketArgs = new MakeBucketArgs() // MakeBucketArgs() - метод создаёт аргументы баккетов 
                        .WithBucket(PhotoBucket);

                    await _minioClient.MakeBucketAsync(makeBucketArgs, ct); // MakeBucketAsync() - метод создаёт баккеты
                }


                // Создаём параметры для картинки (фото)
                var putObjectArgs = new PutObjectArgs() // по традиции сначала создаются аргументы
                    .WithBucket(PhotoBucket)
                    .WithStreamData(stream) // передаём фото байтами в потоке для чтения файла
                    .WithObjectSize(stream.Length)
                    .WithObject(path); // WithObject() - метод для сохранения названия объекта. Guid + расширение файла 
                                       // расширение файла - JPEG, JPG и тд. То есть path = Guid.jpeg и тп 
                                       // формирование path Происходит в классе UploadVolunteerPhotoHandler  в методе Handle()
                                       // streem тоже получаем в классе UploadVolunteerPhotoHandler  в методе Handle()

                var response = await _minioClient.PutObjectAsync(putObjectArgs, ct);

                return response.ObjectName;
            }
            catch (Exception e)
            {
                _logger.LogError("Error while saving file in minio: {message}", e.Message);
                return Errors.General.SaveFailure("photo");
            }
        }

        public async Task<Result<bool>> RemovePhoto(string path, CancellationToken ct)
        {
            try
            {
                var bucketExistArgs = new BucketExistsArgs()
                    .WithBucket(PhotoBucket);

                var bucketExist = await _minioClient.BucketExistsAsync(bucketExistArgs, ct);

                if (bucketExist == false)
                {
                    var makeBucketArgs = new MakeBucketArgs()
                        .WithBucket(PhotoBucket);

                    await _minioClient.MakeBucketAsync(makeBucketArgs, ct);
                }

                var removeObjectArgs = new RemoveObjectArgs()
                    .WithBucket(PhotoBucket)
                    .WithObject(path);

                await _minioClient.RemoveObjectAsync(removeObjectArgs, ct);

                // return Result.Success();
                return Result.Success().IsSucces;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Errors.General.SaveFailure("photo");
            }
        }

        public async Task<Result<bool>> RemovePhotos(List<string> paths, CancellationToken ct)
        {
            try
            {
                var bucketExistArgs = new BucketExistsArgs()
                    .WithBucket(PhotoBucket);

                var bucketExist = await _minioClient.BucketExistsAsync(bucketExistArgs, ct);

                if (bucketExist == false)
                {
                    var makeBucketArgs = new MakeBucketArgs()
                        .WithBucket(PhotoBucket);

                    await _minioClient.MakeBucketAsync(makeBucketArgs, ct);
                }

                var removeObjectsArgs = new RemoveObjectsArgs()
                    .WithBucket(PhotoBucket)
                    .WithObjects(paths);

                await _minioClient.RemoveObjectsAsync(removeObjectsArgs, ct);
                // return Result.Success(); 
                return Result.Success().IsSucces;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Errors.General.SaveFailure("photo");
            }
        }

        public async Task<Result<IReadOnlyList<string>>> GetPhotos(IEnumerable<string> paths, CancellationToken ct)
        {
            try
            {
                List<string> urls = [];

                foreach (var path in paths)
                {
                    var presignedGetObjectArgs = new PresignedGetObjectArgs()
                        .WithBucket(PhotoBucket)
                        .WithObject(path)
                        .WithExpiry(60 * 60 * 24);

                    var url = await _minioClient.PresignedGetObjectAsync(presignedGetObjectArgs);
                    urls.Add(url);
                }

                return urls;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Errors.General.SaveFailure("photo");
            }
        }

        public IObservable<Item> GetObjectsList(CancellationToken ct)
        {
            var listObjectArgs = new ListObjectsArgs().WithBucket(PhotoBucket);

            return _minioClient.ListObjectsAsync(listObjectArgs, ct);
        }
    }
}

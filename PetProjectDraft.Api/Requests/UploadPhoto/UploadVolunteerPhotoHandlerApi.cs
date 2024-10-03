using Microsoft.AspNetCore.Http.HttpResults;
using PetProjectDraft.Application.DataAccess;
using PetProjectDraft.Application.Features.Volunteers;
using PetProjectDraft.Application.Interfaces.Providers;
using PetProjectDraft.Domain.Common;
using PetProjectDraft.Domain.Entities;

namespace PetProjectDraft.Api.Requests.UploadPhoto
{
    public class UploadVolunteerPhotoHandlerApi
    {
        private readonly IMinioProvider _minioProvider;
        private readonly IVolunteersRepository _volunteersRepository;
        private readonly ITransaction _transaction;

        public UploadVolunteerPhotoHandlerApi(
            IMinioProvider minioProvider,
            IVolunteersRepository volunteersRepository,
            ITransaction transaction)
        {
            _minioProvider = minioProvider;
            _volunteersRepository = volunteersRepository;
            _transaction = transaction;
        }

        // Что нужно в этом методе?

        // Получить волонтёра
        // Создать фото (photo) экземпляр
        // Загрузить фотографии в MinIo
        // Сохранить photo у волонтёра в бд
        // Вернуть, что всё ok
        public async Task<Result<string>> Handle(UploadVolunteerPhotoRequest request, CancellationToken ct)
        {
            var volunteer = await _volunteersRepository.GetById(request.VolunteerId, ct);
            if (volunteer.IsFailure)
                return volunteer.Error;

            var photoId = Guid.NewGuid(); // создаём photoId для привязки его к пути с расширением, 
                                          // чтобы в дальнейшем по этому пути фронтенд мог скачать его. К Id самой фото, 
                                          // которое будет в бд (будет указан путь на скачивание) он отношения не имеет
            var path = photoId + Path.GetExtension(request.File.FileName); // GetExtension(request.File.FileName) - 
                                                                           // получаем расширение файла (jpeg и тд)
                                                                           //  path получит в итоге Guid.jpeg
            var photo = VolunteerPhoto.CreateAndActivate(path, request.File.ContentType,
                request.File.Length, request.IsMain);                      // request.File.ContentType и есть в нашем случае
                                                                           // JPEG и тп

            if (photo.IsFailure)
                return photo.Error;

            var isSuccessUpload = volunteer.Value.AddPhoto(photo.Value);//пересмотреть алгоритм добавления фото в коллекцию волонтёра 
                                                                        //сначала нужно, после проверки на PHOTO_COUNT_LIMIT , сохранить 
                                                                        //в MinioProvider и в б.д. (транзакции под вопросом), 
                                                                        //и только после этого добавлять фото в коллекцию 
                                                                        // _photos.Add(volunteerPhoto) в классе Volunteer. 
                                                                        //С транзакциями  не получится, так как minioProvider
                                                                        //является сторонним сервисом. Две разные базы и между ними 
                                                                        //не получится провести транзакцию
            if (isSuccessUpload.IsFailure)
                return isSuccessUpload.Error;

            await using var stream = request.File.OpenReadStream();
            var objectName = await _minioProvider.UploadPhoto(stream, path, ct);
            if (objectName.IsFailure)
                return objectName.Error;

            await _transaction.SaveChangesAsync(ct);

            return path;
        }
    }
}

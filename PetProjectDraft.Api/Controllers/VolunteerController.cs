using Microsoft.AspNetCore.Mvc;
using Minio.DataModel.Args;
using Minio;
using PetProjectDraft.Api.Requests.PublishPet;
using PetProjectDraft.Api.Requests.UploadPhoto;
using PetProjectDraft.Application.Features.Volunteers.PublishPet;
using PetProjectDraft.Application.Interfaces.Providers;
using PetProjectDraft.Api.Mappers.MapperPublishPet;
using PetProjectDraft.Application.Features.Volunteers.DeletePhoto;
using PetProjectDraft.Api.Requests.DeletePhoto;
using PetProjectDraft.Api.Mapping.FromRequestToCommand;

namespace PetProjectDraft.Api.Controllers
{
    [Route("[controller]")]
    public class VolunteerController : ApplicationController
    {
        [HttpPost("petDraftPet")]
        // [HasPermission(Permissions.Pets.Create)]
        public async Task<IActionResult> DraftPublishPet(
          [FromServices] PublishPetHandler handler,
          [FromServices] IMinioProvider minioProvider,
          [FromForm] PublishPetRequest request,
          CancellationToken ct)
        {
            var mapRequestCommand = new MapFromRequestToCommand(minioProvider);
            var commandResult = await mapRequestCommand.GetPublishPetCommand(request, ct);

            if (commandResult.IsFailure)
                return BadRequest(commandResult.Error);

            var command = commandResult.Value;

           

            var idResult = await handler.Handle(command, ct);

            if (idResult.IsFailure)
                return BadRequest(idResult.Error);

            return Ok(idResult.Value);
        }

        [HttpPost("pet")]
        // [HasPermission(Permissions.Pets.Create)]
        public async Task<IActionResult> PublishPet(
            [FromServices] PublishPetHandlerApi handler,
            [FromForm] PublishPetRequest request,
            CancellationToken ct)
        {
            var idResult = await handler.Handle(request, ct);

            if (idResult.IsFailure)
                return BadRequest(idResult.Error);

            return Ok(idResult.Value);
        }



        [HttpPost("photo")]
        public async Task<IActionResult> UploadPhoto(
            [FromServices] UploadVolunteerPhotoHandlerApi handler,
            [FromForm] UploadVolunteerPhotoRequest request,
            CancellationToken ct)
        {
            var result = await handler.Handle(request, ct);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        //[HttpGet("photo")]
        //public async Task<IActionResult> GetPhotos(
        //    [FromServices] GetVolunteerPhotoQuery handler,
        //    [FromQuery] GetVolunteerPhotoRequest request,
        //    CancellationToken ct)
        //{
        //    var result = await handler.Handle(request, ct);
        //    if (result.IsFailure)
        //        return BadRequest(result.Error);

        //    return Ok(result.Value);
        //}

        [HttpDelete("photo")]
        public async Task<IActionResult> DeletePhoto(
            [FromServices] DeleteVolunteerPhotoHandler handler,
            [FromQuery] DeleteVolunteerPhotoRequest request,
            CancellationToken ct)
        {
            var commandResult = DeleteVolunteerPhotoMapper.GetDeleteVolunteerPhotoCommand(request,ct);
            if (commandResult.IsFailure) 
                return BadRequest(commandResult.Error);

            var command = commandResult.Value;
            var result = await handler.Handle(command, ct);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetVolunteers([FromServices] GetVolunteersQuery query,
        //    [FromQuery] GetVolunteersRequest request,
        //    CancellationToken ct)
        //{
        //    var idResult = await query.Handle(request, ct);
        //    if (idResult.IsFailure)
        //        return BadRequest(idResult.Error);
        //    return Ok(idResult.Value);
        //}

        //[HttpGet("GetById")]
        //public async Task<IActionResult> GetById([FromQuery] GetVolunteerRequest request,
        //    [FromServices] GetVolunteerQuery query,
        //    CancellationToken ct)
        //{
        //    var idResult = await query.Handle(request, ct);
        //    if (idResult.IsFailure)
        //        return BadRequest(idResult.Error);
        //    return Ok(idResult.Value);
        //}



        //// Черновые методы


        //// Метод будет сохранять url на скачивание файлов. Не сами фото заливать, а именно url на скачивание
        //// Удобно
        //[HttpGet("photoUrl")]
        //public async Task<IActionResult> GetPhotosUrl(
        //    [FromServices] GetVolunteerPhotoQuery handler,
        //    [FromQuery] GetVolunteerPhotoRequest request,
        //    string photo,
        //    [FromServices] IMinioClient client,
        //    CancellationToken ct)
        //{
        //    var presignetGetObjectArgs = new PresignedGetObjectArgs()
        //          .WithBucket("images")
        //          .WithObject(photo)
        //          .WithExpiry(604800); // устанавливаем время жизни ссылки на скачивание нашего photo 
        //                               // (604800 означает - неделя). Можно написать WithExpiry(60*60*24) - это будут
        //                               // 1 сутки

        //    var url = await client.PresignedGetObjectAsync(presignetGetObjectArgs);

        //    return Ok(url); // скачаем картинку по url
        //}                        // return Ok(url) означает, что мы на фронтенд возвращаем ссылку на скачивание фото
        //                         // (нашу url) 

        //[HttpPost("petDraft")]
        //// [HasPermission(Permissions.Pets.Create)]
        //public async Task<IActionResult> DraftPublishPet(
        //   [FromServices] PublishPetHandler handler,
        //   [FromServices] IMinioProvider minioProvider,
        //   [FromForm] PublishPetRequest request,
        //   CancellationToken ct)
        //{
        //    var mapRequestCommand = new MapRequestCommand(minioProvider);
        //    var commandResult = await mapRequestCommand.GetPublishPetCommand(request, ct);

        //    if (commandResult.IsFailure)
        //        return BadRequest(commandResult.Error);

        //    var command = commandResult.Value;

        //    // Далее необходимо создать другие методы в PublishPetHandler handler и это - другая логика

        //    var idResult = await handler.Handle(request, ct);

        //    if (idResult.IsFailure)
        //        return BadRequest(idResult.Error);

        //    return Ok(idResult.Value);
        //}
    }
}

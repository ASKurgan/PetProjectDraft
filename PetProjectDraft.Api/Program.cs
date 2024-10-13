using PetProjectDraft.Api;
using PetProjectDraft.Api.Middlewares;
using PetProjectDraft.Api.Validation;
using PetProjectDraft.Application;
using PetProjectDraft.Infrastructure;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PetProjectDraft.Api.Extensions;
using PetProjectDraft.Infrastructure.DbContexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// builder.Services.AddSerilog();

builder.Services.AddAuth(builder.Configuration);
builder.Services.AddSwagger();

builder.Services.AddFluentValidationAutoValidation(configuration =>
{
    configuration.OverrideDefaultResultFactoryWith<CustomResultFactory>();
});

builder.Services.AddProblemDetails();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddApi();

var app = builder.Build();


var scope = app.Services.CreateScope();

var dbContext = scope.ServiceProvider.GetRequiredService<PetProjectWriteDbContext>();

await dbContext.Database.MigrateAsync();

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionMiddleware>();


app.UseHttpsRedirection();

app.UseAuthorization();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();

app.Run();

using Server.Application;
using Server.Infrastructure;
using Server.Presentation;
using Server.WebAPI;
using Server.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddCors();

builder
    .Services
    .AddApplication();

builder
    .Services
    .AddInfrastructure(builder.Configuration);

builder
    .Services
    .AddPresentation();

builder
    .Services
    .AddControllers()
    .AddApplicationPart(typeof(AssemblyMarker).Assembly);

builder
    .Services
    .AddEndpointsApiExplorer();

builder
    .Services
    .AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandler>();

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader().SetIsOriginAllowed(p => true).AllowAnyMethod().AllowCredentials());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers().RequireAuthorization();

if (builder.Environment.IsDevelopment())
{
    Helper.GenerateData(app).Wait();
}

app.Run();
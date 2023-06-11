using System.Text.Json.Serialization;
using Marketplace.Common.Loggers;
using Marketplace.Services.Identity.Extensions;
using Marketplace.Services.Identity.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = CustomLogger
    .WriteLogToFileSendToTelegram(builder.Configuration, "IdentityLogger.txt" );

builder.Logging.AddSerilog(logger);

builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(cors =>
{
    cors.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
});

app.UseIdentityErrorHandlerMiddleware();
//app.AutoMigrateIdentityDb();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

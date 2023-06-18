using Marketplace.Common.Loggers;
using Marketplace.Services.Organization.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = CustomLogger
    .WriteLogToFileSendToTelegram(builder.Configuration, "OrganizationLogger.txt");

builder.Logging.AddSerilog(logger);

builder.Services.AddOrganizationServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseSwagger();
//app.UseSwaggerUI();

app.UseCors(cors =>
{
    cors.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
});

//app.AutoMigrateOrganizationDb();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

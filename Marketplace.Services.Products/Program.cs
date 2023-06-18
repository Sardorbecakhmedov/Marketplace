using Marketplace.Common.Loggers;
using Marketplace.Services.Products.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = CustomLogger
    .WriteLogToFileSendToTelegram(builder.Configuration, "ProductsLogger.txt");

builder.Logging.AddSerilog(logger);

builder.Services.AddProductServices(builder.Configuration);


var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(cors =>
{
    cors.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyOrigin();
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

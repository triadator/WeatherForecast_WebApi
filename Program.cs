using Web_Api.Interfaces;
using Web_Api.OpenMeteo;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IForecastProvider, OpenMeteoClient>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

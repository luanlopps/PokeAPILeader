using PokeAPIBackend.Services;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Habilita CORS para permitir acesso do frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:3000") // Endere�o do frontend
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

// Registra os servi�os e controladores
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
});

// Configura o servi�o HttpClient para a API externa
builder.Services.AddHttpClient<PokeApiService>();

// Configura o Swagger para documenta��o e teste dos endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Habilita o Swagger e o Swagger UI em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplica a pol�tica de CORS
app.UseCors("AllowFrontend");

// Mapeia os controladores
app.MapControllers();

app.Run();

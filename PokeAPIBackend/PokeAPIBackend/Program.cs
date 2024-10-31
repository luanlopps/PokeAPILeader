using PokeAPIBackend.Services;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Habilita CORS para permitir acesso do frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:3000") // Endereço do frontend
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

// Registra os serviços e controladores
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
});

// Configura o serviço HttpClient para a API externa
builder.Services.AddHttpClient<PokeApiService>();

// Configura o Swagger para documentação e teste dos endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Habilita o Swagger e o Swagger UI em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplica a política de CORS
app.UseCors("AllowFrontend");

// Mapeia os controladores
app.MapControllers();

app.Run();

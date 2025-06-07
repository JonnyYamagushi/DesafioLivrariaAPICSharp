using DesafioLivrariaAPICSharp.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Registrar o DbContext, usando banco em memória chamado "LivrariaDb"
builder.Services.AddDbContext<BookstoreContext>(options =>
    options.UseInMemoryDatabase("LivrariaDb"));

// Habilitar controllers
builder.Services.AddControllers();

// Adicionar Swagger para documentação/testes
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Defina um nome consistente com o seu assembly principal
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

var app = builder.Build();

// Configurar pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Habilitar roteamento para controllers
app.MapControllers();

app.Run();

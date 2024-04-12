
using Cadastro_Livros.DataContext;
using Cadastro_Livros.Services.AssuntoService;
using Cadastro_Livros.Services.AutorService;
using Cadastro_Livros.Services.LivroService;
using Glimpse.AspNet.Tab;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Web.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAutorInterface, AutorService>();
builder.Services.AddScoped<IAssuntoInterface, AssuntoService>();
builder.Services.AddScoped<ILivroInterface, LivroService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

//app(routes =>
// {
//     Routes.MapHttpRoute(
//        name: "ActionApi",
//        routeTemplate: "api/{controller}/{action}/{id}",
//        defaults: new { id = RouteParameter.Optional }
//});

app.Run();

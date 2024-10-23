using System.Reflection;
using System.Text;
using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("ChefHubAuthentication", new OpenApiSecurityScheme() //Esto va a permitir usar 
                                                                                           //swagger con el token.
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Ac� pegar el token generado al loguearse." //este mensaje va literalmente asi, no modificarlo.
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ChefHubAuthentication",  } //Tiene que coincidir con el id seteado arriba en la definici�n
                }, new List<string>() }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    setupAction.IncludeXmlComments(xmlPath);      //para usar esto, previamente hay que agregar esta etiqueta: 
                                                  //<GenerateDocumentationFile>true</GenerateDocumentationFile> en el archivo csproj de web en la parte de property group

});
builder.Services.AddAuthentication("Bearer") //"Bearer" es el tipo de auntenticaci�n que tenemos que elegir despu�s en PostMan para pasarle el token
    .AddJwtBearer(options => //Ac� definimos la configuraci�n de la autenticaci�n. le decimos qu� cosas queremos comprobar. La fecha de expiraci�n se valida por defecto.
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["AuthenticationService:Issuer"],
            ValidAudience = builder.Configuration["AuthenticationService:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["AuthenticationService:SecretForKey"]))
        };
    }
);
//-----------------------------------------------------------------------------------
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlite(builder.Configuration.GetConnectionString("Connection")));
//-----------------------------------------------------------------------------------
builder.Services.AddScoped<IRepositoryBase<User>, EFRepository<User>>();
builder.Services.AddScoped<IRepositoryBase<Recipe>, EFRepository<Recipe>>();
builder.Services.AddScoped<IRepositoryBase<Comment>, EFRepository<Comment>>();
builder.Services.AddScoped<IRepositoryBase<Favorite>, EFRepository<Favorite>>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<UserMapping>();
builder.Services.AddScoped<RecipeMapping>();
builder.Services.AddScoped<CommentMapping>();
builder.Services.AddScoped<FavoriteMapping>();
builder.Services.AddScoped<ICustomAuthenticationService, AuthenticationService>();

builder.Services.Configure<AuthenticationServiceOptions>(
    builder.Configuration.GetSection(AuthenticationServiceOptions.Authentication));
//--------------------------------------------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAll");
app.MapControllers();
app.Run();

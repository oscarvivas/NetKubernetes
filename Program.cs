using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NetKubernetes.Data;
using NetKubernetes.Data.Inmuebles;
using NetKubernetes.Data.InterviewProgram;
using NetKubernetes.Data.Users;
using NetKubernetes.Middleware;
using NetKubernetes.Models;
using NetKubernetes.Profiles;
using NetKubernetes.Token;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Adiciona el soporte del entityFramework
// builder.Services.AddDbContext<AppDbContext>(opt => {
//         // Agrega un log por cada sentencia sql que se ejecute
//         opt.LogTo(Console.WriteLine, 
//                     new [] { DbLoggerCategory.Database.Command.Name },
//                     LogLevel.Information
//                 ).EnableSensitiveDataLogging();

//         // Agrega el connction string desde el motor de sql
//         opt.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection")!);
//     }
// );

var connectionMySqlString = builder.Configuration.GetConnectionString("MySqlConnection");
builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseMySql(connectionMySqlString, ServerVersion.AutoDetect(connectionMySqlString));
});

builder.Services.AddScoped<IInmuebleRepository, InmuebleRepository>();
builder.Services.AddScoped<IInterviewRepository, InterviewRepository>();

builder.Services.AddControllers(opt => {
        // Agrega la restriccion de autenticacion
        var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        opt.Filters.Add(new AuthorizeFilter(policy));
    }
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// agrega el soporte del mapper que transforma clases
var mapperConfig = new MapperConfiguration(mc => {
    mc.AddProfile(new InmuebleProfile());
    mc.AddProfile(new InterviewProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


var builderSecurity = builder.Services.AddIdentityCore<UserApp>();
var identityBuilder = new IdentityBuilder(builderSecurity.UserType, builder.Services); 
identityBuilder.AddEntityFrameworkStores<AppDbContext>();
identityBuilder.AddSignInManager<SignInManager<UserApp>>();
builder.Services.AddSingleton<ISystemClock, SystemClock>();
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
builder.Services.AddScoped<IUserSession, UserSession>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi palabra secreta"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => {
                    opt.TokenValidationParameters =  new TokenValidationParameters 
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateAudience = false,
                        ValidateIssuer = false
                    };
                });

builder.Services.AddCors(opt => opt.AddPolicy("corsapp", builder => {
    builder.WithOrigins("*")
            .AllowAnyMethod()
            .AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ManagerMiddleware>();

app.UseAuthentication();
//app.UseHttpsRedirection();
app.UseCors("corsapp");

app.UseAuthorization();

app.MapControllers();


//Realizar lo procesos de migracion de base de datos
//es decir crear la tablas en la base de datos
using(var environment = app.Services.CreateScope())
{
    var services = environment.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<UserApp>>();
        var context = services.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
        await LoadDatabase.InsertData(context, userManager);
    }
    catch (Exception ex)
    {
        var logging = services.GetRequiredService<ILogger<Program>>();
        logging.LogError(ex, "Ocurrio un error en la migracion");
    }
}

app.Run();

using Configuracion;
using LibraryTrabajoFinal.DAOS;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using TrabajoFinal.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Configuración de Servicios
ConfigureServices(builder);

var app = builder.Build();

// Configuración del Pipeline de Middleware
ConfigureMiddleware(app);

app.Run();

void ConfigureServices(WebApplicationBuilder builder)
{
    // Agregar controladores y configurar JSON
    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            //Configura el serializador JSON para que me convierta en "enum" el string que envia la request
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

    // Configuración de Swagger
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Registro de Servicios
    builder.Services.AddScoped<IUsuarioService, UsuarioService>();
    builder.Services.AddScoped<ITorneoService, TorneoService>();
    builder.Services.AddScoped<ITorneoJugadorService, TorneoJugadorService>();
    builder.Services.AddScoped<ITorneoJuezService, TorneoJuezService>();
    builder.Services.AddScoped<IJuegoService, JuegoService>();
    builder.Services.AddScoped<JwtService>();

    // Configuración de JWT
    var jwtConfig = builder.Configuration.GetSection("JWT").Get<JWTConfig>();
    builder.Services.Configure<JWTConfig>(builder.Configuration.GetSection("JWT"));


    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(Options =>
    {
        Options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfig.Issuer,
            ValidAudience = jwtConfig.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret)),
            RoleClaimType = ClaimTypes.Role
        };
    });
    // Registro de DAO Usuarios
    builder.Services.AddSingleton<IDAOUsuarios>(sp =>
    {
        var configuration = sp.GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("La cadena de conexión no está configurada");
        }
        return new DAOUsuarios(connectionString);
    });

    //Registro de DAO Torneos
    builder.Services.AddScoped<ITorneoDAO>(sp =>
    {
        var config = sp.GetRequiredService<IConfiguration>();
        var connectionString = config.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("La cadena de conexión no está configurada");
        }
        return new TorneoDAO(connectionString);
    });

    // Registro de DAO TorneoJueces
    builder.Services.AddScoped<IDAOTorneoJuez>(sp =>
    {
        var configuration = sp.GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("La cadena de conexión no está configurada");
        }
        return new DAOTorneoJuez(connectionString);
    });

    // Registro del DAO TorneoJugador
    builder.Services.AddScoped<IDAOTorneoJugador>(sp =>
    {
        var configuration = sp.GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("La cadena de conexión no está configurada");
        }
        return new DAOTorneoJugador(connectionString);
    });
    // Registro de DAOJuego 
    builder.Services.AddScoped<IDAOJuego>(sp =>
    {
        var configuration = sp.GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("La cadena de conexión 'DefaultConnection' no está configurada en appsettings.json.");
        }

        return new DAOJuego(connectionString);
    });


}


void ConfigureMiddleware(WebApplication app)
{
    //if (app.Environment.IsDevelopment())
    //{
    //    app.UseSwagger();
    //    app.UseSwaggerUI();
    //}

    app.UseHttpsRedirection();
    app.UseAuthentication(); // Habilitar autenticación
    app.UseAuthorization(); // Habilitar autorización
    app.MapControllers(); // Mapear controladores
}

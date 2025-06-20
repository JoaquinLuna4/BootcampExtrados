using Configuracion;
using LibraryTrabajoFinal.DAOS;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using TrabajoFinal.Servicios;

var builder = WebApplication.CreateBuilder(args);

//Configuraci�n de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://localhost:5173") // <-- Url del frontend
                                                  // Para permitir m�ltiples or�genes: .WithOrigins("http://localhost:5173", "http://otrodominio.com")
                                                  // Para permitir CUALQUIER origen (NO RECOMENDADO PARA PRODUCCI�N): .AllowAnyOrigin()
            .AllowAnyMethod()                     // Permite todos los m�todos HTTP (GET, POST, PUT, DELETE, OPTIONS, etc.)
            .AllowAnyHeader()                     // Permite todos los encabezados HTTP en las solicitudes
            .AllowCredentials());                   // Permite el env�o de credenciales (cookies, encabezados de autorizaci�n, etc.)
                                                    // Esto es crucial si est�s usando autenticaci�n basada en cookies o si pasas tokens de forma personalizada.
});


// Configuraci�n de Servicios
ConfigureServices(builder);

var app = builder.Build();

// Configuraci�n del Pipeline de Middleware
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

    // Configuraci�n de Swagger
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Registro de Servicios
    builder.Services.AddScoped<IUsuarioService, UsuarioService>();
    builder.Services.AddScoped<ITorneoService, TorneoService>();
    builder.Services.AddScoped<ITorneoJugadorService, TorneoJugadorService>();
    builder.Services.AddScoped<ITorneoJuezService, TorneoJuezService>();
    builder.Services.AddScoped<IJuegoService, JuegoService>();
    builder.Services.AddScoped<ICartaService, CartaService>();
    builder.Services.AddScoped<ISerieService, SerieService>();


    builder.Services.AddScoped<JwtService>();

    // Configuraci�n de JWT
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
            throw new InvalidOperationException("La cadena de conexi�n no est� configurada");
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
            throw new InvalidOperationException("La cadena de conexi�n no est� configurada");
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
            throw new InvalidOperationException("La cadena de conexi�n no est� configurada");
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
            throw new InvalidOperationException("La cadena de conexi�n no est� configurada");
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
            throw new InvalidOperationException("La cadena de conexi�n 'DefaultConnection' no est� configurada en appsettings.json.");
        }

        return new DAOJuego(connectionString);
    });
    // Registro del DAO y servicio de Cartas
    builder.Services.AddScoped<IDAOCarta>(sp =>
    {
        var configuration = sp.GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("La cadena de conexi�n 'DefaultConnection' no est� configurada en appsettings.json.");
        }
        return new DAOCarta(connectionString);
    });

    // Registro del DAO y servicio de Series
    builder.Services.AddScoped<IDAOSeries>(sp =>
    {
        var configuration = sp.GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("La cadena de conexi�n 'DefaultConnection' no est� configurada en appsettings.json.");
        }
        return new DAOSeries(connectionString);
    });
    //Registro de DAO TorneoMazo
    builder.Services.AddScoped<IDAOTorneoMazo>(sp =>
    {
        var config = sp.GetRequiredService<IConfiguration>();
        var connectionString = config.GetConnectionString("DefaultConnection");
        var logger = sp.GetRequiredService<ILogger<DAOTorneoMazo>>();
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("La cadena de conexi�n no est� configurada");
        }
        return new DAOTorneoMazo(connectionString, logger);
    });

}


void ConfigureMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("AllowSpecificOrigin"); //Habilitamos el uso de CORS
    app.UseHttpsRedirection();
    app.UseAuthentication(); // Habilitar autenticaci�n
    app.UseAuthorization(); // Habilitar autorizaci�n
    app.MapControllers(); // Mapear controladores
}

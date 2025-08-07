# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia los archivos de proyecto y de la solución primero
# Esto permite que Docker use el caché de capas para la restauración
COPY ["TrabajoFinal/TrabajoFinal.csproj", "TrabajoFinal/"]
COPY ["LibraryTrabajoFinal/LibraryTrabajoFinal.csproj", "LibraryTrabajoFinal/"]
COPY ["ExtradosBackend.sln", "."]

# Restaura las dependencias de la solución
RUN dotnet restore "ExtradosBackend.sln"

# Copia el resto de los archivos de la solución
COPY . .

# Cambia el directorio de trabajo al proyecto principal
WORKDIR "/src/TrabajoFinal"

# Compila la aplicación en modo Release
RUN dotnet build "TrabajoFinal.csproj" -c Release -o /app_build/

# Etapa de publicación
FROM build AS publish
RUN dotnet publish "TrabajoFinal.csproj" -c Release -o /app_publish/ /p:UseAppHost=false

# Etapa final de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia los archivos publicados
COPY --from=publish /app_publish/ .

# Define el punto de entrada
ENTRYPOINT ["dotnet", "TrabajoFinal.dll"]

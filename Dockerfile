FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY src/ .

WORKDIR Backend/WebApplication2

RUN dotnet restore 
RUN dotnet publish -c Release -o /app/out

# Imagem final (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Instala dependência necessária para Kerberos/GSSAPI
RUN apt-get update && apt-get install -y libkrb5-3 libgssapi-krb5-2 && rm -rf /var/lib/apt/lists/*


COPY --from=build-env /app/out .

ENTRYPOINT [ "dotnet", "Tropical.API.dll" ]

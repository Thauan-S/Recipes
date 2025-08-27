FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# copia tudo que tá na minha pasta src pra /app do container
# origem / destino
# minha api , meu aplication , infra , tá tudo em src
COPY src/ .

# minha api tá dentro de Backend/WebApplication2
#pq minha api é o ponto de entrada da aplicação
# e preciso fazer um .net restore
WORKDIR Backend/WebApplication2

#ele executa o restore dentro da minha api que tá em WebApplication2
RUN dotnet restore 
# publica a api com código otimizado (RELEASE) !!!! Release Lê as configs do appsettings.production.json!!!!!
# e o resultado vai pra pasta raiz /out dentro do container
RUN dotnet publish -c Release -o /app/out

# Imagem final (runtime) , a imagem de cima foi apenas temporária para executar as dlls
 #agora essa é a final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Instala dependência necessária para Kerberos/GSSAPI
# acho q isso n é necessário, só no meu exemplo q deu bug
RUN apt-get update && apt-get install -y libkrb5-3 libgssapi-krb5-2 && rm -rf /var/lib/apt/lists/*
 
#origem / destino , o meu alias da imagem temporária criada acima e o meu destino no container
COPY --from=build-env /app/out .

# comando pra ser executado / nome da dll da api
#O "Tropical.API.dll" é: Por padrão, igual ao nome do projeto .csproj
ENTRYPOINT [ "dotnet", "Tropical.API.dll" ]

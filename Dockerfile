  
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Refugiados.BFF/Refugiados.BFF.csproj", "Refugiados.BFF/"]
RUN dotnet restore "Refugiados.BFF/Refugiados.BFF.csproj"
COPY ./Refugiados.BFF ./Refugiados.BFF
WORKDIR "/src/Refugiados.BFF"
RUN dotnet build "Refugiados.BFF.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Refugiados.BFF.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN useradd -m myappuser
USER myappuser

CMD ASPNETCORE_URLS="http://*:$PORT" dotnet Refugiados.BFF.dll
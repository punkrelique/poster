FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Poster.Api/Poster.Api.csproj", "Poster.Api/"]
COPY ["Poster.Application/Poster.Application.csproj", "Poster.Application/"]
COPY ["Poster.Infrastructure/Poster.Infrastructure.csproj", "Poster.Infrastructure/"]
COPY ["Poster.Domain/Poster.Domain.csproj", "Poster.Domain/"]
RUN dotnet restore "Poster.Api/Poster.Api.csproj"
COPY . .
WORKDIR "/src/Poster.Api"
RUN dotnet build "Poster.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Poster.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Poster.Api.dll"]

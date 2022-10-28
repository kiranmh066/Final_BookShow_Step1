#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["MovieAppCoreAPI/MovieAppCoreAPI.csproj", "MovieAppCoreAPI/"]
COPY ["BookShowBLL/BookShowBLL.csproj", "BookShowBLL/"]
COPY ["BookShowDAL/BookShowDAL.csproj", "BookShowDAL/"]
COPY ["BookShowEntity/BookShowEntity.csproj", "BookShowEntity/"]
RUN dotnet restore "MovieAppCoreAPI/MovieAppCoreAPI.csproj"
COPY . .
WORKDIR "/src/MovieAppCoreAPI"
RUN dotnet build "MovieAppCoreAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MovieAppCoreAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieAppCoreAPI.dll"]
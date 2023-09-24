FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ITranslateTrainer.WebUI/ITranslateTrainer.WebUI.csproj", "ITranslateTrainer.WebUI/"]
RUN dotnet restore "src/ITranslateTrainer.WebUI/ITranslateTrainer.WebUI.csproj"
COPY . .
WORKDIR "/src/ITranslateTrainer.WebUI"
RUN dotnet build "ITranslateTrainer.WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ITranslateTrainer.WebUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ITranslateTrainer.WebUI.dll"]

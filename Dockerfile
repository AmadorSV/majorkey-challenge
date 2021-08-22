FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine as Builder

COPY . .

RUN mkdir app

RUN dotnet restore ./src/RESTApi
RUN dotnet build ./src/RESTApi --no-restore -c Release
RUN dotnet publish ./src/RESTApi --no-restore --no-build -c Release -o ./app


FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine as Host

COPY --from=Builder /app/ .
EXPOSE 80
ENTRYPOINT [ "dotnet","RESTApi.dll"]
dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\WebApiCore31WithSwagger.pfx -p "WebApiCore31WithSwagger"
dotnet dev-certs https --trust

docker run --rm -it -p 5000:80 -p 5003:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=5003 -e ASPNETCORE_Kestrel__Certificates__Default__Password="AuthCore_II" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/AuthCore_II.pfx -v %USERPROFILE%\.aspnet\https:/https/ WebApiCore31WithSwagger:dev
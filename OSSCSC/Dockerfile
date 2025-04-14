#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat 

FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8-windowsservercore-ltsc2019
ARG source
WORKDIR /inetpub/wwwroot

# Create a C:\Log directory for logging purposes
RUN powershell -Command New-Item -Path C:\Log -ItemType Directory -Force
# Set permissions for IIS_IUSRS to have full access to C:\Log
RUN powershell -Command icacls "C:\Log" /grant "IIS_IUSRS:(OI)(CI)F" /T

COPY ${source:-PublishedWebSite/_PublishedWebsites/OSSCSC} .

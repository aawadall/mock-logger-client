FROM microsoft/dotnet

COPY . /app 

WORKDIR /app 

RUN dotnet restore 

ENTRYPOINT ["dotnet", "run"]
# 이 파일은 https://docs.docker.com/engine/examples/dotnetcore/#create-a-dockerfile-for-an-aspnet-core-application 에 있는 것.

#
# build-evn 환경에서 빌드
#
FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

#
# build-env에서 빌드된 것들을 런타임 이미지로 옮김.
# (.dockerignore 파일도 참고하자. bin\ 과 obj\ 폴더는 제외된 상태로 이미지를 만든다. )
# 
FROM microsoft/dotnet:2.1.6-runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "HelloAkkaNetCore.dll"]
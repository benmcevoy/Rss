msbuild rss.sln /p:Configuration=Debug /p:DeployOnBuild=true /p:PublishProfile=default

# dotnet publish really does not work... publish profile is not found, app_data is not excluded, 
# perhaps read the source for that tool, it's just a wrapper on msbuild
msbuild .\src\rss.api\rss.api.csproj /p:Configuration=Debug /p:DeployOnBuild=true /p:PublishProfile=FolderProfile

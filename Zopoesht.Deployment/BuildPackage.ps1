Remove-Item -Recurse -ErrorAction Ignore -Force .\build
Remove-Item -Recurse -ErrorAction Ignore -Force .\package

cd (resolve-path ..\WebApplication).path
npm install
ng build --configuration production
cd (resolve-path ..\Zopoesht.Deployment).path

cd (Resolve-Path ..\Zopoesht.Hosting).Path
dotnet publish -c release -o ..\Zopoesht.Deployment\build /p:WebPublishMethod=Package /p:PackageAsSingleFile=true /p:DesktopBuildPackageLocation="..\Zopoesht.Deployment\package\Zopoesht.zip"
cd (Resolve-Path ..\Zopoesht.Deployment).Path
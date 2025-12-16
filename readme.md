# ClashOfClaps Server 

ClashOfClaps is an application to measure the volume of applause via an API and keep track of scores for teams.
The teams can be configured dynamically using a configuration file.

## Setup

Useful commands for quick setup on a linux machine

### Get the repository

Create a new directory and execute the following command.

```
git clone https://github.com/Ic3Fighter/ClashOfClaps.git
```

### Publish the app

After cloning, you need to publish the dotnet app, which you will the dotnet sdk for.
You can get it by following the [Microsoft instructions](https://learn.microsoft.com/en-us/dotnet/core/install/linux-debian).
To build and deploy a dotnet app on linux, you can follow [this YouTube video](https://www.youtube.com/watch?v=nQWpA5UZBXk) that explains all the commands in short.
Once you have the dotnet sdk installed, you can execute the following command to publish the app to the target directory.

```
sudo dotnet publish ./ClashOfClaps.Presentation/ClashOfClaps.Presentation.csproj -o /var/www/ -c Release
```

### Start the server

Change to the output directory and start the server using

```
dotnet ClashOfClaps.Presentation.dll
```

Note: By default, the server will only start on localhost.
To specify a url, you the following command.

```
dotnet ClashOfClaps.Presentation.dll --urls http://0.0.0.0:5000
```

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
Start by creating a new directory if it does not exist already.

```
sudo mkdir /srv/ClashOfClaps
sudo chown yourusername /srv/ClashOfClaps
```

To build and deploy a dotnet app on linux, you can follow [this YouTube video](https://www.youtube.com/watch?v=nQWpA5UZBXk) that explains all the commands in short.
Once you have the dotnet sdk installed, you can execute the following command to publish the app to the target directory.

```
sudo dotnet publish ./ClashOfClaps.Presentation/ClashOfClaps.Presentation.csproj -o /srv/ClashOfClaps -c Release
```

### Setup the service on linux

Copy the service description file contained in the repository to the systemd path in linux.

```
sudo cp ClashOfClaps.service /etc/systemd/system/ClashOfClaps.service
```

Reload the service daemon and start or enable the ClashOfClaps service.
Enabling will configure the service for auto-start when the machine reboots.

```
sudo systemctl daemon-reload
sudo systemctl enable ClashOfClaps
sudo systemctl start ClashOfClaps
```

After starting the service, it should be active and running.
Check status with the status command.

```
sudo systemctl status ClashOfClaps
```

### Get logging messages from the service

Use `journalctl` to see logging messages by the service.

```
sudo journalctl -u ClashOfClaps
```

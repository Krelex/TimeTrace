# Inital Projcet setup

### Prerequisites

- **Docker** - [Install guide](https://docs.docker.com/docker-for-windows/install/)
- **Keycloak** - [Getting started guide](https://www.keycloak.org/getting-started/getting-started-docker)

- **SQL Server** - [Download and setup](https://hub.docker.com/_/microsoft-mssql-server)

1. Turn on File Sharing on Docker
	- `Settings` -> `Resource` -> `File Sharing`
	- Add `C:\docker` and *Apply & Restart*

2. From a terminal(CMD) start SQL Server with the following command:

```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=NewSql123" -e "MSSQL_AGENT_ENABLED=True" -e "MSSQL_COLLATION=Croatian_CI_AS" -e "TZ=Europe/Zagreb" -p 1433:1433 --name sqldev --hostname sqldev --restart always -v C:\Docker\sql_data\data:/var/opt/mssql/data -v C:\Docker\sql_data\log:/var/opt/mssql/log -v C:\Docker\sql_data\secrets:/var/opt/mssql/secrets -d mcr.microsoft.com/mssql/server:latest
```
3. Add `Keycloak` database to new SQL Server instance

- **Keycloak** = [Download and setup](https://hub.docker.com/r/jboss/keycloak/)

1. Create docker internal network for Keycloak

```
docker network create keycloak-network
```

2. From a terminal(CMD) start Keycloak with the following command:

```
docker run --name TimeTrace-Keycloak --net keycloak-network -p 8080:8080 -e DB_VENDOR=mssql -e DB_USER=sa -e DB_PASSWORD=NewSql123 -e DB_ADDR=host.docker.internal -e DB_DATABASE=Keycloak -e KEYCLOAK_USER=admin -e KEYCLOAK_PASSWORD=admin -e PROXY_ADDRESS_FORWARDING=true jboss/keycloak:latest
```
if you get *java.lang.RuntimeException: Failed to connect to database* got to Keycloak database and allow all permissions (needed to allow 'keycloak' user in SQL Server instance to logon remotely (meaning not just from localhost but any other hosts))

- **Application**

1. Build docker image from soruce
	- Position into `~\TimeTrace` directory
	- run command (put your path to Dockerfile) :
	```
	docker build -t time-trace-app:0.1 -f "C:\Projects\TimeTrace\TimeTrace\TimeTraceMVC\Dockerfile" .
	```
2. Put image into Docker container
	- run command :
	```
	docker run -d -p 8001:80 --restart always --name time-trace-app time-trace-app:0.1
	```
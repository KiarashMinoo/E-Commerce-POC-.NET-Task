# E-Commerce POC .NET Task

#For Creating Migration Use Command Below
  Add-Migration InitialCreate -project Infrastructure 
  Update-Database -project Infrastructure 

#E-Commerce POC

###Used frameworks:
* .Net Core version 6
* React
* lua(For Nginx)

###Used databases:
* PostgreSql
* MongoDb

###Used components:
* EntityFramework
* Mongo Driver
* Mediatr
* Swagger
* Webpack
* Yarn
* Serilog
* EasyUi

###Used docker images:
* bitnami/postgresql:latest
* edoburu/pgbouncer:latest
* bitnami/mongodb:latest
* docker.elastic.co/elasticsearch/elasticsearch:8.4.3     
* docker.elastic.co/kibana/kibana:8.4.3
* openresty/openresty

#Running the app
**Executions with docker and run normally are allowed except in running normally you should run docker compose to start all required 
containers; however, if you use run.sh/bat project would be scale to 3 nodes and openresty nginx is responsible to load balancing requests.**

#POC
**In this scenario, I used CQRS(Mediatr) to executing requests; however, on my opinion to synchronize databases (SQL and NoSQL) publishing notifications as 
DomainEvents was the best solution. Contexts are different and also depends on it using difference Repositories are inevitable. So, after commands complete 
successful post processor pipeline of Mediatr publish DomainEvents and handlers updating MongoDb side data. Commands and Queries are working independently 
and using required services perfectly. Somehow in a few cases syncing data are not going to done perfectly; In these cases, using Quartz and synching data are suggested.
**
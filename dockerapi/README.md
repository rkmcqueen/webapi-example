# ASP.Net Core Web API
A simple ASP.NET Core sample web api using PostgreSQL with Swagger support. Hosted in docker.

## Prerequisites
1. [Docker](https://www.docker.com/)

## Steps
1. `git clone https://github.com/rkmcqueen/webapi-example.git`

2. `cd webapi-example/dockerapi`

3. `docker-compose build`

4. `docker-compose up`

5.  Navigate to http://localhost:8000/swagger

To clear database run `docker-compose down -v` before re-running
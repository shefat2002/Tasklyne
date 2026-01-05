# Tasklyne

A collaborative Task and Project Management System.



### Docker Image of SQL Server
To download and run the Docker image of SQL Server, you can use the following command:

* For Mac with Apple Silicon
```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<GENERATE A STRONG PASSWORD>" -p 1433:1433 --name sql_server -d mcr.microsoft.com/mssql/server:2022-latest
```

* For Windows/linux or Mac with Intel chip
```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<GENERATE A STRONG PASSWORD>" -p 1433:1433 --name sql_server -d mcr.microsoft.com/mssql/server:2022-latest
```

** Note:** Make sure to replace `<GENERATE A STRONG PASSWORD>` with a strong password of your choice.

### Database Setup

# ApiTpl

ApiTpl is a quick start template for simple .net core api project.

# How to use?

## Install



```shell
# Uninstall the old template
dotnet new --uninstall CW.ApiTpl

# Install the new template
dotnet new --install CW.ApiTpl
```


## Help

```shell
# Show the help message
dotnet new apitpl -h

CW.ApiTpl (C#)
Author: Catcher Wong
Options:
  -s|--sqlType  The type of SQL to use
                    Ms        - MS SQL Server
                    My        - MySQL
                    Pg        - PostgreSQL
                    SQLite    - SQLite
                Default: Ms
```

## Create

```shell
# Create a sample named demo with MySql database
dotnet new apitpl -n demo -s My
```

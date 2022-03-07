# ATM

![Project](ATM/Content/Images/UML.png)

---

## How to run to the project

- Open project using visual studio 2015/2019
- Go to Solution Explorer, click to show Files icon
- I would try giving the database a different name. Go to **web.config** file and change database name in connection string.

```xml
  <connectionStrings>
    <add name="DefaultConnection" connectionString="Data Source=(LocalDb)\MSSQLLocalDB;
    AttachDbFilename=|DataDirectory|\aspnet-ATM-20211230115937.mdf;
    Initial Catalog=aspnet-ATM-20211230115937;Integrated Security=True"
      providerName="System.Data.SqlClient" />
  </connectionStrings>

```

- Go to App_Data Folder, right click **add** > **new item** and create a mdf file for database name.
- Go to Package Manager Console in Visual Studio and type:

```shell
enable-migrations -force
add-migration init
update-database
```

## Thanks for checking out my project

- Then click **ctrl+f5** to run your project locally.

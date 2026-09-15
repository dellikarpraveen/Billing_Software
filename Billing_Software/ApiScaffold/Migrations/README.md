This folder will contain EF Core migrations created with dotnet-ef.
Create the first migration with:

  dotnet tool install --global dotnet-ef --version 8.0.0
  dotnet ef migrations add InitialCreate --project Billing_Software/ApiScaffold --startup-project Billing_Software/ApiScaffold

Apply migrations with:
  dotnet ef database update --project Billing_Software/ApiScaffold --startup-project Billing_Software/ApiScaffold

If you prefer LocalDB the default connection string used is: Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BillingApiDb;Integrated Security=True;


https://docs.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=visual-studio

In the package manager console
Check this project is selected.
create the database

Install-Package Microsoft.EntityFrameworkCore.Tools
Add-Migration InitialCreate
Update-Database

Guids - for some reason microsoft stores guids as TEXT and the linq provider does not work
I manually updated to BLOB for those columns.


update database with new migrations
PM> Update-Database


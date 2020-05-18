# Memento.Movies

Web Application implemented using ASP.NET MVC Core, ASP.NET Identity Core, Entity Framework Core and Blazor created as a learning project with the goal of implementing a Web App.

## Prerequisites

To deploy this application it is assumed that the following files exists:

* secrets.json

This file can be accessed from Visual Studio > Project > Manage Secrets.
It is located in a folder following this pattern: "C:\Users\\{user}\AppData\Roaming\Microsoft\UserSecrets\\{guid}\secrets.json"

It should follow this template:

### secrets.json

```json
{
	"AppSettings":
	{
	},
	"ConnectionStrings":
	{
	}
}
```
# Memento.Movies

Memento.Movies is a web-application using ASP.NET Core Blazor Server and Blazor WebAssembly created as a learning project following [Felipe Gaviláns'](https://github.com/gavilanch) Udemy course as a base line.

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
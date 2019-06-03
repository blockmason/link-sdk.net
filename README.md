# Blockmason Link SDK for .NET

[![CircleCI][1]][2]

## Installing

To add this library to your app, do one of the following:

Add the following [PackageReference][4] to your [project file][5]:

```
<PackageReference Include="Blockmason.Link" Version="1.0.0"/>
```

Or, if you prefer to use [NuGet][3] directly:

```
nuget install Blockmason.Link -Version 1.0.0
```

## Usage

First, your app should import the `Blockmason.Link` namespace:

```
using Blockmason.Link;
```

This namespace provides a `Project` class, which you can use to
initialize your project like this:

```
Project project = await Project.Create("<your-client-id>", "<your-client-secret>");
``` 

Use the **Client ID** and **Client Secret** provided by your Link project
to fill in the respective values above.

Then, you can use the `project` object to make requests against your
Link project.

For example, assuming your project has a **GET /echo** endpoint that
expects a `message` input and responds with a `message` output:

```
Dictionary<string, string> outputs = await project.Get<Dictionary<string, string>>("/echo", new {
  message = "Hello, world!"
});

Console.WriteLine(outputs["message"]);
// "Hello, world!"
```

Another example, assuming your project has a **POST /mint** endpoint
that expects `to` and `amount` inputs:

```
await project.Post<object>("/mint", new {
  amount = 1000,
  to = "0x1111222233334444555566667777888899990000"
});
```

[1]: https://circleci.com/gh/blockmason/link-sdk.net.svg?style=svg
[2]: https://circleci.com/gh/blockmason/link-sdk.net
[3]: https://www.nuget.org/
[4]: https://docs.microsoft.com/en-us/nuget/consume-packages/package-references-in-project-files
[5]: https://docs.microsoft.com/en-us/dotnet/core/tools/?tabs=netcore2x

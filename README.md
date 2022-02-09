# ASP.NET Core MVC Modular Monolith Demo
A modular monolith is a monolith, but the term monolith refers more to the hosting/runtime model. All services/parts live in the same solution (not in the same project), are running in the same process, and are therefore deployed at the same time. But, each service/part is located in its own module (.NET project) and is therefore decoupled from other modules.

## Getting Started

- Install [.NET Core 3.1 SDK](https://www.microsoft.com/net/download/all)

- Build the whole solution.
- In Solution Explorer, make sure that Demo.WebHost is selected as the Startup Project.
- In Visual Studio, press "Control + F5".

## Create Modules

- Add new `ASP.NET Core Web Application` > choose `MVC Project`.
- Naming the module as `xxx.Module.yyy`, the location should be in `src\Modules`.
- After the project has created. Right click on project name choose `Properties` from menu.
- Focus on `Application` tab and change `Output Type` from `Console Application` to `Class Library`.
- Remove **Program.cs, Startup.cs, HomeController.cs**, folder inside **View** folder and remove files inside subfolder of **wwwroot** folder.
- Add a new file call **module.json** with the following structure

```
{
  "id": "xxx.Modules.yyy",
  "name": "yyy",
  "isBundledWithHost": true,
  "version": "1.0.0"
}

```

If you set `isBundledWithHost` to true. Then you need to add project reference to your module in the **WebHost** project.

Register your module in **Demo.WebHost/modules.json** so that it will be copied to the **WebHost** on build

- Add a new file call **ModuleInitializer.cs** with the following structure

```
public class ModuleInitializer : IModuleInitializer
{
   public void ConfigureServices(IServiceCollection serviceCollection)
   {
            
   }
   
   public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
   {
            
   }  
}

```

This file is the place where you will register services and middlewares of your module

## Overview

### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### WebHost

 The ASP.NET Core project and it will act as the host. It will bootstrap the app and load all the modules it found in the `Modules` folder.
 In order to prevent `WebHost` to compile stuff in `Modules` folder, we need to exclude them in the `modules.json`.
 
### Modules > Module1 | Module2

`Module1` and `Module2` are logical views of different functional modules. Each module consists of one or more assemblies that contain the logic of a module and all the stuff for itself to run including Controllers, Models, Views and event static files.
If a module needs to share logic with other modules, it should provide interfaces in a `Infrastructure` assembly.

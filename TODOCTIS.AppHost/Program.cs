var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.API_TODO>("api-todo");

builder.Build().Run();

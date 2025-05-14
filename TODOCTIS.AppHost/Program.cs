var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.API_TODO>("api-todo");

builder.AddProject<Projects.API_Users>("api-users");

builder.AddProject<Projects.API_Gateway>("api-gateway");

builder.Build().Run();

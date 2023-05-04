using Subgraph;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(static sp =>
{
    var connectionString = "localhost:6379";
    ArgumentNullException.ThrowIfNull(connectionString);
    return ConnectionMultiplexer.Connect(connectionString);
});

builder.Services.AddHttpClient("SubgraphTest", c => c.BaseAddress = new Uri("http://localhost:7101/graphql"));

builder.Services
    .AddGraphQLServer()
    .AddType(new AnyType("CardNumberType"))
    .AddRemoteSchemasFromRedis("SubgraphTestSchemaKey",
        sp => sp.GetRequiredService<ConnectionMultiplexer>())
    .InitializeOnStartup();

var app = builder.Build();

app.MapGraphQL();

app.Run();

using Subgraph;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(static sp =>
{
    var connectionString = "localhost:6379";
    ArgumentNullException.ThrowIfNull(connectionString);
    return ConnectionMultiplexer.Connect(connectionString);
});

builder.Services
    .AddGraphQLServer()
    .PublishSchemaDefinition(c =>
                c.SetName("SubgraphTest")
                    .PublishToRedis("SubgraphTestSchemaKey",
                        sp => sp.GetRequiredService<ConnectionMultiplexer>()))
    .AddType<CardNumberType>()
    .AddQueryType<CardDataQuery>()
    .InitializeOnStartup();

var app = builder.Build();

app.MapGraphQL();

app.Run();

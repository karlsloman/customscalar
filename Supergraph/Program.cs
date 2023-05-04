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
    .AddType<CardNumberType>()
    //.AddType(new AnyType("CardNumberType"))
    //.BindRuntimeType(typeof(string), "CardNumberType")
    .AddRemoteSchemasFromRedis("SubgraphTestSchemaKey",
        sp => sp.GetRequiredService<ConnectionMultiplexer>())
    .InitializeOnStartup();


var app = builder.Build();

app.MapGraphQL();

app.Run();

# GraphQL Custom Scalar
This solution replicates a problem with using custom scalar's when using remote schemas
## Replication
Launch a Redis cache container into docker\
`docker run -p 6379:6379 --name redis -d redis`

Run up the subgraph project\
`dotnet run --project Subgraph/Subgraph.csproj`

This should publish the schema into the Redis cache\
Then in another terminal run the Supergraph\
`dotnet run --project Supergraph/Supergraph.csproj`

You should see an error output:
```
fail: Microsoft.Extensions.Hosting.Internal.Host[9]
      BackgroundService failed
      HotChocolate.SchemaException: For more details look at the `Errors` property.

      1. Unable to resolve type reference `CardNumberType`. (HotChocolate.Types.ObjectType)
```

## Banana Cake Pop
You can see it working just from the Subgraph\
`http://localhost:7101/graphql`

If you do get the Supergraph working\
`http://localhost:7100/graphql`

Success response:
```
{
  cardData(cardNumber: "1234567890123456") {
    cardNumber
    title
  }
}
```

Failure response:
```
{
  cardData(cardNumber: "abc") {
    cardNumber
    title
  }
}
```

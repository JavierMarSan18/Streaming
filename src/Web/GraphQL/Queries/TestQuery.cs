namespace Netflixs2.Web.GraphQL;

[QueryType]
public class TestQuery
{
    public string Test() => new ("Hello, World!");
}
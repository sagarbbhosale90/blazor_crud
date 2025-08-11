namespace BlazorApp_Crud.Model
{
    public class GraphQLResponse<T>
    {
        public T[]? Data { get; set; }
        public GraphQLError[]? Errors { get; set; }
    }

    public class GraphQLError
    {
        public string? Message { get; set; }
    }

    public class MyResponseType
    {

    }
}

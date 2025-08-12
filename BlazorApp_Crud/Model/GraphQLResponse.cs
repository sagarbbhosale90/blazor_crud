using Newtonsoft.Json.Linq;

namespace BlazorApp_Crud.Model
{
    public class GraphQLResult<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; } = [];
    }

    public static class GraphQLResult
    {
        public static GraphQLResult<T> HandleGraphQLResponse<T>(string json, string dataField)
        {
            try
            {
                var result = new GraphQLResult<T>();

                JObject obj = JObject.Parse(json);

                if (obj["errors"] != null)
                {
                    result.Success = false;
                    foreach (var err in obj["errors"])
                        result.Errors.Add(err["message"]?.ToString());
                }
                else if (obj["data"]?[dataField] != null)
                {
                    var token = obj["data"][dataField];
                    result.Data = token.ToObject<T>(); // parse JSON into requested type
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.Errors.Add("No matching data or errors found in response.");
                }

                return result;
            }
            catch (Exception ex)
            {
                return new GraphQLResult<T>
                {
                    Success = false,
                    Errors = [ex.Message]
                };
            }
        }
    }   
}

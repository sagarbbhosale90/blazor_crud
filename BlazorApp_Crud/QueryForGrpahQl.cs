namespace BlazorApp_Crud
{
    public static class QueryForGrpahQl
    {
        public static object GetAllProduct = new
        {
            query = @"{
                getAllProducts {
                    id
                    name
                    price
                    description
                }
            }"
        };
    }
}

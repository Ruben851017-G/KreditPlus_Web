namespace KreditPlus_Web.Models
{
    public static class SD
    {
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE,
            PATCH
        }
        public static string SessionToken = "JWTToken";
    }
}

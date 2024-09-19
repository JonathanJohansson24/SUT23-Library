namespace WebLibrary
{
    public class StaticDetails
    {
        public static string LibraryApiBase { get; set; } = "https://localhost:7124";

        public enum ApiType
        {
            GET, POST, PUT, DELETE
        }
    }
}

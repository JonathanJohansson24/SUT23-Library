namespace LibraryMinimalApi.Models.DTOs
{
    public class BookDTO
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; } // Genre as string
        public int PublicationYear { get; set; }
        public bool IsAvailable { get; set; }
    }
}

using LibraryMinimalApi.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace LibraryMinimalApi.Models
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }


        [Required(ErrorMessage = "Please enter Booktitle")]
        [Display(Name = "Book title")]
        [StringLength(50)]
        public string Title { get; set; }


        public string? Description { get; set; }


        [Required(ErrorMessage = "Please enter the Author")]
        [Display(Name = "Author")]
        [StringLength(100)]
        public string Author { get; set; }

        [Required(ErrorMessage = "Please choose a genre")]
        public BookGenre Genre { get; set; }


        [Required(ErrorMessage = "Please enter the publication year")]
        [Range(1000, 3000, ErrorMessage = "Please enter a valid year")]
        public int PublicationYear { get; set; }

        public bool IsAvailable { get; set; } 


    }
}

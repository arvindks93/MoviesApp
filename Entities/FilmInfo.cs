using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Entities
{
    [Table("Filminfo")]
    public class FilmInfo
    {
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Rating { get; set; }
    }
}

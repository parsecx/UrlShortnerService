using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kursovaya.Entities
{
    public class UrlModel
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; } = null!;
        public string ShortUrl { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public long AmountOfRedirections { get; set; } = 0;
        public bool IsEnalbed { get; set; } = true;
        public string AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public IdentityUser Author { get; set; }

    }
}

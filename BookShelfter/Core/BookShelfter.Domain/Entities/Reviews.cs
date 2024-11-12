using BookShelfter.Domain.Entities.Common;

namespace BookShelfter.Domain.Entities;

public class Reviews:BaseEntity
{
    public int BookID { get; set; }
    public string UserID { get; set; }

    public string UserName { get; set; }

    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime ReviewDate { get; set; }
    public Book Book { get; set; }
}
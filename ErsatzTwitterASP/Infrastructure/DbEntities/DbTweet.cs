namespace Infrastructure.DbEntities;

public partial class DbTweet
{ 
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public DateTime PostDate { get; set; }
    public int UserId { get; set; }
    public virtual DbUser DbUser { get; set; } = null!;
    public virtual ICollection<DbLike> Likes { get; set; } = new List<DbLike>();
}

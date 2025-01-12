namespace Infrastructure.DbEntities;

public partial class DbUser
{
    public int Id { get; set; }

    public string Pseudo { get; set; } = null!;

    public virtual ICollection<DbTweet> Tweets { get; set; } = new List<DbTweet>();

    public virtual ICollection<DbLike> Likes { get; set; } = new List<DbLike>();
}

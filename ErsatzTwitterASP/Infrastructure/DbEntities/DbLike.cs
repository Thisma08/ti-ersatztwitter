namespace Infrastructure.DbEntities;

public partial class DbLike
{
    public int UserId { get; set; }

    public int TweetId { get; set; }
    
    public virtual DbTweet DbTweet { get; set; } = null!;

    public virtual DbUser DbUser { get; set; } = null!;
}

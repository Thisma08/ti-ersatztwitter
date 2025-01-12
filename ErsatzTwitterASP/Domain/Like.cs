namespace Domain;

public partial class Like
{
    public int UserId { get; set; }

    public int TweetId { get; set; }
    
    public virtual Tweet Tweet { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

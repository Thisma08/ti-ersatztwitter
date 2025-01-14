namespace Domain;

public partial class Tweet
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public DateTime PostDate { get; set; }
    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
}

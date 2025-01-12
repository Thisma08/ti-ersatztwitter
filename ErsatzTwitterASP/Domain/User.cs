namespace Domain;

public partial class User
{
    public int Id { get; set; }

    public string Pseudo { get; set; } = null!;

    public virtual ICollection<Tweet> Musics { get; set; } = new List<Tweet>();

    public virtual ICollection<Like> Votes { get; set; } = new List<Like>();
}

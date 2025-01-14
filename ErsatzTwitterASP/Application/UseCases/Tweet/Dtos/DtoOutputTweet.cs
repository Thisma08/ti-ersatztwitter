namespace Application.UseCases.Tweet.Dtos;

public class DtoOutputTweet
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime PostDate { get; set; }
    public int UserId { get; set; }
    public int LikeCount { get; set; }
}
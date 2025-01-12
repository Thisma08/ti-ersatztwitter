using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Tweet.Dtos;

public class DtoInputTweet
{
    [StringLength(140, MinimumLength = 2)]
    [Required]
    public string Content { get; set; }
    [Required]
    public int UserId { get; set; }
}
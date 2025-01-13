namespace Domain.Exceptions;

public class UserDeleteOwnTweetException: Exception
{
    public UserDeleteOwnTweetException()
        : base("You can delete your own tweets only.")
    {
    }
}
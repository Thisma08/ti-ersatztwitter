using Domain.Exceptions;

namespace Domain.Services;

public class TweetService
{
    public void CheckIfDeletionIsValid(int tweetUserId, int connectedUserId)
    {
        if (tweetUserId != connectedUserId)
        {
            throw new UserDeleteOwnTweetException();
        }
    }
}
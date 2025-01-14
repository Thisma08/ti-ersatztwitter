using Domain.Exceptions;

namespace Domain.Services;

public interface TweetService
{
    public void CheckIfDeletionIsValid(int tweetUserId, int connectedUserId)
    {
        if (tweetUserId != connectedUserId)
        {
            throw new UserDeleteOwnTweetException();
        }
    }
}
using Domain.Exceptions;

namespace Domain.Services;

public class LikeService
{
    public void ValidateLike(bool likeExists)
    {
        if (likeExists)
        {
            throw new DuplicateLikeException();
        }
    }
}
namespace Domain.Exceptions;

public class DuplicateLikeException: Exception
{
    public DuplicateLikeException()
        : base("This like already exists.")
    {
    }
}
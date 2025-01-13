namespace Application.UseCases.Utils;

public interface IUseCaseQuery<TOutput>
{
    Task<TOutput> Execute();
}
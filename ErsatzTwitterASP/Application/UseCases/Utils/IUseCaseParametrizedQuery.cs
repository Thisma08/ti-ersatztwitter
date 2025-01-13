namespace Application.UseCases.Utils;

public interface IUseCaseParametrizedQuery<TOutput, in TParam>
{
    Task<TOutput> Execute(TParam param);
}
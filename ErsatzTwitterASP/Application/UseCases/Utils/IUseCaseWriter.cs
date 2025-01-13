namespace Application.UseCases.Utils;

public interface IUseCaseWriter<TOutput, in TInput>
{
    Task<TOutput> Execute(TInput input);
}
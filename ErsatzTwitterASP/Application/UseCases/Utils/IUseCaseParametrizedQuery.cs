namespace Application.UseCases.Utils;

public interface IUseCaseParametrizedQuery<out TOutput, in TParam>
{
    TOutput Execute(TParam param);
}
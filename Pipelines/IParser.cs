namespace PinaryDevelopment.Framework.Pipelines
{
    public interface IParser<TInput, TOutput>
    {
        TOutput Parse(TInput input);
    }
}

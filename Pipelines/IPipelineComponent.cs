namespace PinaryDevelopment.Framework.Pipelines
{
    using System;
    using System.Threading.Tasks;

    public interface IPipelineComponent<T>
    {
        Func<IPipelineComponent<T>> NextComponent { get; set; }

        PipelineDelegate<T> InvokeAsync { get; }
    }


    public class NoopComponent<T> : IPipelineComponent<T>
    {
        public Func<IPipelineComponent<T>> NextComponent { get; set; }

        public PipelineDelegate<T> InvokeAsync
        {
            get => t => Task.FromResult(t);
        }
    }
}

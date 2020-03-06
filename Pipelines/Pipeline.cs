/*
 * Reference Material
 * https://github.com/aspnet/AspNetCore/blob/4ef204e13b88c0734e0e94a1cc4c0ef05f40849e/src/Http/Http/src/Builder/ApplicationBuilder.cs
 */
namespace PinaryDevelopment.Framework.Pipelines
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Pipeline<TInput, T>
    {
        public virtual IList<Func<IPipelineComponent<T>>> Components { get; }

        public IServiceProvider ServiceProvider { get; }

        public IParser<TInput, T> Parser { get; set; }

        public Pipeline(IServiceProvider serviceProvider)
        {
            Components = new List<Func<IPipelineComponent<T>>>();
            ServiceProvider = serviceProvider;
        }

        public async Task<T> Process(TInput input)
        {
            var t = Parser.Parse(input);
            IPipelineComponent <T> firstComponent = null;
            IPipelineComponent<T> prevComponent = null;
            for (var i = Components.Count - 1; i >= 0; i--)
            {
                var currentComponent = Components[i]();
                var nextComponent = prevComponent ?? new NoopComponent<T>();
                currentComponent.NextComponent = () => nextComponent;

                if (i == 0)
                {
                    firstComponent = currentComponent;
                }

                prevComponent = currentComponent;
            }

            await firstComponent.InvokeAsync(t).ConfigureAwait(false);
            return t;
        }

        public Pipeline<TInput, T> AddParser<TParser>() where TParser : IParser<TInput, T>
        {
            Parser = (TParser)ServiceProvider.GetService(typeof(TParser));
            return this;
        }

        public Pipeline<TInput, T> Use<TComponent>() where TComponent : IPipelineComponent<T>
        {
            Components.Add(() => (TComponent)ServiceProvider.GetService(typeof(TComponent)));
            return this;
        }
    }
}

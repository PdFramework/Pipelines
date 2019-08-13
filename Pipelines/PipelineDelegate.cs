/*
 * Reference Material
 * https://github.com/aspnet/AspNetCore/blob/master/src/Http/Http.Abstractions/src/RequestDelegate.cs
 */
namespace PinaryDevelopment.Framework.Pipelines
{
    using System.Threading.Tasks;

    public delegate Task PipelineDelegate<T>(T t);
}

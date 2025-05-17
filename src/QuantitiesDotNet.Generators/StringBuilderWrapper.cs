using System.Text;

namespace QuantitiesDotNet.Generators
{
    internal class StringBuilderWrapper(StringBuilder sb, CancellationToken token)
    {
        public void Append(string text)
        {
            token.ThrowIfCancellationRequested();
            sb.Append(text);
        }

        public void AppendLine()
        {
            token.ThrowIfCancellationRequested();
            sb.AppendLine();
        }

        public void AppendLine(string text)
        {
            Append(text);
            AppendLine();
        }
    }

}

#pragma warning disable IDE0130
namespace System.Runtime.CompilerServices
#pragma warning restore IDE0130
{
#if !NET6_0_OR_GREATER
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    internal sealed class InterpolatedStringHandlerAttribute : Attribute
    {
    }
#endif
}
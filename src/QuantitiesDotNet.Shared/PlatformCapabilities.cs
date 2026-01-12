#if NETSTANDARD2_0 || NETSTANDARD2_1
namespace System.Runtime.CompilerServices
{
    internal class IsExternalInit
    {
    }
}
#endif

#if NETSTANDARD2_0

namespace System.Diagnostics.CodeAnalysis
{
    public class AllowNullAttribute : Attribute { }

    public class DisallowNullAttribute : Attribute { }

    public class MaybeNullAttribute : Attribute { }

    public class NotNullAttribute : Attribute { }

    public class MaybeNullWhenAttribute(bool returnValue) : Attribute
    {
        public bool ReturnValue => returnValue;
    }

    public class NotNullWhenAttribute(bool returnValue) : Attribute
    {
        public bool ReturnValue => returnValue;
    }

    public class DoesnotReturnAttribute : Attribute { }

    public class DoesNotReturnWhenAttribute(bool parameterValue) : Attribute
    {
        public bool ParameterValue => parameterValue;
    }

    public class MemberNotNullAttribute(string[] members) : Attribute
    {
        public string[] Members => members;

        public MemberNotNullAttribute(string member)
            : this([member])
        {
        }
    }

    public class MemberNotNullWhenAttribute(bool returnValue, string[] members) : Attribute
    {
        public bool ReturnValue => returnValue;
        public string[] Members => members;

        public MemberNotNullWhenAttribute(bool returnValue, string member)
            : this(returnValue, [member])
        {
        }
    }
}
#endif
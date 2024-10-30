#region Header

// 2024
// 10

#endregion

namespace WinUiAbpTemplate.Aop;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method, Inherited = true)]
public class LoggingInterceptorAttribute : Attribute
{
}
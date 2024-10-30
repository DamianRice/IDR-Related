using WinUiAbpTemplate.Aop;

namespace WinUiAbpTemplate.Application.Abstract;

[LoggingInterceptor]
public interface ITestService
{
    public string GetMessage();
    public string GetVersion();
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using WinUiAbpTemplate.Application.Abstract;

namespace WinUiAbpTemplate.Application;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true, TryRegister = true)]
public class TestService : ITransientDependency, ITestService
{
    // TODO: 自定义服务，Transient DI
    public string GetMessage()
    {
        return $@"Message from {nameof(TestService)} Service";
    }

    public string GetVersion() => $@"Version info from {nameof(TestService)} v1.0.0";
}
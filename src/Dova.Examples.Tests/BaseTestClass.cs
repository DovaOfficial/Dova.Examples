using Dova.InterfaceProviders.Moq;

namespace Dova.Examples.Tests;

public abstract class BaseTestClass
{
    protected void Setup()
    {
        MoqInterfaceObjectProvider.Setup();
    }
}
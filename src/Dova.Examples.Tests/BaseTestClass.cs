using System;
using Dova.Core;
using Dova.InterfaceProviders.Moq;

namespace Dova.Examples.Tests;

public abstract class BaseTestClass
{
    protected void Setup()
    {
        MoqInterfaceObjectProvider.Setup();

        try
        {
            DovaVM.Initialize(new DovaConfiguration());
        }
        catch (Exception e)
        {
        }
    }
}
using Dova.Core;
using Dova.Core.Runtime;
using Dova.JDK.Extensions;
using Dova.JDK.java.lang;
using Xunit;

namespace Dova.Examples.Tests;

public class ConsoleTests : BaseTestClass
{
    [Fact]
    public void Should_print_on_console_without_using_extensions()
    {
        Setup();

        var text = "Hello World from JVM using Dova.JDK";
        
        DovaVM.Initialize(new DovaConfiguration());

        var textPtr = DovaVM.Runtime.GetString(text);
        var str = new String(textPtr);
        
        JDK.java.lang.System.out_Property.println(str);
    }
    
    [Fact]
    public void Should_print_on_console_using_extensions()
    {
        Setup();

        DovaVM.Initialize(new DovaConfiguration());

        JDK.java.lang.System.out_Property.println("Hello World from JVM using Dova.JDK".ToJava());
    }
}
using Dova.Common;
using Dova.JDK.Extensions;
using Dova.JDK.java.io;
using Dova.JDK.java.lang;
using Dova.JDK.java.net;
using Xunit;

namespace Dova.Examples.Tests;

public class ExternalJarFileTests : BaseTestClass
{
    [Fact]
    public void Should_find_class_from_JAR_file()
    {
        const string ByteBuddyClassName = "net.bytebuddy.ByteBuddy";
        
        Setup();

        var jnaFile = new File("/home/<user>/Downloads/jna-5.12.1.jar".ToJava());
        var bbFile = new File("/home/<user>/Downloads/byte-buddy-1.12.13.jar".ToJava());

        var defaultValue = new URL(string.Empty.ToJava()).CurrentRefPtr;
        var filesArray = new JavaArray<URL>(2, URL.ClassPtr, defaultValue);
        filesArray[0] = jnaFile.toURI().toURL();
        filesArray[1] = bbFile.toURI().toURL();

        var classLoader = new URLClassLoader(filesArray);

        var clazz = Class.forName(ByteBuddyClassName.ToJava(), true, classLoader);
        var bb = clazz.newInstance();
        
        Assert.Equal(ByteBuddyClassName, bb.getClass().getName().ToCSharp());
    }
    
    [Fact]
    public void Should_create_new_Java_class_from_JAR_file()
    {
    }
}
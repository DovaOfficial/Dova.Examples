using System.Collections.Generic;
using System.IO;
using Dova.Common;
using Dova.Core;
using Dova.JDK.Extensions;
using Dova.JDK.java.lang;
using Dova.JDK.java.net;
using Dova.JDK.Loaders;
using Dova.JDK.net.bytebuddy;
using Dova.JDK.net.bytebuddy.implementation;
using Dova.JDK.net.bytebuddy.matcher;
using Xunit;
using File = Dova.JDK.java.io.File;

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
        const string PathToJars = "/home/<user>/.gradle/caches/modules-2/files-2.1/net.bytebuddy/byte-buddy/1.12.13/35ffee9c24b1c68b08d9207e1a2d3da1add6166";
        
        var config = new DovaConfiguration
        {
            JvmOptions = new List<string>
            {
                "-Djdk.attach.allowAttachSelf=true"
            }
        };
        
        DovaVM.Initialize(config);

        Setup();

        JarLoader.Load(
            new FileInfo($"{PathToJars}/byte-buddy-1.12.13.jar"), 
            new FileInfo($"{PathToJars}/jna-5.12.1.jar"));
        
        var bb = new ByteBuddy();
        var dynamicType = bb
            .subclass(new Class(Object.ClassPtr))
            .method(ElementMatchers.named("toString".ToJava()))
            .intercept(FixedValue.value("Hello World!".ToJava()))
            .make()
            .load(ClassLoader.getSystemClassLoader())
            .getLoaded();

        Assert.Equal(dynamicType.newInstance().toString().ToCSharp(), "Hello World!");
    }
}
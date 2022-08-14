using System.Linq;
using Dova.Common;
using Dova.Core;
using Dova.JDK.Extensions;
using Dova.JDK.java.lang;
using Xunit;

namespace Dova.Examples.Tests;

public class JavaArrayTests : BaseTestClass
{
    [Fact]
    public void Should_create_valid_array_with_primitive_values()
    {
        Setup();
        
        var array = new JavaArray<int>(10);

        for (int index = 0; index < 10; index++)
        {
            array[index] = index * 2;
        }
        
        for (int index = 0; index < 10; index++)
        {
            Assert.Equal(array[index], index * 2);
        }

        var array2 = new JavaArray<int>(array.CurrentRefPtr);
        
        for (int index = 0; index < 10; index++)
        {
            Assert.Equal(array[index], array2[index]);
        }
    }

    [Fact]
    public void Should_create_valid_array_with_objects()
    {
        Setup();

        var initialValue = new Integer(0);
        
        var array = new JavaArray<Integer>(10, Integer.ClassPtr, initialValue.CurrentRefPtr);

        for (int index = 0; index < 10; index++)
        {
            array[index] = new Integer(index * 2);
        }
        
        for (int index = 0; index < 10; index++)
        {
            Assert.Equal(array[index].value_Property, index * 2);
        }

        var array2 = new JavaArray<Integer>(array.CurrentRefPtr);
        
        for (int index = 0; index < 10; index++)
        {
            Assert.Equal(array[index].value_Property, array2[index].value_Property);
        }
    }

    [Fact]
    public void Should_create_valid_array_with_Java_strings()
    {
        Setup();

        var array = new JavaArray<String>(10, String.ClassPtr, string.Empty.ToJava().CurrentRefPtr);

        for (int index = 0; index < 10; index++)
        {
            array[index] = $"Sample_String_With_Value_{index}".ToJava();
        }
        
        for (int index = 0; index < 10; index++)
        {
            Assert.Equal(array[index].ToCSharp(), $"Sample_String_With_Value_{index}");
        }
        
        var array2 = new JavaArray<String>(array.CurrentRefPtr);
        
        for (int index = 0; index < 10; index++)
        {
            Assert.Equal(array[index].value_Property, array2[index].value_Property);
        }
    }
    
    [Fact]
    public void Should_create_valid_array_with_CSharp_strings()
    {
        Setup();

        var array = new JavaArray<string>(10);

        for (int index = 0; index < 10; index++)
        {
            array[index] = $"Sample_String_With_Value_{index}";
        }
        
        for (int index = 0; index < 10; index++)
        {
            Assert.Equal(array[index], $"Sample_String_With_Value_{index}");
        }
        
        var array2 = new JavaArray<string>(array.CurrentRefPtr);
        
        for (int index = 0; index < 10; index++)
        {
            Assert.Equal(array[index], array2[index]);
        }
    }
    
    [Fact]
    public void Should_test_LINQ_to_JavaArray()
    {
        Setup();

        var array = new JavaArray<int>(10);

        for (int index = 0; index < 10; index++)
        {
            array[index] = index;
        }

        Assert.True(array.Count == 10);
        Assert.True(array.Count(x => x > 5) == 4);
        Assert.True(array.First(x => x == 9) == 9);
        Assert.True(array.Select(x => x * 2).First(x => x > 10) == 12);
    }
    
    [Fact]
    public void Should_test_basic_array_function_implementations()
    {
        Setup();

        var array = new JavaArray<int>(10);

        for (int index = 0; index < 10; index++)
        {
            array[index] = index;
        }

        Assert.Throws<DovaException>(() => array.Add(default));
        Assert.Contains(3, array);
        Assert.True(array.Remove(2));
        Assert.True(array.IndexOf(9) == 9);
    }
}
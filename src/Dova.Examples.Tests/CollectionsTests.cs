using System.Linq;
using Dova.JDK.Extensions;
using Dova.JDK.java.lang;
using Dova.JDK.java.util;
using Xunit;

namespace Dova.Examples.Tests;

public class CollectionsTests : BaseTestClass
{
    [Fact]
    public void Should_perform_operations_on_ArrayList()
    {
        Setup();

        var list = new ArrayList();

        list.add(11.ToJava());
        list.add(new Integer(22));
        
        Assert.True(list.size() == 2);

        list.remove(11.ToJava());
        
        Assert.True(list.size() == 1);
        
        list.add(33.ToJava());
        list.add(44.ToJava());
        list.add(55.ToJava());
        
        Assert.True(list.size() == 4);

        for (var index = 0; index < list.size(); ++index)
        {
            var intObj = new Integer(list.get(index).CurrentRefPtr);
            var intPropStr = intObj.value_Property.ToString();
            var intExtMetStr = intObj.toString().ToCSharp();
            
            Assert.True(intPropStr.Equals(intExtMetStr));
        }
    }

    [Fact]
    public void Should_convert_Iterable_to_Enumerable()
    {
        Setup();
        
        var list = new ArrayList();

        for (int i = 0; i < 10; ++i)
        {
            list.add((i * i).ToJava());
        }
        
        Assert.True(list.size() == 10);

        var higher = list
            .AsEnumerable()
            .Select(x => new Integer(x.CurrentRefPtr))
            .Where(x => x.value_Property > 50)
            .ToList();
        
        Assert.True(higher.Count == 2);
    }
}
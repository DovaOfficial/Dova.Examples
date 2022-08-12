using Dova.JDK.Extensions;
using Dova.JDK.java.io;
using Xunit;

namespace Dova.Examples.Tests;

public class SerializationTests : BaseTestClass
{
    [Fact]
    public void Should_serialize_and_deserialize_primitives()
    {
        // Prepare
        
        Setup();
        
        const string FileName = "FileName.obj";

        if (System.IO.File.Exists(FileName))
        {
            System.IO.File.Delete(FileName);
        }

        // Serialize

        var valueInt = 1234;
        var valueDouble = 987.65;
        var valueString = "Some_Sample_String_To_Serialize";

        var fos = new FileOutputStream(FileName.ToJava());
        var oos = new ObjectOutputStream(fos);

        oos.writeInt(valueInt);
        oos.writeDouble(valueDouble);
        oos.writeObject(valueString.ToJava());

        oos.close();
        fos.close();

        // Deserialize

        var fis = new FileInputStream(FileName.ToJava());
        var ois = new ObjectInputStream(fis);

        var deserializedValueInt = ois.readInt();
        var deserializedValueDouble = ois.readDouble();
        
        var deserializedValueStringObj = ois.readObject();
        var deserializedValueString = deserializedValueStringObj.toString().ToCSharp();
        
        ois.close();
        fis.close();
        
        // Check
        
        Assert.Equal(valueInt, deserializedValueInt);
        Assert.Equal(valueDouble, deserializedValueDouble);
        Assert.Equal(valueString, deserializedValueString);
    }
}
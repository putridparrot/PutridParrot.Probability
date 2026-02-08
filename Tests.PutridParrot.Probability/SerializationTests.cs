using PutridParrot.Probability;
using System.Text.Json;

namespace Tests.PutridParrot.Probability;

public class SerializationTests
{
    [Test]
    public void JsonSerialization_SerializesCorrectly()
    {
        var p = new P(0.75f);
        var json = JsonSerializer.Serialize(p);
        
        Assert.That(json, Does.Contain("0.75"));
    }

    [Test]
    public void JsonDeserialization_DeserializesCorrectly()
    {
        var json = "{\"Value\":0.75}";
        var p = JsonSerializer.Deserialize<P>(json);
        
        Assert.That(p, Is.Not.Null);
        Assert.That(p!.Value, Is.EqualTo(0.75f).Within(0.001));
    }

    [Test]
    public void JsonRoundTrip_PreservesValue()
    {
        var original = new P(0.3333f);
        var json = JsonSerializer.Serialize(original);
        var deserialized = JsonSerializer.Deserialize<P>(json);
        
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.Value, Is.EqualTo(original.Value).Within(0.0001));
    }

    [Test]
    public void JsonSerialization_WithInvalidValue_Clamps()
    {
        var json = "{\"Value\":1.5}";
        var p = JsonSerializer.Deserialize<P>(json);
        
        Assert.That(p, Is.Not.Null);
        Assert.That(p!.Value, Is.EqualTo(1.0f));
    }

    [Test]
    public void JsonSerialization_WithNegativeValue_Clamps()
    {
        var json = "{\"Value\":-0.5}";
        var p = JsonSerializer.Deserialize<P>(json);
        
        Assert.That(p, Is.Not.Null);
        Assert.That(p!.Value, Is.EqualTo(0.0f));
    }

    [Test]
    public void JsonSerialization_InCollection_WorksCorrectly()
    {
        var probabilities = new List<P>
        {
            new P(0.1f),
            new P(0.5f),
            new P(0.9f)
        };

        var json = JsonSerializer.Serialize(probabilities);
        var deserialized = JsonSerializer.Deserialize<List<P>>(json);
        
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.Count, Is.EqualTo(3));
        Assert.That(deserialized[0].Value, Is.EqualTo(0.1f).Within(0.001));
        Assert.That(deserialized[1].Value, Is.EqualTo(0.5f).Within(0.001));
        Assert.That(deserialized[2].Value, Is.EqualTo(0.9f).Within(0.001));
    }
}

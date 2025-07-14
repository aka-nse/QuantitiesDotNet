using System.Text.Json;
using Xunit.Abstractions;

namespace QuantitiesDotNet;

public class XunitSerializable<T> : IXunitSerializable
{
    public T Value { get; set; } = default!;

    public XunitSerializable() { }
    public XunitSerializable(T value) => Value = value;

    public void Serialize(IXunitSerializationInfo info)
    {
        info.AddValue("JsonValue", JsonSerializer.Serialize(Value!));
    }

    public void Deserialize(IXunitSerializationInfo info)
    {
        Value = JsonSerializer.Deserialize<T>(info.GetValue<string>("JsonValue"))!;
    }
}

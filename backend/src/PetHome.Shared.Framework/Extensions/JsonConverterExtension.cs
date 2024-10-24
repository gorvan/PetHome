using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace PetHome.Shared.Framework.Extensions
{
    public static class JsonConverterExtension
    {
        public static PropertyBuilder<T> HasValueJsonConverter<T>(this PropertyBuilder<T> builder)
        {
            return builder.HasConversion(
                new ValueJsonConverter<T>(),
                new ValueJsonComparer<T>());
        }
    }

    public class ValueJsonConverter<T>(ConverterMappingHints? mappingHints = null)
        : ValueConverter<T, string>(
            v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
            v => JsonSerializer.Deserialize<T>(v, JsonSerializerOptions.Default)!,
            mappingHints)
    {
    }

    public class ValueJsonComparer<T> : ValueComparer<T>
    {
        public ValueJsonComparer() : base(
            (l, r) => JsonSerializer.Serialize(l, JsonSerializerOptions.Default)
                == JsonSerializer.Serialize(r, JsonSerializerOptions.Default),
            v => v == null ? 0 : JsonSerializer.Serialize(v, JsonSerializerOptions.Default).GetHashCode(),
            v => JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
                JsonSerializerOptions.Default)!)
        {
        }
    }
}

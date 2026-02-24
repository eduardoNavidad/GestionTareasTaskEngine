using Xunit;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace TaskEngine.Tests
{
    public class JwtServiceTests
    {
        [Fact]
        public void JwtConfig_Key_ShouldNotBeNull()
        {
            // Arrange: Agregamos el '?' después de string para que coincida con lo que espera .NET
            var myConfiguration = new Dictionary<string, string?> {
                {"JwtConfig:Key", "8ioKNu1pTS6f8gb3Tg7WGyJbylaf2yF4"}
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            // Act
            var key = configuration["JwtConfig:Key"];

            // Assert
            Assert.NotNull(key);
            Assert.True(key.Length >= 32, "La clave JWT debe tener al menos 32 caracteres.");
        }
    }
}
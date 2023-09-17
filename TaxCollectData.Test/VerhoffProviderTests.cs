using FluentAssertions;
using TaxCollectData.Library.Abstraction.Providers;
using TaxCollectData.Library.Algorithms;
using Xunit;

namespace TaxCollectData.Test;

public class VerhoffProviderTests
{
    private const string Number = "123";
    private readonly IErrorDetectionAlgorithm _errorDetectionAlgorithm;

    public VerhoffProviderTests()
    {
        _errorDetectionAlgorithm = new VerhoeffAlgorithm();
    }

    [Fact]
    public void ValidateVerhoeffTest()
    {
        _errorDetectionAlgorithm.ValidateCheckDigit(Number).Should().BeFalse();
    }

    [Fact]
    public void GenerateVerhoeffTest()
    {
        _errorDetectionAlgorithm.GenerateCheckDigit(Number).Should().Be("3");
    }
}
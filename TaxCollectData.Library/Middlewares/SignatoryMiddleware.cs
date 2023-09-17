using TaxCollectData.Library.Abstraction.Cryptography;

namespace TaxCollectData.Library.Middlewares;

public class SignatoryMiddleware : Middleware
{
    private readonly ISignatory _signatory;

    public SignatoryMiddleware(ISignatory signatory)
    {
        _signatory = signatory;
    }

    public override string Handle(string text)
    {
        return HandleNext(_signatory.Sign(text));
    }
}
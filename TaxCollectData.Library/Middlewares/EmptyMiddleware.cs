namespace TaxCollectData.Library.Middlewares;

public class EmptyMiddleware : Middleware
{
    public override string Handle(string text)
    {
        return HandleNext(text);
    }
}
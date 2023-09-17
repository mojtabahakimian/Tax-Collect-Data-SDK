namespace TaxCollectData.Library.Middlewares;

public abstract class Middleware
{
    private Middleware _next;

    public static Middleware Link(Middleware first, params Middleware[] chain)
    {
        var head = first;
        foreach (var nextInChain in chain)
        {
            head._next = nextInChain;
            head = nextInChain;
        }

        return first;
    }

    public abstract string Handle(string text);

    protected string HandleNext(string text)
    {
        return _next == null ? text : _next.Handle(text);
    }
}
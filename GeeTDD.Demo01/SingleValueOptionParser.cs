namespace GeekTDD.Demo01;

class SingleValueOptionParser<T> : IOptionParser
{
    private readonly Func<string, T> _valueParser;

 
    public SingleValueOptionParser(Func<string, T> valueParser)
    {
        _valueParser = valueParser;
    }


    public object Parse(IList<string> args, CmdArgAttribute cmdArg)
    {
        var index = args.IndexOf($"-{cmdArg.Name}"); // index of "-p"
        return ParseValue(args[index + 1]) ?? throw new InvalidOperationException();
    }

    public virtual T ParseValue(string value)
    {
        return _valueParser(value);
    }
}
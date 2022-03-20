namespace GeekTDD.Demo01;

class BoolOptionParser : IOptionParser
{
    public object Parse(IList<string> args, CmdArgAttribute cmdArg)
    {
        return args.Contains($"-{cmdArg.Name}");
    }
}
namespace GeekTDD.Demo01;

interface IOptionParser
{
    object Parse(IList<string> args, CmdArgAttribute cmdArg);
}
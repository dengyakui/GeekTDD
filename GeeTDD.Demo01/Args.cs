using System.Diagnostics;
using System.Reflection;

using BindingFlags = System.Reflection.BindingFlags;

namespace GeekTDD.Demo01;

public class Args
{
    public static T Parse<T>(params string[] args) where T : class
    {
        var constructorInfos = typeof(T).GetConstructors(BindingFlags.Public | BindingFlags.Instance);
        var parameterInfos = constructorInfos[0].GetParameters();

        var argList = args.ToList();

        var valueArray = parameterInfos.Select(p => ParseOption<T>(argList, p)).ToArray();

        return (T)constructorInfos[0].Invoke(valueArray); //  [CmdArg("-l") ];
    }

    private static readonly Dictionary<Type, IOptionParser> Parsers = new()
    {
        { typeof(bool), new BoolOptionParser() },
        { typeof(int), new SingleValueOptionParser<int>(int.Parse) },
        { typeof(string), new SingleValueOptionParser<string>(s=>s) }
    };

    private static object? ParseOption<T>(IList<string> argList, ParameterInfo parameterInfo)
        where T : class
    {
        var cmdArgAttribute = parameterInfo.GetCustomAttribute<CmdArgAttribute>()!;

        var parameterType = parameterInfo.ParameterType;

        var parser = GetOptionParser<T>(parameterType);

        Debug.Assert(parser != null, nameof(parser) + " != null");
        return parser.Parse(argList, cmdArgAttribute);
    }

    private static IOptionParser? GetOptionParser<T>(Type parameterType) where T : class
    {
        return Parsers[parameterType];
    }
}
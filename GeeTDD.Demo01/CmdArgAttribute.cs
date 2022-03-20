namespace GeekTDD.Demo01;

[AttributeUsage(AttributeTargets.Parameter)]
public class CmdArgAttribute : Attribute
{

    public CmdArgAttribute(string s)
    {
        this.Name = s;
    }

    /// <summary>
    /// 命令行参数的名称 -d, -l ,-g,...
    /// </summary>
    public string Name { get; set; }
}
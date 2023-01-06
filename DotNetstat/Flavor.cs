using System.ComponentModel;

namespace DotNetstat;

public enum NetstatFlavor
{
    [Description("netstat -n -a -o")]
    Automatic,
    
    [Description("netstat -n -a -o")]
    WindowsNetstatNao,
    
    [Description("ss -ltnup")]
    LinuxSsLtnup,
    
    [Description("netstat -nlp")]
    LinuxNetstatNlp,
    
    [Description("netstat -n -a -o")]
    LinuxNetstatNao
}
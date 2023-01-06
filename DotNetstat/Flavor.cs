namespace DotNetstat;

public enum Flavor
{
    [RelatedPlatform(Platform.Automatic)] [Command("netstat", "-n -a -o")]
    Automatic,

    [RelatedPlatform(Platform.Windows)] [Command("netstat", "-n -a -o")]
    WindowsNetstatNao,

    [RelatedPlatform(Platform.Linux)] [Command("ss", "-ltnup")]
    LinuxSsLtnup,

    [RelatedPlatform(Platform.Linux)] [Command("netstat", "-nlp")]
    LinuxNetstatNlp,

    [RelatedPlatform(Platform.Linux)] [Command("netstat", "-n -a -o")]
    LinuxNetstatNao
}
namespace DotNetstat.Shell;

public static class PlatformExtension
{
    public static string ExecuteShellCommand(this Platform platform, ICommand command)
    {
        while (true)
            switch (platform)
            {
                case Platform.Automatic:
                    platform = PlatformDetector.Detect();
                    continue;
                case Platform.Windows:
                    return Call.Cmd(command);
                case Platform.Linux:
                    return Call.Bash(command);
                case Platform.Osx:
                    return Call.Zsh(command);
                default:
                    throw new ArgumentOutOfRangeException(nameof(platform), platform, null);
            }
    }
}
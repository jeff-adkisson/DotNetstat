using System.Diagnostics;

namespace DotNetstat.Shell;

public static class Call
{
    public static string Cmd(ICommand command)
    {
        return ExecuteCommand("cmd", command);
    }

    public static string Bash(ICommand command)
    {
        return ExecuteCommand("/bin/bash", command);
    }

    public static string Zsh(ICommand command)
    {
        return ExecuteCommand("zsh", command);
    }

    private static string ExecuteCommand(string shellPath, ICommand command)
    {
        //https://loune.net/2017/06/running-shell-bash-commands-in-net-core/
        var cmd = $"{command.Name} {command.Arguments}";
        var escapedArgs = cmd.Replace("\"", "\\\"");

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = shellPath,
                Arguments = $"-c \"{escapedArgs}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        process.Start();
        var result = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        return result;
    }
}
using System.Collections;
using System.Diagnostics;

namespace DotNetstat;

public class Processes : IReadOnlyDictionary<int, Process>
{
    private readonly Dictionary<int, Process> _processes;

    private Processes(Dictionary<int, Process> processes)
    {
        _processes = processes;
    }

    public IEnumerator<KeyValuePair<int, Process>> GetEnumerator()
    {
        return _processes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int Count => _processes.Count;

    public bool ContainsKey(int key)
    {
        return _processes.ContainsKey(key);
    }

    public bool TryGetValue(int key, out Process value)
    {
        return _processes.TryGetValue(key, out value!);
    }

    public Process this[int key] => _processes[key];

    public IEnumerable<int> Keys => _processes.Keys;

    public IEnumerable<Process> Values => _processes.Values;

    /// <summary>
    ///     Returns a readonly dictionary of all running processes.
    ///     Note that processes may be disposed before you use them,
    ///     so take precautions when accessing values.
    /// </summary>
    /// <returns></returns>
    public static Processes GetRunning()
    {
        return new Processes(Process
            .GetProcesses()
            .ToDictionary(key => key.Id, val => val));
    }

    /// <summary>
    ///     Returns a single process by ID.
    /// </summary>
    /// <param name="processId"></param>
    /// <returns>Returns null if process ID not found.</returns>
    public static Process? GetProcessById(int processId)
    {
        try
        {
            return Process.GetProcessById(processId);
        }
        catch (Exception)
        {
            return null;
        }
    }
}
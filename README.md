# DotNetstat

Simplifies the process of finding out what processes are listening on what ports on various platforms.

## Automatic Usage

Determines the platform and attempts to select the most appropriate command to run.

```bash

```csharp
using DotNetStat;

var results = NetStat.Call();
```

## Specify Platform

Attempts to select the most appropriate command to run for the specified platform.

```csharp
using DotNetStat;

var results = NetStat.Call(Platform.Windows);
```

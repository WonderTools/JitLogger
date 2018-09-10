# JitLogger
Just in time logger for ASP.NET Core applications

## Logging in ASP.NET Core
Microsoft has modularized logging in ASP.Net core application. 
When logging using ILogger in ASP.NET core, the logs could be made available in Debug, Console, Application Insights, etc... by just adding one line in Startup.cs
Logs are very useful in an application. 
When properly logged there could be many benefits such as getting more information about bugs reported, analysing features based on usage.

## The logging use case Jit Logger solves
Logs could speed up development. The logs could help the developer to verify the use case that he has just implemented. 
It could help testers confirm an error scenario. Logs could also reduce the time spent by the developer in debugging with break points.
This would be very useful in enviroments where break points are not possible.

For all of the above uses of logs, the availability (speed and easiness) of the logs is very important.
For these use cases, logs can not be used if it would take time to get the logs. (In this case, I consider 2 mins as a log time)
It can not be used if too many steps have to be performed to get the logs.

Jit Logger solves these problems. Jit logger make application logs easily available in a decent UI.

## How to use it
1. In your asp.net core application install the nuget package "Nachiappan.JitLogger"
2. In ConfigureServices method in Startup.cs, add JitLogger by "services.AddJitLogger();"
3. In Configure method in Startup.cs, inject ILoggerFactory by "ILoggerFactory loggerFactory"
4. In Configure method in Startup.cs, add JitLogger Middleware by "app.UseJitLogger(loggerFactory);"

## Features
1. Just in Time logging for asp.net core applications
2. Aggregation of Just in Time logs from multiple services
3. Modifiying the Jit logging parameters at run-time
4. Adding authentication for Jit Logger

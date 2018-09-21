# JitLogger
Just in time logger for ASP.NET Core applications

## Logging in ASP.NET Core
Microsoft has modularized logging in ASP.Net core application.
Logging could be done in ASP.Net core by injecting an interface "ILogger" to your business code.
When logged in ILogger, the logs are available to the logging providers in the system. The logging providers could take the log and put it to the console, or debug or application insights or to a file or anywhere else.
Logging providers could be easily added by one or two lines of code. This has made accessing of application logs very easy.

There are many logging providers provided by default such as Console, Debug, ApplicationInsights, etc... There are also other logging providers such as Serilog, NLog, etc... It is also very easy to implement your own logging provider.

## Philosophy of Jit Logger
Most of the logger that are now available are making sure that logs are safely stored, and are analysable in the futher. They are doing a fantastic job. Most of the loggers are taking some time (few minutes) to make the log available. There were no logger that makes the logs instantaneously available. (Or at least I wasn't able to find). With Jit Logger a latest portion of your logs are instantaneous available in in a web page in your own application.

**Jit logger's philosophy is quick and easy access to recent logs in an analysable form.**

## Expected benefits of Jit Logger
1. Speed up developement by reducing debugging time
2. Speed up testing
3. Assists in debugging in enviroments where break points are not possible.
4. Aggregating of logs from a bunch of micro services

## How to use it
1. In your asp.net core application install the nuget package "Nachiappan.JitLogger"
2. In ConfigureServices method in Startup.cs, add JitLogger by "services.AddJitLogger();"
3. In Configure method in Startup.cs, inject ILoggerFactory by "ILoggerFactory loggerFactory"
4. In Configure method in Startup.cs, add JitLogger Middleware by "app.UseJitLogger(loggerFactory);"
5. When all the above steps are done, the logs are available you asp.net application in YOUR_HOST/jit-logger/ui.
Sample application using jit logger is also available in the below link
https://github.com/WonderTools/JitLoggerUsageSample#jitloggerusagesample


## Releases
All releases are planned to be done through nuget.
https://www.nuget.org/packages/Nachiappan.JitLogger

## Features
1. Just in Time logging for asp.net core applications (Available from 1.0.0)
2. Aggregation of Just in Time logs from multiple services (To be done)
3. Modifiying the Jit logging parameters at run-time (To be done)
4. Adding authentication for Jit Logger (To be done)


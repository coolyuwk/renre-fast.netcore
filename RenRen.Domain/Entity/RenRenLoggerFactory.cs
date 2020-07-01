using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace RenRen.Domain.Entity
{
    public class RenRenLoggerFactory
    {
        public static readonly ILoggerFactory DebugLogFactory = LoggerFactory.Create(build =>
        {
            Console.ForegroundColor = ConsoleColor.Green;
            build.AddConsole();  // 用于控制台程序的输出
            build.AddDebug();    // 用于VS调试，输出窗口的输出
        });
    }
}

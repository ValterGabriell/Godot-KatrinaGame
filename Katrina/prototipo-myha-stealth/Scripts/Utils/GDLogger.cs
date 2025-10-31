using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace PrototipoMyha.Utilidades
{
    public static class GDLogger
    {
        private static readonly object _lockObject = new object();
        public static void PrintBlue(object message,
        bool isVerbose = false,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0)
        {
            PrintWithEmoji("🔵", message, isVerbose, memberName, filePath, lineNumber);
        }

        public static void PrintGreen(object message,
            bool isVerbose = false,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            PrintWithEmoji("🟢", message, isVerbose, memberName, filePath, lineNumber);
        }

        public static void PrintRed(object message,
            bool isVerbose = false,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            PrintWithEmoji("🔴", message, isVerbose, memberName, filePath, lineNumber);
        }

        public static void PrintYellow(object message,
            bool isVerbose = false,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            PrintWithEmoji("🟡", message, isVerbose, memberName, filePath, lineNumber);
        }

        private static void PrintWithEmoji(string emoji, object message, bool isVerbose,
            string memberName, string filePath, int lineNumber)
        {
            var className = GetClassNameFromFilePath(filePath);
            var timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;

            lock (_lockObject)
            {
                if (isVerbose)
                {
                    GD.Print($"{emoji} ╔══ VERBOSE LOG ══════════════════════════════════");
                    GD.Print($"{emoji} ║ Timestamp: {timestamp}");
                    GD.Print($"{emoji} ║ Thread: {threadId}");
                    GD.Print($"{emoji} ║ Class: {className}");
                    GD.Print($"{emoji} ║ Method: {memberName}");
                    GD.Print($"{emoji} ║ Line: {lineNumber}");
                    GD.Print($"{emoji} ║ File: {System.IO.Path.GetFileName(filePath)}");
                    GD.Print($"{emoji} ║ Message: {message}");
                    GD.Print($"{emoji} ╚═════════════════════════════════════════════════");
                }
                else
                {
                    GD.Print($"{emoji} {message}");
                }
            }
        }

        private static string GetClassNameFromFilePath(string filePath)
        {
            return System.IO.Path.GetFileNameWithoutExtension(filePath) ?? "UnknownClass";
        }


        /// <summary>
        /// Print básico com informações de contexto
        /// </summary>
        public static void Print(object message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            var className = GetClassNameFromFilePath(filePath);
            var timestamp = DateTime.Now.ToString("HH:mm:ss.fff");

            lock (_lockObject)
            {
                GD.Print($"[{timestamp}] {className}.{memberName}:{lineNumber} -> {message}");
            }
        }

        /// <summary>
        /// Print com nível de log (INFO, WARNING, ERROR)
        /// </summary>
        public static void PrintLog(LogLevel level, object message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            var className = GetClassNameFromFilePath(filePath);
            var timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            var levelStr = GetLogLevelString(level);

            lock (_lockObject)
            {
                switch (level)
                {
                    case LogLevel.Error:
                        GD.PrintErr($"[{timestamp}] {levelStr} {className}.{memberName}:{lineNumber} -> {message}");
                        break;
                    case LogLevel.Warning:
                        GD.PrintErr($"[{timestamp}] {levelStr} {className}.{memberName}:{lineNumber} -> {message}");
                        break;
                    default:
                        GD.Print($"[{timestamp}] {levelStr} {className}.{memberName}:{lineNumber} -> {message}");
                        break;
                }
            }
        }

        /// <summary>
        /// Print de debug com informações extras
        /// </summary>
        public static void PrintDebug(object message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
#if DEBUG
            var className = GetClassNameFromFilePath(filePath);
            var timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;

            lock (_lockObject)
            {
                GD.Print($"[{timestamp}] [DEBUG] {className}.{memberName}:{lineNumber} -> {message}");
            }
#endif
        }

        /// <summary>
        /// Print de erro com stack trace
        /// </summary>
        public static void PrintError(object message, Exception exception = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            var className = GetClassNameFromFilePath(filePath);
            var timestamp = DateTime.Now.ToString("HH:mm:ss.fff");

            lock (_lockObject)
            {
                GD.PrintErr($"[{timestamp}] [ERROR] {className}.{memberName}:{lineNumber} -> {message}");

                if (exception != null)
                {
                    GD.PrintErr($"Exception: {exception.Message}");
                    GD.PrintErr($"StackTrace: {exception.StackTrace}");
                }
            }
        }

        /// <summary>
        /// Print de warning
        /// </summary>
        public static void PrintWarning(object message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            var className = GetClassNameFromFilePath(filePath);
            var timestamp = DateTime.Now.ToString("HH:mm:ss.fff");

            lock (_lockObject)
            {
                GD.PrintErr($"[{timestamp}] [WARNING] {className}.{memberName}:{lineNumber} -> {message}");
            }
        }

        /// <summary>
        /// Print de informação
        /// </summary>
        public static void PrintInfo(object message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            var className = GetClassNameFromFilePath(filePath);
            var timestamp = DateTime.Now.ToString("HH:mm:ss.fff");

            lock (_lockObject)
            {
                GD.Print($"[{timestamp}] [INFO] {className}.{memberName}:{lineNumber} -> {message}");
            }
        }

        /// <summary>
        /// Print com dados de performance
        /// </summary>
        public static void PrintPerformance(string operationName, TimeSpan elapsed,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            var className = GetClassNameFromFilePath(filePath);
            var timestamp = DateTime.Now.ToString("HH:mm:ss.fff");

            lock (_lockObject)
            {
                GD.Print($"[{timestamp}] [PERF] {className}.{memberName}:{lineNumber} -> {operationName}: {elapsed.TotalMilliseconds:F2}ms");
            }
        }


        /// <summary>
        /// Print de objeto com propriedades
        /// </summary>
        public static void PrintObject<T>(T obj, string objectName = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            var className = GetClassNameFromFilePath(filePath);
            var timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            var name = objectName ?? typeof(T).Name;

            lock (_lockObject)
            {
                GD.Print($"[{timestamp}] [OBJECT] {className}.{memberName}:{lineNumber} -> {name}:");

                if (obj == null)
                {
                    GD.Print("  null");
                }
                else
                {
                    var properties = typeof(T).GetProperties();
                    foreach (var prop in properties)
                    {
                        try
                        {
                            var value = prop.GetValue(obj);
                            GD.Print($"  {prop.Name}: {value ?? "null"}");
                        }
                        catch (Exception ex)
                        {
                            GD.Print($"  {prop.Name}: <Error: {ex.Message}>");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Método para medir performance de operações
        /// </summary>
        public static T MeasurePerformance<T>(Func<T> operation, string operationName = "Operation",
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                var result = operation();
                stopwatch.Stop();
                PrintPerformance(operationName, stopwatch.Elapsed, memberName, filePath, lineNumber);
                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                PrintError($"Error in {operationName} after {stopwatch.ElapsedMilliseconds}ms", ex, memberName, filePath, lineNumber);
                throw;
            }
        }

        /// <summary>
        /// Método para medir performance de operações void
        /// </summary>
        public static void MeasurePerformance(Action operation, string operationName = "Operation",
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                operation();
                stopwatch.Stop();
                PrintPerformance(operationName, stopwatch.Elapsed, memberName, filePath, lineNumber);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                PrintError($"Error in {operationName} after {stopwatch.ElapsedMilliseconds}ms", ex, memberName, filePath, lineNumber);
                throw;
            }
        }

        #region Helper Methods


        private static string GetLogLevelString(LogLevel level)
        {
            return level switch
            {
                LogLevel.Info => "[INFO]",
                LogLevel.Warning => "[WARNING]",
                LogLevel.Error => "[ERROR]",
                LogLevel.Debug => "[DEBUG]",
                _ => "[LOG]"
            };
        }

        #endregion
    }

    public enum LogLevel
    {
        Info,
        Warning,
        Error,
        Debug
    }
}
using UnityEngine;

public static class DebugMode
{
    public static void Log(Object caller, string message, bool debugMode, bool printCallerName = true)
    {
        if (debugMode)
        {
            string output = printCallerName ? $"[{caller.GetType().Name}] {message}" : message;

            Debug.Log(output, caller);
        }
    }
}
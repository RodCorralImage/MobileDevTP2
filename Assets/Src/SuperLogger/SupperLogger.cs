using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SupperLogger
{
    public static SupperLogger GetNewInstance() {
#if UNITY_EDITOR
        return new SupperLogerEditor();
#elif UNITY_ANDROID 
        return new SuperLoggerAndroid();
#endif
    }

    public abstract void SendLog(string msj);
    public abstract string GetAllLogs();
    //public abstract void ShowLogsWindow();
}

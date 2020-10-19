using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperLoggerAndroid : SupperLogger {
    const string PLUGUIN_NAME = "com.imagecampus.superlogger.RCLogger";
    static AndroidJavaClass _pluginClass = null;
    public static AndroidJavaClass PluginClass {
        get {
            if (_pluginClass == null)
                _pluginClass = new AndroidJavaClass(PLUGUIN_NAME);
            return _pluginClass;
        }
    }

    static AndroidJavaObject _pluginInstace = null;
    public AndroidJavaObject PluginInstace {
        get {
            if (_pluginInstace == null) {
                _pluginInstace = PluginClass.CallStatic<AndroidJavaObject>("getInstance");
            }
            return _pluginInstace;
        }
    }

    public override void SendLog(string msj) {
        PluginInstace.Call("sendLog", msj);
    }
    public override string GetAllLogs() {
        return PluginInstace.Call<string>("getAllLogs");
    }
}

using UnityEngine;
using UnityEngine.UI;

public class PuginTester : MonoBehaviour {

    [SerializeField] Text outputText;


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
    

    void SendLog(string msj) {
        PluginInstace.Call("sendLog", msj);
    }
    string GetLogs() {
        return PluginInstace.Call<string>("getAllLogs");
    }

    public void TestPluginBtn() {
        if (Application.platform != RuntimePlatform.Android) {
            Debug.LogWarning("You are not in android platform");
            outputText.text = "You are not in android platform";
            return;
        }

        SendLog(Time.time.ToString());

        outputText.text = GetLogs();
    }
}

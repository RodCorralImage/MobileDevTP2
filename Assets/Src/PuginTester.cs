using UnityEngine;
using UnityEngine.UI;

public class PuginTester : MonoBehaviour {

    [SerializeField] Text outputText;


    const string PLUGUIN_NAME = "com.imagecampus.rodrigounitylib";
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

    string GetInstanceTime() {
        return PluginInstace.Call<string>("getLocalTime");
    }

    string GetFullTime() {
        return PluginClass.CallStatic<string>("getFullTime");
    }

    public void TestPluginBtn() {
        if (Application.platform != RuntimePlatform.Android) {
            Debug.LogWarning("You are not in android platform");
            outputText.text = "You are not in android platform";
            return;
        }

        string localTime = GetInstanceTime();
        Debug.Log(localTime);
        string fullTime = GetFullTime();
        Debug.Log(fullTime);
        outputText.text = $"local time: {localTime}" +
            $"\n" +
            $"full time: {fullTime}";
    }
}

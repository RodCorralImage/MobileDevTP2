using UnityEngine;
using UnityEngine.UI;

public class PuginTester : MonoBehaviour {

    [SerializeField] Text outputText;
    SupperLogger logger = null;

    private void Start() {
        logger = SupperLogger.GetNewInstance();
    }

    public void TestPluginBtn() {
        logger.SendLog(Time.time.ToString());
        outputText.text = logger.GetAllLogs();

        logger.ShowLogsWindow();
    }
}

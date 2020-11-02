using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupperLogerEditor : SupperLogger
{
    public override void SendLog(string msj) {
        Debug.Log("Send log to Super logger: " + msj);
    }
    public override string GetAllLogs() {
        return "No estas en Android CARAJO!";
    }
    public override void ShowLogsWindow() {
        Debug.Log("ShowLogsWindow()");
    }
}

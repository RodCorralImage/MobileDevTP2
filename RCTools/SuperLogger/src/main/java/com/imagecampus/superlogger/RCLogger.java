package com.imagecampus.superlogger;

import android.util.Log;

import java.util.ArrayList;

public class RCLogger {
    private static final String LOGGER_TAG = "RCLogger";
    private static final String GAME_TAG = "TPNro2";

    private static RCLogger _instance = null;
    public static RCLogger getInstance() {
        if (_instance == null) {
            Log.d(LOGGER_TAG, "RCLogger created");
            _instance = new RCLogger();
        }
        return _instance;
    }


    private ArrayList<String> allLogs = new ArrayList<String>();

    public void sendLog(String msj) {
        Log.d(GAME_TAG, msj);
        allLogs.add(msj);
    }

    private static final String SEPARATOR = "\n";
    public String getAllLogs() {
        String logs = "";
        for (int i = 0; i < allLogs.size(); i++)
            logs += allLogs.get(i) + SEPARATOR;
        return logs;
    }
}

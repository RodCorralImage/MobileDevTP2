package com.imagecampus.superlogger;

import android.app.Activity;
import android.util.Log;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.view.WindowManager;

public class SupperLorggerAlert {
    private static final String TAG = "SupperLorggerAlert";

    /** Create a new TextAlert */
    private SupperLorggerAlert(){}

    /** Get Singleton instance of TextAlert */
    public static SupperLorggerAlert getInstance()
    {
        return SingletonHelper.INSTANCE;
    }

    /** Display passed string in Alert Dialog */
    public void show(Activity a, String msg)
    {
        Log.i(TAG, "Showing alert ("+msg+")");

        AlertDialog d = new AlertDialog
                .Builder(a)
                .setMessage(msg)
                .setPositiveButton("OK", new DialogInterface.OnClickListener(){
                    public void onClick(DialogInterface dialog, int id) {
                        dialog.dismiss();
                    }
                })
                .create();
        d.setCancelable(false);
        d.setCanceledOnTouchOutside(false);
        d.getWindow()
                .setFlags(WindowManager.LayoutParams.FLAG_NOT_FOCUSABLE, WindowManager.LayoutParams.FLAG_NOT_FOCUSABLE);
        d.show();
    }

    // Inner class singleton helper
    private static class SingletonHelper
    {
        private static final SupperLorggerAlert INSTANCE = new SupperLorggerAlert();
    }
}

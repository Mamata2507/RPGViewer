﻿using UnityEngine;

namespace RPG
{
    public static class Console
    {
        private static string myLog = "";
        private static bool showConsole = false;

        private static void OnEnable()
        {
            UnityEngine.Application.logMessageReceived += Log;
        }

        private static void OnDisable()
        {
            UnityEngine.Application.logMessageReceived -= Log;
        }

        private static void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) showConsole = !showConsole;
        }

        private static void Log(string logString, string stackTrace, LogType type)
        {
            myLog = myLog + "\n" + logString;
        }

        private static void OnGUI()
        {
            if (!showConsole) return;
            GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Screen.width / 1200.0f, Screen.height / 800.0f, 1.0f));
            GUI.TextArea(new Rect(0, 600, 300, 200), myLog);
        }
    }
}
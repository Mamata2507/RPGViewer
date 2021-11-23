using UnityEngine;

public class ConsoleManager : MonoBehaviour
{
    string myLog = "";
    bool showManager = true;

    private void OnEnable()
    {
        Application.logMessageReceived += Log;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= Log;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) showManager = !showManager;
    }

    public void Log(string logString, string stackTrace, LogType type)
    {
        myLog = myLog + "\n" + logString;
    }

    void OnGUI()
    {
        if (!showManager) return;
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Screen.width / 1200.0f, Screen.height / 800.0f, 1.0f));
        GUI.TextArea(new Rect(0, 600, 300, 200), myLog);
    }
}
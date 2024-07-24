using UnityEditor;
using UnityEngine;
using System.Diagnostics;
using System.IO;

public class LabEditor : EditorWindow
{
    [MenuItem("1Lab/Show Editor")]
    public static void ShowWindow()
    {
        GetWindow<LabEditor>("1Lab");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Обновить"))
        {
            UpdateGitRepository();
        }
    }

    private void UpdateGitRepository()
    {
        string projectPath = Application.dataPath;
        string targetDirectory = Path.Combine(projectPath, "Plugins", "1Lab");

        if (!Directory.Exists(targetDirectory))
        {
            UnityEngine.Debug.LogError("Directory not found: " + targetDirectory);
            return;
        }

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            WorkingDirectory = targetDirectory,
            FileName = "git",
            Arguments = "pull",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (Process process = new Process { StartInfo = startInfo })
        {
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
        }

        UnityEngine.Debug.Log("Git pull completed.");
        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();
    }
}
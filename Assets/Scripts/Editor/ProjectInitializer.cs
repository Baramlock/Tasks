using System.IO;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public static class ProjectInitializer
{
    [MenuItem("Window/InitProject/Full", priority = 0)]
    private static void InitProject()
    {
        SetDefaultSetting();
        CreateDefaultFolder();
    }

    [MenuItem("Window/InitProject/Folder", priority = 2)]
    private static void CreateDefaultFolder()
    {
        var rootPath = Application.dataPath;

        var scriptsPath = FolderCreator(rootPath, "Scripts");
        var artPath = FolderCreator(rootPath, "Art");

        FolderCreator(rootPath, "Prefabs");
        FolderCreator(rootPath, "Animation");
        FolderCreator(rootPath, "Resources");
        FolderCreator(rootPath, "Packages");

        FolderCreator(artPath, "Materials");
        FolderCreator(artPath, "Sprites");
        FolderCreator(scriptsPath, "RunTime");
        FolderCreator(scriptsPath, "Editor");
        AssetDatabase.Refresh();
    }

    [MenuItem("Window/InitProject/Setting", priority = 1)]
    private static void SetDefaultSetting()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);

        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64 | AndroidArchitecture.ARMv7;

        PlayerSettings.colorSpace = ColorSpace.Gamma;

        PlayerSettings.SetUseDefaultGraphicsAPIs(BuildTarget.Android, false);

        PlayerSettings.SetGraphicsAPIs(BuildTarget.Android,
            new[] {GraphicsDeviceType.OpenGLES2, GraphicsDeviceType.OpenGLES3});

        PlayerSettings.Android.targetSdkVersion = (AndroidSdkVersions) 31;
    }

    private static string FolderCreator(string root, string folderName)
    {
        var path = root + @$"\{folderName}";
        Directory.CreateDirectory(path);
        return path;
    }
}
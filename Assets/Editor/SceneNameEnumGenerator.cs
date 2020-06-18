using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class SceneNameEnumGenerator
{
    private const string MenuItem = "Callum";
    private const string MenuItemPath = MenuItem + "/Generate Editor Scene Name Enum";

    [MenuItem(MenuItemPath)]
    private static void GenerateSceneNames()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("namespace BalsamicBits.BouncyTrash.Core\n{");

        sb.Append("\t");

        sb.AppendLine();
        sb.Append($"/// Generated using {nameof(SceneNameEnumGenerator)}.{nameof(GenerateSceneNames)}. Use menu item {MenuItemPath}");
        sb.AppendLine();

        sb.Append("\t");
        sb.Append("public enum TestSceneNames");
        sb.AppendLine();
        sb.Append("\t");
        sb.Append("{");
        sb.AppendLine();

        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (string.IsNullOrEmpty(scene.path))
            {
                continue;
            }

            string sceneName = Path.GetFileNameWithoutExtension(scene.path);
            sceneName = sceneName.Replace(" ", "_");

            sb.Append("\t");
            sb.Append("\t");
            sb.Append(sceneName);
            sb.Append(",");
            sb.AppendLine();
        }

        sb.Append("\t");
        sb.Append("}");
        sb.AppendLine();
        sb.Append("}");

        string dir = Path.Combine(Application.dataPath, "Scripts", "Core");

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        File.WriteAllText(Path.Combine(dir, "EditorSceneNames.cs"), sb.ToString());

        AssetDatabase.Refresh();
    }
}
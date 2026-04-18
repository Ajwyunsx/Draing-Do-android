using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;

public static class CodexBuild
{
    public static void BuildWindows()
    {
        string[] scenes = EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToArray();

        if (scenes.Length == 0)
        {
            throw new InvalidOperationException("No enabled scenes were found in EditorBuildSettings.");
        }

        string outputDirectory = Path.Combine("Builds", "Windows");
        Directory.CreateDirectory(outputDirectory);

        BuildReport report = BuildPipeline.BuildPlayer(
            scenes,
            Path.Combine(outputDirectory, "CodexBuild.exe"),
            BuildTarget.StandaloneWindows64,
            BuildOptions.None);

        if (report.summary.result != BuildResult.Succeeded)
        {
            throw new InvalidOperationException("Build failed with result: " + report.summary.result);
        }
    }
}

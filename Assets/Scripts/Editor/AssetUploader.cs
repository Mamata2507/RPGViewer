using System.IO;
using UnityEditor;
using Unity;

public class AssetUploader : Editor
{
    [MenuItem("Upload/Upload Tokens")]
    static void UploadTokens()
    {
        string init = File.ReadAllText(@"C:\Projects\GitHub\Repositories\RPGViewer\Uploader\tokens.txt");

        var process = System.Diagnostics.Process.Start("cmd.exe", init);

        process.WaitForExit();
    }

    [MenuItem("Upload/Upload Maps")]
    static void UploadBundles()
    {
        BuildPipeline.BuildAssetBundles(@"C:\Projects\GitHub\Repositories\RPGViewer\AssetBundles", BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);

        string init = File.ReadAllText(@"C:\Projects\GitHub\Repositories\RPGViewer\Uploader\bundles.txt");

        var process = System.Diagnostics.Process.Start("cmd.exe", init);

        process.WaitForExit();

        if (File.ReadAllText(@"C:\Projects\GitHub\Repositories\RPGViewer\AssetBundles\update.txt") == "false") File.WriteAllText(@"C:\Projects\GitHub\Repositories\RPGViewer\AssetBundles\update.txt", "true");
    }
}
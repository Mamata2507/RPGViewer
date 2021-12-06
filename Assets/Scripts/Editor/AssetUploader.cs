using System.IO;
using UnityEditor;

public class AssetUploader : Editor
{
    /// <summary>
    /// Upload tokens to Google Cloud Storage as images
    /// </summary>
    [MenuItem("Upload/Upload Tokens")]
    static void UploadTokens()
    {
        // Getting the command to run on cmd
        string init = File.ReadAllText(@"C:\Projects\GitHub\Repositories\RPGViewer\Uploader\tokens.txt");

        // Opening cmd and running the command
        var process = System.Diagnostics.Process.Start("cmd.exe", init);

        // Waiting until all tokens has been uploaded
        process.WaitForExit();
    }

    /// <summary>
    /// Upload maps to Google Cloud Storage as AssetBundles
    /// </summary>
    [MenuItem("Upload/Upload Maps")]
    static void UploadBundles()
    {
        // Building the AssetBundle, which contains all the maps
        BuildPipeline.BuildAssetBundles(@"C:\Projects\GitHub\Repositories\RPGViewer\AssetBundles", BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);

        // Getting the command to run on cmd
        string init = File.ReadAllText(@"C:\Projects\GitHub\Repositories\RPGViewer\Uploader\bundles.txt");

        // Opening cmd and running the command
        var process = System.Diagnostics.Process.Start("cmd.exe", init);

        // Waiting until all maps has been uploaded
        process.WaitForExit();

        // Announcing runtime builds to reload maps
        if (File.ReadAllText(@"C:\Projects\GitHub\Repositories\RPGViewer\AssetBundles\update.txt") == "false") File.WriteAllText(@"C:\Projects\GitHub\Repositories\RPGViewer\AssetBundles\update.txt", "true");
    }
}
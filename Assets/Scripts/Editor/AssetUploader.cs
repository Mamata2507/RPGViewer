using System.IO;
using UnityEditor;

public class AssetUploader : Editor
{
    #region Tokens

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
    #endregion

    #region Maps
    [MenuItem("Upload/Upload Maps")]
    static void UploadBundles()
    {
        // Building the AssetBundle for WINDOWS, which contains all the maps
        BuildPipeline.BuildAssetBundles(@"C:\Projects\GitHub\Repositories\RPGViewer\AssetBundles\Windows", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows);

        // Building the AssetBundle for ANDROID, which contains all the maps
        BuildPipeline.BuildAssetBundles(@"C:\Projects\GitHub\Repositories\RPGViewer\AssetBundles\Android", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);

        // Getting the command to run on cmd
        string init = File.ReadAllText(@"C:\Projects\GitHub\Repositories\RPGViewer\Uploader\bundles.txt");

        // Opening cmd and running the command
        var process = System.Diagnostics.Process.Start("cmd.exe", init);

        // Waiting until all maps has been uploaded
        process.WaitForExit();

        // Announcing runtime builds to reload maps
        if (File.ReadAllText(@"C:\Projects\GitHub\Repositories\RPGViewer\AssetBundles\update.txt") == "false") File.WriteAllText(@"C:\Projects\GitHub\Repositories\RPGViewer\AssetBundles\update.txt", "true");
    }
#endregion
}
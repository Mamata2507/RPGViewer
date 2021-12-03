using System.IO;
using UnityEditor;
using Unity;

public class BundleBuilder : Editor
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles(@"C:\Projects\GitHub\Repositories\RPGViewer\AssetBundles", BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);
        string init;
        init = File.ReadAllText(@"C:\Projects\GitHub\Repositories\RPGViewer\AssetBundles\init.txt");
        
        var process = System.Diagnostics.Process.Start("cmd.exe", init);

        process.WaitForExit();

        File.WriteAllText(@"C:\Projects\GitHub\Repositories\RPGViewer\AssetBundles\update.txt", "true");
    }
}
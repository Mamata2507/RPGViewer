using System.IO;
using UnityEditor;

public class BundleBuilder : Editor
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles(@"C:\Projects\GitHub\Repositories\RPGViewer\AssetBundles", BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);
        string init;
        init = File.ReadAllText(@"C:\Projects\GitHub\Repositories\RPGViewer\AssetBundles\init.txt");
        System.Diagnostics.Process.Start(@"C:\Users\ALEKSI\AppData\Local\Google\Cloud SDK\cloud_env.bat", init);
    }
}
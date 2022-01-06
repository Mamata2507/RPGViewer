using System.IO;
using UnityEditor;

namespace RPG
{
    public class CloudUploader: Editor
    {
        [MenuItem("Upload/Upload Tokens")]
        private static void UploadTokens()
        {
            string command = File.ReadAllText(@"C:\Projects\GitHub\Repositories\RPGViewer\Uploader\tokens.txt");
            System.Diagnostics.Process process = System.Diagnostics.Process.Start("cmd.exe", command);
            
            process.WaitForExit();
        }

        [MenuItem("Upload/Upload Maps")]
        private static void UploadBundles()
        {
            BuildPipeline.BuildAssetBundles(@"C:\Projects\GitHub\Repositories\RPGViewer\AssetBundles\Windows", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows);
            BuildPipeline.BuildAssetBundles(@"C:\Projects\GitHub\Repositories\RPGViewer\AssetBundles\Android", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);

            string command = File.ReadAllText(@"C:\Projects\GitHub\Repositories\RPGViewer\Uploader\bundles.txt");
            System.Diagnostics.Process process = System.Diagnostics.Process.Start("cmd.exe", command);
            
            process.WaitForExit();          
        }
    }
}
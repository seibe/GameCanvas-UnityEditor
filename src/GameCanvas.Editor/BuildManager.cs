/*------------------------------------------------------------*/
/// <summary>GameCanvas for Unity</summary>
/// <author>Seibe TAKAHASHI</author>
/// <remarks>
/// (c) 2015-2017 Smart Device Programming.
/// This software is released under the MIT License.
/// http://opensource.org/licenses/mit-license.php
/// </remarks>
/*------------------------------------------------------------*/

using System.IO;
using UnityEditor;
using UnityEditor.Build;

namespace GameCanvas.Editor
{
    /// <summary>
    /// GameCanvas ビルドマネージャー
    /// </summary>
    internal sealed class BuildManager : IPreprocessBuild, IPostprocessBuild
    {
        public int callbackOrder { get { return 1; } }

        public void OnPreprocessBuild(BuildTarget target, string path)
        {
            //UnityEngine.Debug.LogFormat("{0}, {1}", target, path);
        }

        public void OnPostprocessBuild(BuildTarget target, string path)
        {
            //UnityEngine.Debug.LogFormat("{0}, {1}", target, path);

            disableAutorotateAssertion(target, path);
        }

        /// <summary>
        /// community.unity.com/t5/iOS-tvOS/UnityDefaultViewController-should-be-used-only-if-unity-is-set/td-p/2112497
        /// </summary>
        private static void disableAutorotateAssertion(BuildTarget target, string path)
        {
            if (target != BuildTarget.iOS) return;

            var viewControllerFileName = "UnityViewControllerBaseiOS.mm";

            var targetString = "\tNSAssert(UnityShouldAutorotate()";
            var filePath = Path.Combine(path, "Classes");
            filePath = Path.Combine(filePath, "UI");
            filePath = Path.Combine(filePath, viewControllerFileName);
            if (File.Exists(filePath))
            {
                var classFile = File.ReadAllText(filePath);
                var newClassFile = classFile.Replace(targetString, "\t//NSAssert(UnityShouldAutorotate()");
                if (classFile.Length != newClassFile.Length)
                {
                    File.WriteAllText(filePath, newClassFile);
                }
            }
        }
    }
}

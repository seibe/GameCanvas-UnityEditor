/*------------------------------------------------------------*/
/// <summary>GameCanvas for Unity</summary>
/// <author>Seibe TAKAHASHI</author>
/// <remarks>
/// (c) 2015-2017 Smart Device Programming.
/// This software is released under the MIT License.
/// http://opensource.org/licenses/mit-license.php
/// </remarks>
/*------------------------------------------------------------*/

using System.Text.RegularExpressions;
using UnityEditor;

namespace GameCanvas.Editor
{
    internal static class Updater
    {
        public static void AssertUnityVersion()
        {
            var match = cReg.Match(UnityEngine.Application.unityVersion);
            var x = match.Success ? int.Parse(match.Groups["x"].Value) : 0;
            var y = match.Success ? int.Parse(match.Groups["y"].Value) : 0;
            //var z = match.Success ? int.Parse(match.Groups["z"].Value) : 0;
            //var w = match.Success ? int.Parse(match.Groups["w"].Value) : 0;
            //var type = match.Success ? match.Groups["type"].Value : "?";

            if (x < 5 || y < 6)
            {
                if (EditorUtility.DisplayDialog("Required Unity 5.6", "Unity のバージョンが古いです\n\n5.6.0 以降をインストールしてください", "インストール", "無視"))
                {
                    System.Diagnostics.Process.Start("https://unity3d.com/jp/get-unity/download/archive");
                }
            }
        }

        private static Regex cReg = new Regex(@"^(?<x>\d+)\.(?<y>\d+)\.(?<z>\d+)(?<type>[fp])(?<w>\d+)$");
    }
}

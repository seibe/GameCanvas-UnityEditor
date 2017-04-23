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
using UnityEngine;

namespace GameCanvas.Editor
{
    internal static class PathUtil
    {
        public static bool Exists(string path)
        {
            return File.Exists(path) || Directory.Exists(path);
        }

        public static string Combine(params string[] paths)
        {
            return string.Join("/", paths);
        }

        public static readonly string ProjectRoot = Directory.GetParent(Application.dataPath).FullName;
    }
}

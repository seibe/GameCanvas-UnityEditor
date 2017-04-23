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
using UnityEngine;

namespace GameCanvas.Editor
{
    /// <summary>
    /// GameCanvas エディタマネージャー
    /// </summary>
    internal static class EditorManager
    {
        [InitializeOnLoadMethod]
        public static void OnLoad()
        {
            if (File.Exists(TempFile))
            {
                OnHotReload();
            }
            else
            {
                File.Create(TempFile);
                OnAwake();
            }
        }

        public static void OnAwake()
        {
            Debug.LogFormat("{0} (Unity {1})\n", ProductInfo.ProductName, Application.unityVersion);

            Updater.AssertUnityVersion();
        }

        public static void OnHotReload()
        {
            //Debug.Log("OnHotReload()\n");
        }

        public const string TempFile = "Temp/GameCanvasEditor.tmp";
    }
}

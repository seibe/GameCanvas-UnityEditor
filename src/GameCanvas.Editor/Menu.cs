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
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GameCanvas.Editor
{
    /// <summary>
    /// エディターメニュー
    /// </summary>
    internal class Menu
    {
        [MenuItem("GameCanvas/書き出し/アプリをビルドする", false, 100)]
        static void OpenBuilder()
        {
            BuildWindow.Open();
        }

        [MenuItem("GameCanvas/書き出し/Game.cs をクリップボードにコピー", false, 200)]
        static void CopyScript()
        {
            var targetPath = Path.Combine(Application.dataPath, "Scripts/Game.cs");
            if (!File.Exists(targetPath))
            {
                EditorUtility.DisplayDialog("Game.cs が見つかりません", "Game.csが既定の場所にないのでコピーできませんでした", "OK");
                return;
            }

            var content = File.ReadAllText(targetPath);
            EditorGUIUtility.systemCopyBuffer = content;
            Debug.Log("クリップボードに Game.cs の内容をコピーしました");
        }

        [MenuItem("GameCanvas/書き出し/UnityPackage を書き出す", false, 201)]
        static void ExportUnityPackage()
        {
            EditorApplication.ExecuteMenuItem("Assets/Export Package...");
        }

        [MenuItem("GameCanvas/エディタ設定/タッチ操作の無効化", false, 202)]
        static void DisableTouchInEditor()
        {
            var currentBuildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            var symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(currentBuildTargetGroup)
                .Split(';')
                .Select(symbol => symbol.Trim())
                .Distinct()
                .ToList();
            if (!symbols.Contains("GC_DISABLE_TOUCH"))
            {
                symbols.Add("GC_DISABLE_TOUCH");
            }
            PlayerSettings.SetScriptingDefineSymbolsForGroup(currentBuildTargetGroup, string.Join(";", symbols.ToArray()));
        }

        [MenuItem("GameCanvas/エディタ設定/タッチ操作の有効化", false, 203)]
        static void EnableTouchInEditor()
        {
            var currentBuildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            var symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(currentBuildTargetGroup)
                .Split(';')
                .Select(symbol => symbol.Trim())
                .Distinct()
                .ToList();
            if (symbols.Contains("GC_DISABLE_TOUCH"))
            {
                symbols.Remove("GC_DISABLE_TOUCH");
            }
            PlayerSettings.SetScriptingDefineSymbolsForGroup(currentBuildTargetGroup, string.Join(";", symbols.ToArray()));
        }

        [MenuItem("GameCanvas/エディタ設定/アセットデータベースの強制更新", false, 300)]
        static void ForceRebuildDatabase()
        {
            AssetProcessor.RebuildAssetDatabase();
            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }

        [MenuItem("GameCanvas/About GameCanvas", false, 1000)]
        static void OpenAbout()
        {
            var message = ProductInfo.Copyright + "\n\nAuthors: " + string.Join(", ", ProductInfo.Authors);
            EditorUtility.DisplayDialog("GameCanvas " + ProductInfo.Version, message, "OK");
        }
    }
}

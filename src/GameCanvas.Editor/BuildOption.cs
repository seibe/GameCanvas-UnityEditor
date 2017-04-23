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
    /// ビルドオプション
    /// </summary>
    public sealed class BuildOption
    {
        /// <summary>
        /// ビルドオプション
        /// </summary>
        /// <param name="target">ビルドターゲット</param>
        /// <param name="isDevelop">開発モードでビルドするかどうか</param>
        public BuildOption(MobilePlatform target, bool isDevelop = false)
        {
            bundleIdentifier = "jp.ac.keio.sfc.sdp" + System.DateTime.Now.ToString("MMddHHmmss");
            bundleVersion = "1.0.0";
            productName = PlayerSettings.productName;
            companyName = "Keio University";
            outFolderPath = Path.GetFullPath(Application.dataPath + "/../Build/");
            this.target = target;
            isDevelopment = isDevelop;
            connectProfiler = isDevelop;
            allowDebugging = false;
            il2cpp = target == MobilePlatform.iOS;
            minAndroidSdkVersion = AndroidSdkVersions.AndroidApiLevel16;
            iOSSdkVersion = iOSSdkVersion.DeviceSDK;
        }

        /// <summary>
        /// ビルドオプション
        /// </summary>
        public BuildOption()
        {
            bundleIdentifier = PlayerSettings.applicationIdentifier;
            bundleVersion = PlayerSettings.bundleVersion;
            productName = PlayerSettings.productName;
            companyName = "Keio University";
            outFolderPath = Path.GetFullPath(Application.dataPath + "/../Build/");
            target = EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS ? MobilePlatform.iOS : MobilePlatform.Android;
            isDevelopment = false;
            connectProfiler = false;
            allowDebugging = false;
            il2cpp = target == MobilePlatform.iOS;
            minAndroidSdkVersion = AndroidSdkVersions.AndroidApiLevel16;
            iOSSdkVersion = iOSSdkVersion.DeviceSDK;
        }

        /// <summary>アプリごとに固有の識別子</summary>
        public string bundleIdentifier;
        /// <summary>バージョン情報。`0.10.2`のように3つの数字をドットで繋げた文字列で表現してください</summary>
        public string bundleVersion;
        /// <summary>英数字のみで表したアプリケーション名</summary>
        public string productName;
        /// <summary>アプリケーションを開発した組織の名称</summary>
        public string companyName;
        /// <summary>アプリケーションを書き出す先のフォルダーパス</summary>
        public string outFolderPath;
        /// <summary>ビルドターゲット</summary>
        public MobilePlatform target;
        /// <summary>開発モードでビルドするかどうか</summary>
        public bool isDevelopment;
        /// <summary>Unityプロファイラーを使用するかどうか</summary>
        public bool connectProfiler;
        /// <summary>スクリプトデバッギングを許容するかどうか</summary>
        public bool allowDebugging;
        /// <summary>IL2CPPビルドを行うかどうか</summary>
        public bool il2cpp;
        /// <summary>最小Android SDKバージョン (Android限定)</summary>
        public AndroidSdkVersions minAndroidSdkVersion;
        /// <summary>実機向けかシミュレーター向けか (iOS限定)</summary>
        public iOSSdkVersion iOSSdkVersion;
    }
}

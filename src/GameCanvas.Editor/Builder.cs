/*------------------------------------------------------------*/
/// <summary>GameCanvas for Unity</summary>
/// <author>Seibe TAKAHASHI</author>
/// <remarks>
/// (c) 2015-2017 Smart Device Programming.
/// This software is released under the MIT License.
/// http://opensource.org/licenses/mit-license.php
/// </remarks>
/*------------------------------------------------------------*/

using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameCanvas.Editor
{
    /// <summary>
    /// アプリケーションビルダー
    /// </summary>
    public sealed class Builder
    {
        /// <summary>
        /// アプリケーションをAndroid向けに開発モードでビルドします
        /// </summary>
        public static void DebugBuildApk()
        {
            var option = new BuildOption(MobilePlatform.Android, true);
            OverrideOptionByCommandLine(ref option);
            Run(option);
        }

        /// <summary>
        /// アプリケーションをAndroid向けにビルドします
        /// </summary>
        public static void BuildApk()
        {
            var option = new BuildOption(MobilePlatform.Android);
            OverrideOptionByCommandLine(ref option);
            Run(option);
        }

        /// <summary>
        /// アプリケーションをiOS向けにビルドします
        /// </summary>
        public static void DebugBuildXcodeProj()
        {
            var option = new BuildOption(MobilePlatform.iOS, true);
            OverrideOptionByCommandLine(ref option);
            Run(option);
        }

        /// <summary>
        /// アプリケーションをiOS向けにビルドします
        /// </summary>
        public static void BuildXcodeProj()
        {
            var option = new BuildOption(MobilePlatform.iOS);
            OverrideOptionByCommandLine(ref option);
            Run(option);
        }

        /// <summary>
        /// ビルドを実行する
        /// </summary>
        /// <param name="option">ビルドオプション</param>
        internal static void Run(BuildOption option)
        {
            // 現在のビルド設定を控えておく
            var prevTarget = EditorUserBuildSettings.activeBuildTarget;
            var prevTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            var prevAllowDebuggin = EditorUserBuildSettings.allowDebugging;
            var prevConnectProfiler = EditorUserBuildSettings.connectProfiler;
            var prevDevelopment = EditorUserBuildSettings.development;

            // ビルド設定を上書きする
            PlayerSettings.applicationIdentifier = option.bundleIdentifier;
            PlayerSettings.bundleVersion = option.bundleVersion;
            PlayerSettings.productName = option.productName;
            PlayerSettings.companyName = option.companyName;
            EditorUserBuildSettings.allowDebugging = option.allowDebugging;
            EditorUserBuildSettings.connectProfiler = option.connectProfiler;
            EditorUserBuildSettings.development = option.isDevelopment;
            var buildTarget = option.target.ToBuildTarget();
            if (prevTarget != buildTarget)
            {
                option.target.SwitchActiveBuildTarget();
            }

            var outFilePath = option.outFolderPath + option.productName;

            switch (buildTarget)
            {
                case BuildTarget.Android:
                    if (Path.GetExtension(option.outFolderPath) != "apk")
                    {
                        outFilePath += ".apk";
                    }

                    PlayerSettings.Android.minSdkVersion = option.minAndroidSdkVersion;
                    var prevVersionCode = PlayerSettings.Android.bundleVersionCode;
                    PlayerSettings.Android.bundleVersionCode = prevVersionCode + 1;

                    // 既に出力ファイルがあれば退避させておく
                    if (File.Exists(outFilePath))
                    {
                        File.Move(outFilePath, outFilePath.Remove(outFilePath.Length - 4) + "." + File.GetLastWriteTime(outFilePath).ToString("MMddHHmmss") + ".apk");
                    }
                    break;

                case BuildTarget.iOS:
                    PlayerSettings.iOS.sdkVersion = option.iOSSdkVersion;
                    PlayerSettings.iOS.targetDevice = iOSTargetDevice.iPhoneAndiPad;
                    PlayerSettings.iOS.targetOSVersionString = string.Empty;
                    break;

                default:
                    return;
            }

            // ビルドを実行する
            var errorMessage = BuildPipeline.BuildPlayer(
                GetEnabledScenePaths(),
                outFilePath,
                buildTarget,
                option.il2cpp ? BuildOptions.Il2CPP : BuildOptions.None
            );

            // ビルド前の設定に戻す
            EditorUserBuildSettings.SwitchActiveBuildTarget(prevTargetGroup, prevTarget);
            EditorUserBuildSettings.allowDebugging = prevAllowDebuggin;
            EditorUserBuildSettings.connectProfiler = prevConnectProfiler;
            EditorUserBuildSettings.development = prevDevelopment;

            // エラー出力
            if (!string.IsNullOrEmpty(errorMessage))
            {
                Debug.LogError(errorMessage);
            }
        }

        /// <summary>
        /// コマンドライン引数を読み取り、ビルドオプションを上書きします
        /// </summary>
        /// <param name="option"></param>
        static void OverrideOptionByCommandLine(ref BuildOption option)
        {
            var argv = System.Environment.GetCommandLineArgs();
            var argc = argv.Length;

            for (var i = 0; i < argc; ++i)
            {
                switch (argv[i])
                {
                    case "/name":
                        if (i + 1 >= argc || argv[i + 1][0] == '/') break;
                        option.productName = argv[i + 1];
                        option.outFolderPath = "Build/" + argv[i + 1];
                        ++i;
                        break;

                    case "/id":
                        if (i + 1 >= argc || argv[i + 1][0] == '/') break;
                        option.bundleIdentifier = "jp.ac.keio.sfc." + argv[i + 1];
                        ++i;
                        break;

                    case "/bundleIdentifier":
                        if (i + 1 >= argc || argv[i + 1][0] == '/') break;
                        option.bundleIdentifier = argv[i + 1];
                        ++i;
                        break;

                    case "/version":
                        if (i + 1 >= argc || argv[i + 1][0] == '/') break;
                        option.bundleVersion = argv[i + 1];
                        ++i;
                        break;

                    case "/company":
                        if (i + 1 >= argc || argv[i + 1][0] == '/') break;
                        option.companyName = argv[i + 1];
                        ++i;
                        break;

                    case "/out":
                        if (i + 1 >= argc || argv[i + 1][0] == '/') break;
                        option.outFolderPath = Path.GetFileNameWithoutExtension(argv[i + 1]);
                        ++i;
                        break;

                    case "/minAndroidSDK":
                        if (i + 1 >= argc || argv[i + 1][0] == '/') break;
                        int version;
                        if (int.TryParse(argv[i + 1], out version))
                        {
                            option.minAndroidSdkVersion = (AndroidSdkVersions)version;
                        }
                        ++i;
                        break;

                    case "/develop":
                        option.isDevelopment = true;
                        break;

                    case "/useProfiler":
                        option.isDevelopment = true;
                        option.connectProfiler = true;
                        break;

                    case "/allowDebugging":
                        option.allowDebugging = true;
                        break;

                    case "/release":
                        option.isDevelopment = false;
                        option.connectProfiler = false;
                        break;

                    case "/il2cpp":
                        option.il2cpp = true;
                        break;

                    case "/mono2x":
                        option.il2cpp = false;
                        break;

                    case "/simulatorSDK":
                        option.iOSSdkVersion = iOSSdkVersion.SimulatorSDK;
                        break;

                    case "/deviceSDK":
                        option.iOSSdkVersion = iOSSdkVersion.DeviceSDK;
                        break;
                }
            }
        }

        /// <summary>
        /// 現在有効なシーンの一覧を取得します
        /// </summary>
        /// <returns></returns>
        internal static string[] GetEnabledScenePaths()
        {
            var scenePathList = new List<string>();

            foreach (var scene in EditorBuildSettings.scenes)
            {
                if (scene.enabled) scenePathList.Add(scene.path);
            }

            return scenePathList.ToArray();
        }
    }
}

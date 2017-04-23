/*------------------------------------------------------------*/
/// <summary>GameCanvas for Unity</summary>
/// <author>Seibe TAKAHASHI</author>
/// <remarks>
/// (c) 2015-2017 Smart Device Programming.
/// This software is released under the MIT License.
/// http://opensource.org/licenses/mit-license.php
/// </remarks>
/*------------------------------------------------------------*/

using UnityEditor;

namespace GameCanvas.Editor
{
    /// <summary>
    /// モバイルビルドターゲット
    /// </summary>
    public enum MobilePlatform
    {
        Invalid,
        Android = BuildTarget.Android,
        iOS = BuildTarget.iOS
    }

    /// <summary>
    /// <see cref="MobilePlatform"/> 拡張メソッド
    /// </summary>
    public static class MobilePlatformExtension
    {
        public static BuildTarget ToBuildTarget(this MobilePlatform platform)
        {
            return (BuildTarget)platform;
        }

        public static BuildTargetGroup ToBuildTargetGroup(this MobilePlatform platform)
        {
            switch (platform)
            {
                case MobilePlatform.Android: return BuildTargetGroup.Android;
                case MobilePlatform.iOS: return BuildTargetGroup.iOS;
                default: return BuildTargetGroup.Unknown;
            }
        }

        public static MobilePlatform ToMobilePlatform(this BuildTarget target)
        {
            switch (target)
            {
                case BuildTarget.Android: return MobilePlatform.Android;
                case BuildTarget.iOS: return MobilePlatform.iOS;
                default: return MobilePlatform.Invalid;
            }
        }

        public static void SwitchActiveBuildTarget(this MobilePlatform platform)
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(platform.ToBuildTargetGroup(), platform.ToBuildTarget());
        }
    }
}

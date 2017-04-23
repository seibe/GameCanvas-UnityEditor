/*------------------------------------------------------------*/
/// <summary>GameCanvas for Unity</summary>
/// <author>Seibe TAKAHASHI</author>
/// <remarks>
/// (c) 2015-2017 Smart Device Programming.
/// This software is released under the MIT License.
/// http://opensource.org/licenses/mit-license.php
/// </remarks>
/*------------------------------------------------------------*/

using UnityEngine;

namespace GameCanvas
{
    /// <summary>
    /// リソース情報を保持するためのカスタムアセット
    /// </summary>
    public class AssetHolder : ScriptableObject
    {
        /// <summary>
        /// カスタムアセットの保存先
        /// </summary>
        public const string Path = "Assets/Plugins/UnityGC/Resources/AssetHolder.asset";

        /// <summary>
        /// 画像のリスト
        /// </summary>
        public Sprite[] Images;

        /// <summary>
        /// 音声のリスト
        /// </summary>
        public AudioClip[] Sounds;

        /// <summary>
        /// 矩形
        /// </summary>
        public Sprite Rect;

        /// <summary>
        /// 円
        /// </summary>
        public Sprite Circle;

        /// <summary>
        /// ダミー
        /// </summary>
        public Sprite Dummy;

        /// <summary>
        /// 文字のリスト
        /// </summary>
        public Sprite[] Characters;

        /// <summary>
        /// スプライト用マテリアル
        /// </summary>
        public Material Material;
    }
}

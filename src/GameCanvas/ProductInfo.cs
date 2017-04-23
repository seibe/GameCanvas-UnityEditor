
namespace GameCanvas
{
    /// <summary>
    /// GameCanvas 基本情報
    /// </summary>
    internal class ProductInfo
    {
        /// <summary>
        /// バージョン
        /// </summary>
        public const string Version = "1.3.0.0";

        /// <summary>
        /// バージョン表記
        /// </summary>
        public const string DisplayName = "1.3.0";

        /// <summary>
        /// プロダクト名
        /// </summary>
        public static readonly string ProductName = string.Format("GameCanvas for Unity v.{0}", DisplayName);

        /// <summary>
        /// 著作権表記
        /// </summary>
        public const string Copyright = "Copyright (c) 2015-2017 Smart Device Programming.";

        /// <summary>
        /// 執筆者リスト
        /// </summary>
        public readonly static string[] Authors =
        {
            "kuro",
            "fujieda",
            "seibe"
        };
    }
}

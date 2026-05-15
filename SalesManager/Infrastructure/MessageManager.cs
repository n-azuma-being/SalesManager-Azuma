using System.Windows.Forms;

namespace SalesManager.Infrastructure {
    /// <summary>
    /// メッセージを表示するためのクラス
    /// </summary>
    internal class MessageManager {

        private const string AppTitle = "販売管理アプリ";

        public static void ShowInfo(string text) {
            MessageBox.Show(text, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowError(string text) {
            MessageBox.Show(text, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool ShowQuestion(string text) {
            return MessageBox.Show(text, AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
    }
}

using System.Windows.Forms;

namespace SalesManager.Infrastructure {
    public static class FocusManager {
        /// <summary>
        /// Tabキーのループ処理を制御するクラス
        /// </summary>
        public static bool HandleTabLoop(Control activeControl, Control first, Control last, Keys keyData) {
            if ((keyData & Keys.KeyCode) == Keys.Tab) {
                bool shift = (keyData & Keys.Shift) == Keys.Shift;

                //最後にいたら最初へ
                if (!shift && activeControl == last) {
                    first.Focus();
                    return true;
                }
                //最初にいたら最後へ
                if (shift && activeControl == first) {
                    last.Focus();
                    return true;
                }
            }
            return false;
        }
    }
}

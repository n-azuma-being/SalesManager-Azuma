using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManager.Infrastructure {
    internal class MessageManager {

        private const string AppTitle = "販売管理アプリ";

        public static void ShowInfo(string text) {
            XtraMessageBox.Show(text, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowError(string text) {
            XtraMessageBox.Show(text, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool ShowQuestion(string text) {
            return XtraMessageBox.Show(text, AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
    }
}

using System.Windows.Forms;
using System.IO;

namespace SalesManager.Infrastructure {
    /// <summary>
    /// ファイル選択のためのクラス
    /// </summary>
    public static class FileManager {

        /// <summary>
        /// フォルダ選択ダイアログを表示し、ファイルを選択する
        /// </summary>
        public static string[] ShowOpenFilesDialog(string title, string filter) {
            using (OpenFileDialog dialog = new OpenFileDialog()) {
                dialog.Title = title;
                dialog.Filter = filter;
                dialog.Multiselect = true; // 複数選択を許可

                if (dialog.ShowDialog() == DialogResult.OK) {
                    return dialog.FileNames; // 選択された全パスを返す
                }
            }
            return null;
        }

        /// <summary>
        /// ファイル選択ダイアログを表示し、フォルダを選択する
        public static string ShowFolderDialog(string title, string filter) {
            using (OpenFileDialog dialog = new OpenFileDialog()) {
                dialog.Title = title;
                dialog.Filter = "Folder|.";
                dialog.CheckFileExists = false;
                dialog.CheckPathExists = true;

                dialog.FileName = "フォルダーを選択してください";

                if (dialog.ShowDialog() == DialogResult.OK) {
                    return Path.GetDirectoryName(dialog.FileName); // 選択された全パスを返す
                }
            }
            return null;
        }
    }
}
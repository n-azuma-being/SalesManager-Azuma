using System.Windows.Forms;

namespace SalesManager.Infrastructure {
    public static class FileManager {

        /// <summary>
        /// フォルダ選択ダイアログを表示し、選択されたパスを返す
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
        /// フォルダ選択ダイアログを表示し、選択されたパスを返す
        /// </summary>
        public static string ShowFolderDialog(IWin32Window owner, string description) {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog()) {
                dialog.Description = description;

                if (dialog.ShowDialog(owner) == DialogResult.OK) {
                    return dialog.SelectedPath;
                }
            }
            return null;
        }
    }
}
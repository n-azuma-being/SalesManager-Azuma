using System;
using System.Data;
using System.Data.SQLite;

namespace SalesManager.Infrastructure {
    /// <summary>
    /// DBとのやりとりを行うためのクラス
    /// </summary>
    public static class DbManager {
        private static readonly string ConnectionString = "Data Source=SalesManagement.db;Version=3;";

        /// <summary>
        /// INSERT, UPDATE, DELETE文を実行する。
        /// </summary>
        public static int ExecuteNonQuery(string sql, SQLiteParameter[] parameters = null) {
            using (var conn = new SQLiteConnection(ConnectionString)) {
                conn.Open();
                using (var cmd = new SQLiteCommand(sql, conn)) {
                    if (parameters != null) {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return cmd.ExecuteNonQuery(); //影響を与えた行数
                }
            }
        }

        /// <summary>
        /// SELECT文を実行し、結果をDataTableで返す。
        /// </summary>
        public static DataTable ExecuteQuery(string sql, SQLiteParameter[] parameters = null) {
            DataTable dt = new DataTable();
            try {
                using (var conn = new SQLiteConnection(ConnectionString)) {
                    conn.Open();
                    using (var cmd = new SQLiteCommand(sql, conn)) {
                        if (parameters != null) {
                            cmd.Parameters.AddRange(parameters);
                        }
                        //SQLの結果をDataTableに流し込む
                        try {
                            using (var adapter = new SQLiteDataAdapter(cmd)) {
                                adapter.Fill(dt);
                            }
                        } catch (Exception ex) {
                            MessageManager.ShowError($"データベース登録中にエラーが発生しました。{Environment.NewLine}{ex.Message}");
                        }
                    }
                }
            } catch (Exception ex) {
                MessageManager.ShowError($"データベース接続に失敗しました。{Environment.NewLine}{ex.Message}");
            }
            return dt; //取得したデータの中身を返す
        }

        public static void ExecuteBatch(Action<SQLiteCommand> action) {
            using (var conn = new SQLiteConnection(ConnectionString)) {
                conn.Open();
                using (var transaction = conn.BeginTransaction()) //トランザクション開始
                {
                    try {
                        using (var cmd = new SQLiteCommand(conn)) {
                            action(cmd);
                        }
                        transaction.Commit();
                    } catch (Exception ex) {
                        transaction.Rollback();
                        MessageManager.ShowError($"データベース登録中にエラーが発生しました。{Environment.NewLine}{ex.Message}");
                    }
                }
            }
        }
    }
}
using SalesManager.Infrastructure;
using SalesManager.Model;
using SalesManager.Service;
using System;
using System.Collections.Generic;
using System.Data;


namespace SalesManager.Services {
    /// <summary>
    /// 商品管理画面の内部処理を行うクラス
    /// </summary>
    internal class ProductService {

        private readonly OrderReportService _orderReportService = new OrderReportService();

        /// <summary>
        /// 商品データを取得
        /// </summary>
        public List<ProductRecord> GetProduct() {
            var list = new List<ProductRecord>();

            DataTable dt = Repository.GetProductList();

            foreach (DataRow row in dt.Rows) {
                list.Add(new ProductRecord {
                    ProductId = Convert.ToInt32(row["商品ID"]),
                    ProductName = row["商品名"].ToString(),
                    UnitPrice = Convert.ToDecimal(row["単価"]),
                    StockQuantity = Convert.ToInt32(row["在庫数"]),
                    UpdatedAt = row["更新日時"].ToString()
                });
            }
            return list;
        }

        public void ExportFile(string folderPath, string templatePath) {
            try {
                RequestOrderReport(templatePath, folderPath);
            } catch (Exception ex) {
                MessageManager.ShowError($"出力に失敗しました。{Environment.NewLine}{ex.Message}");
            }
        }

        /// <summary>
        /// 発注書作成
        /// </summary>
        public void RequestOrderReport(string templatePath, string folderPath) {
            _orderReportService.CreateOrderReport(templatePath, folderPath);
        }
    }
}
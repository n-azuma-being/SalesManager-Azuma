using System;
using System.ComponentModel;

namespace SalesManager.Model {
    /// <summary>
    /// SQLiteの売上データを扱いやすくするためのクラス
    /// </summary>
    public class SaleRecord {
        // --- 画面表示用の項目 ---

        [DisplayName("売上日")]
        public DateTime SaleDate { get; set; }

        [DisplayName("店舗名")]
        public string StoreName { get; set; }

        [DisplayName("商品名")]
        public string ProductName { get; set; }

        [DisplayName("数量")]
        public int Quantity { get; set; }

        [DisplayName("売上金額")]
        // 単価 × 数量 を自動で計算するプロパティ
        public decimal TotalAmount => UnitPrice * Quantity;


        // --- 内部処理用（グリッドには表示させない） ---

        [Browsable(false)]
        public int SaleId { get; set; }

        [Browsable(false)]
        public int ProductId { get; set; }

        [Browsable(false)]
        public int StoreId { get; set; }

        [Browsable(false)]
        public decimal UnitPrice { get; set; }

    }
}

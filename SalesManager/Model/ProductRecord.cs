using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SalesManager.Model {
    internal class ProductRecord {
        // --- 画面表示用の項目 ---

        [DisplayName("商品ID")]
        public int ProductId { get; set; }

        [DisplayName("商品名")]
        public string ProductName { get; set; }

        [DisplayName("単価")]
        public decimal UnitPrice { get; set; }

        [DisplayName("区分")]
        public string CategoryName { get; set; }

        [DisplayName("在庫数")]
        public int StockQuantity { get; set; }


        // --- 内部処理用（グリッドには表示させない） ---

        [Browsable(false)]
        public int CategoryId { get; set; }

        [Browsable(false)]
        public DateTime LastUpdate { get; set; }
    }
}

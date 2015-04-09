using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AfterCareApplication
{
    class RowStyleSelector : StyleSelector
    {
        public Style PaidStyle { get; set; }
        public Style UnpaidStyle { get; set; }

        public override Style SelectStyle(object item,
                      DependencyObject container)
        {
            AfterCareDataContext dbContext = new AfterCareDataContext();
            var invoiceItem = item as InvoiceItem;

            if (invoiceItem == null)
            {
                return base.SelectStyle(item, container);
            }
            if (invoiceItem.Paid == true)
            {
                return PaidStyle;
            }
            return UnpaidStyle;
        }
    }
}

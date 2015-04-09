using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace AfterCareApplication
{
    class TemplateSelector : DataTemplateSelector
    {
        public DataTemplate StudentTemplate { get; set; }
        public DataTemplate UnpaidTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item,
                      DependencyObject container)
        {
            AfterCareDataContext dbContext = new AfterCareDataContext();
            var invoiceItem = item as InvoiceItem;
            
            if (invoiceItem == null)
                return base.SelectTemplate(item, container);

            return StudentTemplate;
        }
    }
}

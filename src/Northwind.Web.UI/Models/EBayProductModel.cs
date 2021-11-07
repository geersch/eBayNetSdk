using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Northwind.Web.UI.Models
{
    public class EBayProductModel
    {
        #region Properties

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int EBayCategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        #endregion

        #region Constructor(s)

        public EBayProductModel() { }

        public EBayProductModel(Product product)
        {
            this.ProductID = product.ProductID;
            this.ProductName = product.ProductName;
            this.EBayCategoryId = product.Category.eBayCategoryId.Value;
            this.UnitPrice = product.UnitPrice.Value;
            this.Quantity = product.UnitsInStock.Value;
        }

        #endregion

        private void Serialize(string filename)
        {
            string path = Path.Combine(ConfigurationManager.AppSettings["ebayDirectory"], filename);
            FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            XmlTextWriter writer = new XmlTextWriter(fileStream, new UTF8Encoding());
            XmlSerializer serializer = new XmlSerializer(typeof(EBayProductModel));
            writer.Formatting = Formatting.Indented;
            writer.IndentChar = ' ';
            writer.Indentation = 3;
            serializer.Serialize(writer, this);
            fileStream.Close();
        }

        public void PublishOnEBay()
        {
            Serialize(String.Format("{0}.xml", this.ProductID));
        }

        public void RemoveFromEbay()
        {
            Serialize(String.Format("remove_{0}.xml", this.ProductID));
        }
    }
}

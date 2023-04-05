using System.Xml.Linq;

namespace SFAccountingSystem.Models
{
    public class TransactionOFX
    {
        public TransactionOFX(XElement transaction)
        {
            Value = decimal.Parse(transaction.Element("TRNAMT")
                                             .Value
                                             .Replace("</TRNAMT>", "")
                                             .Replace(".", ","));

            Description = transaction.Element("MEMO")
                                     .Value
                                     .Replace("</MEMO>", "");

            Date = DateTimeOffset.ParseExact(transaction.Element("DTPOSTED")
                                                        .Value
                                                        .Replace("</DTPOSTED>", "")
                                                        .Substring(0, 8), "yyyyMMdd", null);
        }

        public decimal Value { get; set; }

        public string? Description { get; set; }

        public DateTimeOffset Date { get; set; } = new DateTimeOffset();
    }
}

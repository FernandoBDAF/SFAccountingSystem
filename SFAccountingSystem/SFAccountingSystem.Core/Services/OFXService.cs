using Microsoft.AspNetCore.Http;
using SFAccountingSystem.Core.Models;
using System.Text;
using System.Xml.Linq;

namespace SFAccountingSystem.Core.Services
{
    public class OFXService
    {
        public OFXService()
        {
        }

        public List<TransactionOFX> Process(IFormFile file)
        {
            var list = new List<TransactionOFX>();

            if (file != null)
            {
                var doc = ToXElement(file);

                foreach (var transaction in doc.Descendants("STMTTRN"))
                {
                    list.Add(new TransactionOFX(transaction));
                }
            }

            return list;
        }

        private XElement ToXElement(IFormFile file)
        {
            var stream = new StreamReader(file.OpenReadStream(), Encoding.GetEncoding("iso-8859-1"));
            var lines = new List<string>();

            while (stream.Peek() >= 0)
            {
                lines.Add(stream.ReadLine());
            }


            var tags = lines.Where(x => x.Contains("<STMTTRN>") ||
                                        x.Contains("<TRNTYPE>") ||
                                        x.Contains("<DTPOSTED>") ||
                                        x.Contains("<TRNAMT>") ||
                                        x.Contains("<FITID>") ||
                                        x.Contains("<CHECKNUM>") ||
                                        x.Contains("<MEMO>"));

            XElement el = new XElement("root");

            XElement son = null;

            foreach (var l in tags)
            {
                if (l.IndexOf("<STMTTRN>") != -1)
                {
                    son = new XElement("STMTTRN");
                    el.Add(son);

                    continue;
                }

                var tagName = GetTagName(l);
                var elSon = new XElement(tagName);

                elSon.Value = GetTagValue(l);

                son.Add(elSon);
            }

            return el;
        }

        private string GetTagName(string line)
        {
            int pos_init = line.IndexOf("<") + 1;
            int pos_end = line.IndexOf(">");

            pos_end = pos_end - pos_init;

            return line.Substring(pos_init, pos_end);
        }

        private string GetTagValue(string line)
        {
            int pos_init = line.IndexOf(">") + 1;

            string retValue = line.Substring(pos_init).Trim();

            if (retValue.IndexOf("[") != -1)
            {
                retValue = retValue.Substring(0, 8);
            }

            return retValue;
        }
    }
}

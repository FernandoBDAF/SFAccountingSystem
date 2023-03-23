using System.Text;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using SFAccountingSystem.ViewMoldes;

namespace SFAccountingSystem.Controllers
{
    public class FileController : Controller
    {
        private ILogger<FileController> _logger;

        public FileController(ILogger<FileController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Rows = new List<RowOFxViewModel>();

            return View();
        }

        [HttpPost]
        public IActionResult Index(ObjectFileViewModel model)
        {
            try
            {
                var rows = new List<RowOFxViewModel>();

                if (model.Ofx != null)
                {
                    _logger.LogError($"NOME DO ARQUIVO: {model.Ofx.Name}");
                    _logger.LogError($"TAMANHO DO ARQUIVO: {model.Ofx.Length}");

                    var doc = toXElement(model);

                    _logger.LogError("AQUI... (2)");
                    _logger.LogError($"QTD DESCENDENTES: {doc.Descendants("STMTTRN").Count()}");

                    foreach (var transaction in doc.Descendants("STMTTRN"))
                    {
                        _logger.LogError($"VALOR: {transaction.Element("TRNAMT").Value.Replace("</TRNAMT>", "").Replace(".", ",")}");

                        rows.Add(new RowOFxViewModel
                        {
                            Value = decimal.Parse(transaction.Element("TRNAMT").Value.Replace("</TRNAMT>", "").Replace(".", ",")),
                            Description = transaction.Element("MEMO").Value.Replace("</MEMO>", ""),
                            Date = DateTimeOffset.ParseExact(transaction.Element("DTPOSTED").Value.Replace("</DTPOSTED>", "").Substring(0, 8), "yyyyMMdd", null)
                        });
                    }
                }
                else
                {
                    _logger.LogError($"ARQUIVO VAZIO.");
                }

                ViewBag.Rows = rows;

                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public XElement toXElement(ObjectFileViewModel model)
        {
            var stream = new StreamReader(model.Ofx.OpenReadStream(), Encoding.GetEncoding("iso-8859-1"));
            var lines = new List<string>();

            while (stream.Peek() >= 0)
                lines.Add(stream.ReadLine());

            _logger.LogError($"LINES: {lines.Count()}");

            var tags = from line in lines
                       where line.Contains("<STMTTRN>") ||
                             line.Contains("<TRNTYPE>") ||
                             line.Contains("<DTPOSTED>") ||
                             line.Contains("<TRNAMT>") ||
                             line.Contains("<FITID>") ||
                             line.Contains("<CHECKNUM>") ||
                             line.Contains("<MEMO>")
                       select line;

            _logger.LogError($"TAGS: {tags.Count()}");

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

                var tagName = getTagName(l);
                var elSon = new XElement(tagName);
                elSon.Value = getTagValue(l);
                son.Add(elSon);
            }

            return el;
        }
        private string getTagName(string line)
        {
            int pos_init = line.IndexOf("<") + 1;
            int pos_end = line.IndexOf(">");
            pos_end = pos_end - pos_init;
            return line.Substring(pos_init, pos_end);
        }
        private string getTagValue(string line)
        {
            int pos_init = line.IndexOf(">") + 1;
            string retValue = line.Substring(pos_init).Trim();
            if (retValue.IndexOf("[") != -1)
            {
                //date--lets get only the 8 date digits
                retValue = retValue.Substring(0, 8);
            }
            return retValue;
        }
    }
}
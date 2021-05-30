using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItSerwis_Merge_v2
{
    public class DocumentName
    {
        public int parsedEmpNum { get; set; }
        public DocumentName(int _parsedEmpNum)
        {
            parsedEmpNum = _parsedEmpNum;
        }

        public string NameDocument()
        {
            Vars v = new Vars();
            string chars = v.chars;
            Random rdNum = new Random();

            // variables for randomNumber string - inserted to column documentnumber
            int rand1 = rdNum.Next(1, 100);
            int rand2 = rdNum.Next(1, 100);
            int rand3 = rdNum.Next(1, 100);

            int randChar1 = rdNum.Next(chars.Length - 1);
            char char1 = chars[randChar1];

            int randChar2 = rdNum.Next(chars.Length - 1);
            char char2 = chars[randChar2];

            int randChar3 = rdNum.Next(chars.Length - 1);
            char char3 = chars[randChar3];

            string randomNumber = $"NR{rand1}{rand2}{rand3}-{char1}{char2}{char3}";
            ShortServiceDocument s = new ShortServiceDocument();
            var lastDocID = s.Get_LastDocID();
            var parsedDocumentID = Int32.Parse(lastDocID);
            var now = s.GetDateString();

            parsedDocumentID += 1;

            var documentInternalID = $"ITSD/{now}/{parsedDocumentID}/{parsedEmpNum}/{randomNumber}";

            return documentInternalID;
        }
    }
}

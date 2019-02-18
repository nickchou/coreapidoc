using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoreApiDoc.Summary
{
    public class ClassSummary : BaseSummary
    {
        public ClassSummary(string xmlPath)
        {
            xmlPath = Path.ChangeExtension(xmlPath, ".xml");
            base.SummaryFileURI = xmlPath;
            base.SummaryType = new List<string>() { "T", "M" };
        }
    }
}

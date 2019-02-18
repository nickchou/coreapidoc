using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoreApiDoc.Summary
{
    public class ClassSummary : BaseSummary
    {
        public ClassSummary(string dllPath)
        {
            dllPath = Path.ChangeExtension(dllPath, ".xml");
            base.SummaryFileURI = dllPath;
            base.SummaryType = new List<string>() { "T", "M" };
        }
    }
}

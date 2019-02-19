using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoreApiDoc.Summary
{
    public class PropertiesSummary : BaseSummary
    {
        public PropertiesSummary(Type type)
        {
            base.SummaryType = new List<string>() { "P" };
            if (type != null)
            {
                base.SummaryFileURI = Path.ChangeExtension(new Uri(type.Assembly.CodeBase).AbsolutePath, ".xml");
            }
        }
    }
}

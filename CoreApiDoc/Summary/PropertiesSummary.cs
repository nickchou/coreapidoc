using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApiDoc.Summary
{
    public class PropertiesSummary : BaseSummary
    {
        public PropertiesSummary()
        {
            base.SummaryType = new List<string>() { "P" };
        }
    }
}

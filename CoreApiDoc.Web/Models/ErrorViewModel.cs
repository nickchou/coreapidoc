using System;

namespace CoreApiDoc.Web.Models
{
    /// <summary>
    /// ������ʵ��
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// ����ID
        /// </summary>
        public string RequestId { get; set; }
        /// <summary>
        /// ��ʾ����ID
        /// </summary>

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
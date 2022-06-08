using System;

namespace WebsiteApi.CusException
{
    /// <summary>
    /// Customer Exception chỉ mục XXX đã tồn tại trong hệ thống
    /// </summary>
    public class IsExist : Exception
    {
        public string StrName { get; }
        public IsExist() { }
        public IsExist(string message)
        : base(message) { }

        public IsExist(string message, Exception inner)
            : base(message, inner) { }

        public IsExist(string message, string strName)
            : this(message)
        {
            StrName = strName;
        }
    }
}

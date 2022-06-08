using System;

namespace WebsiteApi.CusException
{
    /// <summary>
    /// Customer chỉ mục YYY chưa tồn tại trong hệ thống
    /// </summary>
    public class IsNotExist : Exception
    {
        public string StrName { get; }
        public IsNotExist() { }
        public IsNotExist(string message)
        : base(message) { }

        public IsNotExist(string message, Exception inner)
            : base(message, inner) { }

        public IsNotExist(string message, string strName)
            : this(message)
        {
            StrName = strName;
        }

    }
}

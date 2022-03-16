﻿using System;

namespace WebsiteApi.CusException
{
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

﻿using System;

namespace Voca.DAL.Exceptions
{
    public class DataException : Exception
    {
        public DataException()
        {
        }

        public DataException(string message) : base(message)
        {
        }

        public DataException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

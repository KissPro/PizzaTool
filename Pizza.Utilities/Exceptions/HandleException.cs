using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Pizza.Utilities.Exceptions
{
    class HandleException : Exception
    {
        public HandleException() { }

        public HandleException(string message) : base(message) { }

        public HandleException(string message, Exception ex) : base(message, ex)
        {
        }
        public HandleException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}

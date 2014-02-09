using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iKiwi.Messages
{
    class InvalidMessageFormatException : Exception
    {
        #region Ctors

        public InvalidMessageFormatException()
            : base()
        {
        }

        public InvalidMessageFormatException(string message)
            : base(message)
        {
        }

        #endregion
    }
}

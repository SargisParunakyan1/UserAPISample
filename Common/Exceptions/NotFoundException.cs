using System;

namespace Common.Exceptions
{
    public class NotFoundException : Exception
    {
        #region Constructor

        public NotFoundException(string message) : base(message)
        {

        }

        #endregion
    }
}
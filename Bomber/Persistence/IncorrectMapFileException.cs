using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomber.Persistence
{
    public class IncorrectMapFileException : Exception
    {
        public IncorrectMapFileException()
        {

        }

        public IncorrectMapFileException(string message) : base(message)
        {

        }

        public IncorrectMapFileException(string message, Exception e) : base(message, e)
        {

        }
    }
}

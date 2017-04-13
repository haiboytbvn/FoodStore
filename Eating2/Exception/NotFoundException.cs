using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eating2.Exception
{
    public class NotFoundException : SystemException
    {
        public NotFoundException() { }

        public NotFoundException(string message) { }
    }
}
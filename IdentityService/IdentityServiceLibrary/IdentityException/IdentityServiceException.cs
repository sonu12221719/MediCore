using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityServiceLibrary.IdentityException
{
    public class IdentityServiceException:Exception
    {
        public IdentityServiceException(string errMsg) : base(errMsg) { }
    }
}

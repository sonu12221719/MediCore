using System;

namespace EmrLibrary.Exceptions;

public class EmrServiceException:Exception
{
    public EmrServiceException(string errMsg):base(errMsg){}
}

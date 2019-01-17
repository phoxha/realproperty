using System;

namespace RealProperty.Model.Exceptions
{
    public class BusinessException : Exception
    {
        public int Status { get; set; }
        public string Description { get; set; }
    }
}

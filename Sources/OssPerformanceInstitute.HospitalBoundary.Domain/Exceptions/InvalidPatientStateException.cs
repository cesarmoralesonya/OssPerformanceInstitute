﻿
namespace OssPerformanceInstitute.HospitalBoundary.Domain.Exceptions
{
    public class InvalidPatientStateException : Exception
    {
        public InvalidPatientStateException(string message) : base(message)
        {
        }
    }
}

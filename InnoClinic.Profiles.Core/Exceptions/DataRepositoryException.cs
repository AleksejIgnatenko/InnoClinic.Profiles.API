using InnoClinic.Profiles.Core.Models;

namespace InnoClinic.Profiles.Core.Exceptions
{
    public class DataRepositoryException : Exception
    {
        public int HttpStatusCode { get; }
        public IEnumerable<PatientModel> FoundPatients { get; } = new List<PatientModel>();

        public DataRepositoryException(string message, int httpStatusCode) : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }

        public DataRepositoryException(string message, int httpStatusCode, IEnumerable<PatientModel> foundPatients) : base(message)
        {
            HttpStatusCode = httpStatusCode;
            FoundPatients = foundPatients;
        }
    }
}

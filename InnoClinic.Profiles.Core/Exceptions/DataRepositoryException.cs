using InnoClinic.Profiles.Core.Models.PatientModels;

namespace InnoClinic.Profiles.Core.Exceptions
{
    public class DataRepositoryException : Exception
    {
        public int HttpStatusCode { get; }
        public IEnumerable<PatientEntity> FoundPatients { get; } = new List<PatientEntity>();

        public DataRepositoryException(string message, int httpStatusCode) : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }

        public DataRepositoryException(string message, int httpStatusCode, IEnumerable<PatientEntity> foundPatients) : base(message)
        {
            HttpStatusCode = httpStatusCode;
            FoundPatients = foundPatients;
        }
    }
}

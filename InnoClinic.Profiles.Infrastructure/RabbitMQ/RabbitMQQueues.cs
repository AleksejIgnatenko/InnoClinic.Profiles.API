namespace InnoClinic.Profiles.Infrastructure.RabbitMQ
{
    public class RabbitMQQueues
    {
        public const string ADD_ACCOUNT_QUEUE = "ADD_ACCOUNT_QUEUE";
        public const string ADD_ACCOUNT_IN_PROFILE_API_QUEUE = "ADD_ACCOUNT_IN_PROFILE_API_QUEUE";
        public const string UPDATE_ACCOUNT_QUEUE = "UPDATE_ACCOUNT_QUEUE";
        public const string UPDATE_ACCOUNT_PHONE_QUEUE = "UPDATE_ACCOUNT_PHONE_QUEUE";
        public const string DELETE_ACCOUNT_QUEUE = "DELETE_ACCOUNT_QUEUE";

        public const string ADD_OFFICE_QUEUE = "ADD_OFFICE_QUEUE";
        public const string UPDATE_OFFICE_QUEUE = "UPDATE_OFFICE_QUEUE";
        public const string DELETE_OFFICE_QUEUE = "DELETE_OFFICE_QUEUE";

        public const string ADD_SPECIALIZATION_QUEUE = "ADD_SPECIALIZATION_QUEUE";
        public const string UPDATE_SPECIALIZATION_QUEUE = "UPDATE_SPECIALIZATION_QUEUE";
        public const string DELETE_SPECIALIZATION_QUEUE = "DELETE_SPECIALIZATION_QUEUE";

        public const string ADD_DOCTOR_QUEUE = "ADD_DOCTOR_QUEUE";
        public const string UPDATE_DOCTOR_QUEUE = "UPDATE_DOCTOR_QUEUE";
        public const string DELETE_DOCTOR_QUEUE = "DELETE_DOCTOR_QUEUE";

        public const string ADD_PATIENT_QUEUE = "ADD_PATIENT_QUEUE";
        public const string UPDATE_PATIENT_QUEUE = "UPDATE_PATIENT_QUEUE";
        public const string DELETE_PATIENT_QUEUE = "DELETE_PATIENT_QUEUE";

        public const string ADD_RECEPTIONIST_QUEUE = "ADD_RECEPTIONIST_QUEUE";
        public const string UPDATE_RECEPTIONIST_QUEUE = "UPDATE_RECEPTIONIST_QUEUE";
        public const string DELETE_RECEPTIONIST_QUEUE = "DELETE_RECEPTIONIST_QUEUE";
    }
}

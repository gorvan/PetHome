namespace PetHome.Shared.SharedKernel.Authorization
{
    public static class Permissions
    {
        public static class Volunteers
        {            
            public const string CreateVolunteer = "volunteers.create";
            public const string UpdateVolunteer = "volunteers.update";
            public const string DeleteVolunteer = "volunteers.delete";
            public const string ReadVolunteer = "volunteers.read";
            public const string ReadParticipant = "participant.read";
        }

        public static class Admin
        {
            public const string CreateAdmin = "admin.create";           
            public const string RestoreAdmin = "admin.restore";           
        }

        public static class Participant
        {            
            public const string ReadParticipant = "participant.read";
        }
    }
}

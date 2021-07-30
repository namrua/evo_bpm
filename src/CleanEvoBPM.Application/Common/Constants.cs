using System;
namespace CleanEvoBPM.Application.Common
{
    public static class Configuration
    {
        public const string ConnectionString = "ConnectionStrings:CleanEvoBPM";
    }

    public static class TypeLog
    {
        public const string Create = "Create";
        public const string Update = "Update";
        public const string Delete = "Delete";
    }

    public static class UserRole
    {
        public const string Admin = "admin";
    }
    public static class TableName
    {
        public const string BusinessDomain = "BusinessDomain";
        public const string ProjectBusinessDomain = "ProjectBusinessDomain";
        public const string ProjectLLBP = "ProjectLLBP";
        public const string ProjectTechnology = "ProjectTechnology";
        public const string ProjectMethodology = "ProjectMethodology";
        public const string Project = "Project";
        public const string Status = "Status";
    }


    public static class ValidateMessage
    {
        public const string UniqueName = "The inputted name has already existed";
        public const string DeleteMasterDataFailed = "Cannot delete this item as this is being used in other project";
        public const string NotFound = "Cannot Found this item";
        public const string DeleteSucess = "Delete Successfully";
    } 

}

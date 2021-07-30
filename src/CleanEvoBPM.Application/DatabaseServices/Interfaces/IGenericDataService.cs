using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.DatabaseServices.Interfaces
{
    public interface IGenericDataService<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(string tableName);
        Task<IEnumerable<T>> GetListIgnoreByID(string tableName, string ignoreByID);
        Task<bool> IsUniqueName(string tableName,string columnName,string valueName, string id);
        // Task<bool> IsNToNRelationship(string tableName, string colunmName, Guid statusId);
    }
}
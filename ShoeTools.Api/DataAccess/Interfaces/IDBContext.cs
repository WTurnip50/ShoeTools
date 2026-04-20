using System.Data.Common;

namespace ShoeTools.Api.DataAccess.Interfaces;

public interface IDBContext
{
    DbConnection Connection { get; }
}
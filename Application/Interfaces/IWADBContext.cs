using Microsoft.EntityFrameworkCore;
using SceletonAPI.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using StoredProcedureEFCore;

namespace SceletonAPI.Application.Interfaces
{
    public interface IDBContext
    {
        // For add to DB
        DbSet<User> Users { set; get; }
        IStoredProcBuilder loadStoredProcedureBuilder(string val);        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync1();
        
    }
}

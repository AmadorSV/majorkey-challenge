using System.Threading;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interface
{
    public interface IAppContext
    {
        DbSet<ServiceRequest> ServiceRequests { get; set; }
 
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
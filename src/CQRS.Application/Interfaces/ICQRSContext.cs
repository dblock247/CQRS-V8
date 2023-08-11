using System.Threading;
using System.Threading.Tasks;
using CQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Application.Interfaces;

public interface ICQRSContext
{
    // DbSet<User> Users { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

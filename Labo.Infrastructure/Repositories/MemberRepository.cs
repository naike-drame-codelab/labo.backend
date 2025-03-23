using Be.Khunly.EFRepository;
using Labo.Application.Interfaces.Repositories;
using Labo.Domain.Entities;

namespace Labo.Infrastructure.Repositories
{
    public class MemberRepository(LaboContext context) 
        : RepositoryBase<Member>(context), IMemberRepository
    {
    }
}

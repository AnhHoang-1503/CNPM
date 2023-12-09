using CNPM.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Domain
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<IEnumerable<Person>> FindPeopleByFamilyId(int familyId);

        Task<IEnumerable<Person>> FindPeopleByListIds(IEnumerable<int> ids);
    }
}

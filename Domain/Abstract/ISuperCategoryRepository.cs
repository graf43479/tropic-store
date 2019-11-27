
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface ISuperCategoryRepository
    {
        IQueryable<SuperCategory> SuperCategories { get; }

        SuperCategory GetSuperCategoryByShortName(string shortName);

        void SaveSuperCategory(SuperCategory superCategory);

        void DeleteSuperCategory(SuperCategory superCategory);

        string GetShortName(string name, int maxID);

        void RefreshAllShortNames();

        void UpdateSuperCategorySequence();

        void SuperCategorySequence(int superCategoryId, string actionType);

       // Task<IEnumerable<SuperCategory>> GetSuperCategoryListAsync();
    }
}

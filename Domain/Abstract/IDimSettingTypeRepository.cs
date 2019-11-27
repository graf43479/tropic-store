using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IDimSettingTypeRepository
    {
        IQueryable<DimSettingType> DimSettingTypes { get; }

        void SaveDimSettingType(DimSettingType dimSettingType, bool create);

        void DeleteDimSettingType(DimSettingType dimSettingType);

    //    Task<IEnumerable<DimSettingType>> GetDimSettingTypeListAsync();
    }
}

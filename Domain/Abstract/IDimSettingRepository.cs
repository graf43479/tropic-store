using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
   public interface IDimSettingRepository
    {
        IQueryable<DimSetting> DimSettings { get; }

        void SaveDimSetting(DimSetting dimSetting, bool create);

        void DeleteDimSetting(DimSetting dimSetting);

       bool SettingIsValid(string settingId, string settingTypeId);

     //  Task<IEnumerable<DimSetting>> GetDimSettingListAsync();
    }
}

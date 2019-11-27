
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;
using EntityState = System.Data.Entity.EntityState;

namespace Domain.Concrete
{
    public class EFDimSettingRepository : IDimSettingRepository
    {
        private EFDbContext context;

        public EFDimSettingRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IQueryable<DimSetting> DimSettings
        {
            get { return context.DimSettings; }
        }

        public void SaveDimSetting(DimSetting dimSetting, bool create)
        {
            if (create)
            {
                //context.DimSettings.Add(dimSetting);
                //context.Entry(dimSetting).State = EntityState.Added;
                //var tmp = context.DimSettingsTypes.Where(x => x.SettingTypeID == dimSetting.SettingsTypeID);
                
                //context.DimSettings.Add(dimSetting);
                context.DimSettings.Add(dimSetting);
                //context.Entry(dimSetting).State = EntityState.Added;
                context.SaveChanges();
            }
            else
            {
                DimSetting ds = context.DimSettings.FirstOrDefault(x => x.SettingsID == dimSetting.SettingsID);
                ds.SettingTypeID = dimSetting.SettingTypeID;
                ds.SettingsDesc = dimSetting.SettingsDesc;
                ds.SettingsValue = dimSetting.SettingsValue;
                ds.SettingsID = dimSetting.SettingsID;
                context.Entry(ds).State = EntityState.Modified;
                context.SaveChanges();
            }
            
        }

        public void DeleteDimSetting(DimSetting dimSetting)
        {
            context.DimSettings.Remove(dimSetting);
            context.SaveChanges();
        }

        public bool SettingIsValid(string settingId, string settingTypeId)
        {
            DimSetting ds =
                context.DimSettings.FirstOrDefault(x => x.SettingTypeID == settingTypeId && x.SettingsID == settingId);
            if (ds!=null)
            {
                return true;
            }
            return false;
        }


        //public async Task<IEnumerable<DimSetting>> GetDimSettingListAsync()
        //{
        //    return await context.DimSettings.ToListAsync().ConfigureAwait(false);
        //}
    }
}


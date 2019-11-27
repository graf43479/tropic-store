using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;
using EntityState = System.Data.Entity.EntityState;

namespace Domain.Concrete
{
    public class EFDimSettingTypeRepository : IDimSettingTypeRepository
    {
        
       private EFDbContext context;

       public EFDimSettingTypeRepository(EFDbContext context)
        {
            this.context = context;
        }

       public IQueryable<Entities.DimSettingType> DimSettingTypes
       {
           get { return context.DimSettingsTypes; }
       }

        public void SaveDimSettingType(DimSettingType dimSettingType, bool create)
        {
            if (create)
            {
                context.DimSettingsTypes.Add(dimSettingType);
            }
            else
            {
                context.Entry(dimSettingType).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public void DeleteDimSettingType(DimSettingType dimSettingType)
        {
            context.DimSettingsTypes.Remove(dimSettingType);
            context.SaveChanges();
        }

        //public async Task<IEnumerable<DimSettingType>> GetDimSettingTypeListAsync()
        //{
        //    return await context.DimSettingsTypes.ToListAsync().ConfigureAwait(false);
        //}
    }
}



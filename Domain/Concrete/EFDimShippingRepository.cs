using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EFDimShippingRepository : IDimShippingRepository
    {
        private EFDbContext context;

        public EFDimShippingRepository(EFDbContext context)
        {
            this.context = context;
        }
        
        public IQueryable<DimShipping> DimShipping
        {
            get { return context.DimShippings; }
        }
        public void SaveDimShipping(DimShipping dimShipping)
        {
            if (dimShipping.ShippingID == 0)
            {
                context.DimShippings.Add(dimShipping);
            }
            else
            {
                context.Entry(dimShipping).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public void DeleteDimShipping(DimShipping dimShipping)
        {
            context.DimShippings.Remove(dimShipping);
            context.SaveChanges();
        }

        //public async Task<IEnumerable<DimShipping>> GetDimShippingListAsync()
        //{
        //    return await context.DimShippings.ToListAsync().ConfigureAwait(false);
        //}
    }
}

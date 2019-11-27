using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;
using EntityState = System.Data.Entity.EntityState;

namespace Domain.Concrete
{
    public class EFDimOrderStatusRepository : IDimOrderStatusRepository
    {
        private EFDbContext context;

        public EFDimOrderStatusRepository(EFDbContext context)
        {
            this.context = context;
        }
        
        public IQueryable<DimOrderStatus> DimOrderStatuses
        {
            get { return context.DimOrderStatuses; }
        }

        public void SaveDimOrderStatus(DimOrderStatus dimOrderStatus)
        {
            if (dimOrderStatus.DimOrderStatusID == 0)
            {
                //dimOrderStatus.Sequence==context
                //var maxSequenceValue = context.DimOrderStatuses.Select(x => x.Sequence).Max() + 1;


                try
                {
                    dimOrderStatus.Sequence = context.DimOrderStatuses.Select(x => x.Sequence).Max() + 1;
                }
                catch (Exception)
                {
                    dimOrderStatus.Sequence = 1;
                }
                
                context.DimOrderStatuses.Add(dimOrderStatus);
            }
            else
            {
                context.Entry(dimOrderStatus).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public void DeleteDimOrderStatus(DimOrderStatus dimOrderStatus)
        {
            try
            {
                foreach (var status in context.DimOrderStatuses)
                {
                    if (status.Sequence < dimOrderStatus.Sequence)
                    {
                       
                    }
                    else
                    {
                        --status.Sequence;
                        context.Entry(status).State=EntityState.Modified;
                    }
                }
                
            }
            catch (Exception)
            {
                //dimOrderStatus.Sequence = 1;
            }
            context.DimOrderStatuses.Remove(dimOrderStatus);
            context.SaveChanges();
        }

        public void DimOrderStatusSequence(int dimOrderStatusId, string actionType)
        {
            DimOrderStatus dimOrderStatus1 =
                context.DimOrderStatuses.FirstOrDefault(x => x.DimOrderStatusID == dimOrderStatusId);
            
            if (actionType == "Up")
            {
                if (dimOrderStatus1.Sequence==1) 
                {
                    return;
                }
                else
                {
                DimOrderStatus dimOrderStatus2 =
                context.DimOrderStatuses.FirstOrDefault(x => x.Sequence == dimOrderStatus1.Sequence-1);
                    dimOrderStatus2.Sequence = dimOrderStatus2.Sequence + 1;
                    dimOrderStatus1.Sequence = dimOrderStatus1.Sequence - 1;
                    context.Entry(dimOrderStatus1).State = EntityState.Modified;
                    context.Entry(dimOrderStatus2).State = EntityState.Modified;
                    context.SaveChanges();
                }
              }
            else if (actionType == "Down")
            {
                if (dimOrderStatus1.Sequence==context.DimOrderStatuses.Max(x=>x.Sequence))
                {
                    return;
                }
                else
                {
                    DimOrderStatus dimOrderStatus2 =
                context.DimOrderStatuses.FirstOrDefault(x => x.Sequence == dimOrderStatus1.Sequence+1);
                    dimOrderStatus2.Sequence = dimOrderStatus2.Sequence - 1;
                    dimOrderStatus1.Sequence = dimOrderStatus1.Sequence + 1;
                    context.Entry(dimOrderStatus1).State = EntityState.Modified;
                    context.Entry(dimOrderStatus2).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            
        }

        public void UpdateDimOrderStatusSequence()
        {
            int i = 1;
            foreach (var dimOrderStatus in DimOrderStatuses)
            {
                dimOrderStatus.Sequence = i;
                context.Entry(dimOrderStatus).State=EntityState.Modified;
                i++;
            }
            context.SaveChanges();
        }

        //public async Task<IEnumerable<DimOrderStatus>> GetDimOrderStatusListAsync()
        //{
        //    return await context.DimOrderStatuses.ToListAsync().ConfigureAwait(false);
        //}
    }
}

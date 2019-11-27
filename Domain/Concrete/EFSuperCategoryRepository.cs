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
    public class EFSuperCategoryRepository : ISuperCategoryRepository
    {
        private EFDbContext context;

        public EFSuperCategoryRepository(EFDbContext context)
        {
            this.context = context;
        }


        public IQueryable<SuperCategory> SuperCategories
        {
            get { return context.SuperCategories.Include("Categories"); }
        }

        public SuperCategory GetSuperCategoryByShortName(string shortName)
        {
            SuperCategory superCategory = context.SuperCategories.FirstOrDefault(x => x.ShortName == shortName);
            return superCategory;
        }

        public void SaveSuperCategory(SuperCategory superCategory)
        {
            if (superCategory.SuperCategoryID == 0)
            {

                try
                {
                    superCategory.Sequence = context.SuperCategories.Select(x => x.Sequence).Max() + 1;
                }
                catch (Exception)
                {
                    superCategory.Sequence = 1;
                }

                if (superCategory.ShortName == null)

                    superCategory.ShortName = GetShortName(superCategory.Name, context.SuperCategories.Max(x => x.SuperCategoryID) + 1);

                context.SuperCategories.Add(superCategory);
            }
            else
            {
                context.Entry(superCategory).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public void DeleteSuperCategory(SuperCategory superCategory)
        {
            try
            {
                foreach (var c in context.SuperCategories)
                {
                    if (c.Sequence < superCategory.Sequence)
                    {

                    }
                    else
                    {
                        --c.Sequence;
                        context.Entry(c).State = EntityState.Modified;
                    }
                }

            }
            catch (Exception)
            {
                //dimOrderStatus.Sequence = 1;
            }

            context.SuperCategories.Remove(superCategory);
            context.SaveChanges();
        }

        public string GetShortName(string name, int maxID)
        {
            EFDbContext ef = new EFDbContext();
            {
                string s = Constants.TransliterateText(name);
                if (ef.SuperCategories.Any(x => x.ShortName == s && x.SuperCategoryID != maxID))
                {
                    return s + maxID.ToString();
                }
                return s;
            } 
        }

        public void RefreshAllShortNames()
        {
            for (int i = 0; i < 2; i++)
            {
                foreach (var superCategory in context.SuperCategories)
                {
                    superCategory.ShortName = GetShortName(superCategory.Name, superCategory.SuperCategoryID);
                    context.Entry(superCategory).State = EntityState.Modified;
                }
                context.SaveChanges();
            }
        }

        public void UpdateSuperCategorySequence()
        {
            int i = 1;
            foreach (var superCategory in SuperCategories)
            {
                superCategory.Sequence = i;
                context.Entry(superCategory).State = EntityState.Modified;
                i++;
            }
            context.SaveChanges();
        }

        public void SuperCategorySequence(int superCategoryId, string actionType)
        {
            SuperCategory superCategory1 =
                context.SuperCategories.FirstOrDefault(x => x.SuperCategoryID == superCategoryId);

            if (actionType == "Up")
            {
                if (superCategory1.Sequence == 1)
                {
                    return;
                }
                else
                {
                    SuperCategory superCategory2 =
                    context.SuperCategories.FirstOrDefault(x => x.Sequence == superCategory1.Sequence - 1);
                    superCategory2.Sequence = superCategory2.Sequence + 1;
                    superCategory1.Sequence = superCategory1.Sequence - 1;
                    context.Entry(superCategory1).State = EntityState.Modified;
                    context.Entry(superCategory2).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            else if (actionType == "Down")
            {
                if (superCategory1.Sequence == context.SuperCategories.Max(x => x.Sequence))
                {
                    return;
                }
                else
                {
                    SuperCategory superCategory2 =
                context.SuperCategories.FirstOrDefault(x => x.Sequence == superCategory1.Sequence + 1);
                    superCategory2.Sequence = superCategory2.Sequence - 1;
                    superCategory1.Sequence = superCategory1.Sequence + 1;
                    context.Entry(superCategory1).State = EntityState.Modified;
                    context.Entry(superCategory2).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }

        //public async Task<IEnumerable<SuperCategory>> GetSuperCategoryListAsync()
        //{
        //    return await context.SuperCategories.ToListAsync().ConfigureAwait(false);
        //}
    }
}

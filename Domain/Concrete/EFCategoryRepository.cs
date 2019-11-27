using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;
using EntityState = System.Data.Entity.EntityState;


namespace Domain.Concrete
{
    public class EFCategoryRepository: ICategoryRepository
    {
        private EFDbContext context;
       
        public EFCategoryRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Category> Categories
        {
            get { return context.Categories.Include("Products"); }
        }


        public Category GetCategoryByShortName(string shortName)
        {
            try
            {
                Category category = context.Categories.FirstOrDefault(x => x.ShortName == shortName);
                return category;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        /*public async Task<Category> GetCategoryByShortNameAsync(string shortName)
        {
            try
            {
                return await context.Categories.FirstOrDefaultAsync(x => x.ShortName == shortName).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return null;
            }
            
        }*/
        
        public void SaveCategory(Category category)
        {
            Debug.WriteLine("SaveCategory1 поток №{0}", Thread.CurrentThread.ManagedThreadId);
            if (category.CategoryID == 0)
            {

                    try
                    {
                        category.Sequence = context.Categories.Select(x => x.Sequence).Max() + 1;
                    }
                    catch (Exception)
                    {
                        category.Sequence = 1;
                    }
                
                if (category.ShortName == null)
                   
                category.ShortName = GetShortName(category.Name, context.Categories.Max(x => x.CategoryID)+1);

                category.UpdateDate = DateTime.Now;

                context.Categories.Add(category);
            }
            else
            {
                Category changingCat = context.Categories.FirstOrDefault(x => x.CategoryID == category.CategoryID);
                        changingCat.SuperCategoryID = category.SuperCategoryID;
                        changingCat.CategoryID = category.CategoryID;
                        changingCat.ImageExt = category.ImageExt;
                        changingCat.Name = category.Name;
                        changingCat.Sequence = category.Sequence;
                        changingCat.ShortName = category.ShortName;
                        changingCat.IsDeleted = category.IsDeleted;
                        changingCat.CategoryKeywords = category.CategoryKeywords;
                        changingCat.CategorySnippet = category.CategorySnippet;
                        changingCat.Description = category.Description;
                        changingCat.UpdateDate = DateTime.Now;

                        context.Entry(changingCat).State = EntityState.Modified;
            }
            Debug.WriteLine("SaveCategory2 поток №{0}", Thread.CurrentThread.ManagedThreadId);
            context.SaveChanges();
            Debug.WriteLine("SaveCategory3 поток №{0}", Thread.CurrentThread.ManagedThreadId);
        }

        public void DeleteCategory(Category category)
        {
            try
            {
                foreach (var c in context.Categories)
                {
                    if (c.Sequence < category.Sequence)
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
         
                context.Categories.Remove(category);
                context.SaveChanges();
         
         
            
        }

        public string GetShortName(string name, int maxID)
        {

            EFDbContext ef = new EFDbContext();
            {
                string s = Constants.TransliterateText(name);
                if (ef.Categories.Any(x => x.ShortName == s && x.CategoryID!=maxID))
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
                foreach (var category in context.Categories)
                {
                    category.ShortName = GetShortName(category.Name, category.CategoryID);
                    context.Entry(category).State = EntityState.Modified;
                }
                context.SaveChanges();    
            }
            
        }

        public void CategorySequence(int categoryId, string actionType)
        {
            Category category1 =
                context.Categories.FirstOrDefault(x => x.CategoryID == categoryId);

            if (actionType == "Up")
            {
                if (category1.Sequence == 1)
                {
                }
                else
                {
                    Category category2 =
                    context.Categories.FirstOrDefault(x => x.Sequence == category1.Sequence - 1);
                    category2.Sequence = category2.Sequence + 1;
                    category1.Sequence = category1.Sequence - 1;
                    context.Entry(category1).State = EntityState.Modified;
                    context.Entry(category2).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            else if (actionType == "Down")
            {
                if (category1.Sequence == context.Categories.Max(x => x.Sequence))
                {
                    return;
                }
                else
                {
                    Category category2 =
                context.Categories.FirstOrDefault(x => x.Sequence == category1.Sequence + 1);
                    category2.Sequence = category2.Sequence - 1;
                    category1.Sequence = category1.Sequence + 1;
                    context.Entry(category1).State = EntityState.Modified;
                    context.Entry(category2).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }

        }

        public void SetDeletedStatus(bool isDeleted, Category category)
        {
            category.IsDeleted = isDeleted;
            context.Entry(category).State = EntityState.Modified;
            context.SaveChanges();
        }

        //public async Task<IEnumerable<Category>> GetCategoryListAsync()
        //{
        //    return await context.Categories.ToListAsync().ConfigureAwait(false);
        //}

        public void UpdateCategorySequence()
        {
            int i = 1;
            foreach (var category in Categories)
            {
                category.Sequence = i;
                context.Entry(category).State = EntityState.Modified;
                i++;
            }
            context.SaveChanges();
        }
       
        
        
        /*

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
         */



      

    }
}

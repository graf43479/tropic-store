
using System;
using System.Data.Entity;
using System.Data.SqlClient;
using Domain.Entities;
using System.Configuration;


//using Domain.;

namespace Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext()
        {
            Database.SetInitializer<EFDbContext>(null);
        }
      /*  public EFDbContext()
        {
            try
            {
               this.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["EFDbContext"].ToString(); 
               this.Database.Connection.Open();
            }
            catch (Exception)
            {
                Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString(); 
      
            }
        
        }*/

        

        public DbSet<Product> Products { get; set; }

        public DbSet<SuperCategory> SuperCategories { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<OrderDetails> OrdersDetails { get; set; }

        public DbSet<OrdersSummary> OrdersSummaries { get; set; }

        public DbSet<ProductImage> ProductImages  { get; set; }

        public DbSet<DimOrderStatus> DimOrderStatuses { get; set; }

        public DbSet<OrderStatus> OrderStatuses { get; set; }

        public DbSet<DimSettingType> DimSettingsTypes { get; set; }

        public DbSet<DimSetting> DimSettings { get; set; }

        public DbSet<NewsTape> NewsTapes { get; set; }
        
        public DbSet<Role> Roles  { get; set; }
        
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<NLog_Error> NLogErrors { get; set; }

        public DbSet<DimShipping> DimShippings { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Mailing> Mailings { get; set; }



    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
         //  modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); 
            modelBuilder.Entity<OrdersSummary>().Property(r => r.OrderSummaryID).HasColumnName("OrderSummaryID");
           // modelBuilder.Entity<OrdersSummary>().Property(r => r.TransactionDate).HasColumnName("TransactionDate");
            modelBuilder.Entity<OrderDetails>().ToTable("OrdersDetails");

            modelBuilder.Entity<DimSetting>().ToTable("DimSettings");
            modelBuilder.Entity<DimSettingType>().ToTable("DimSettingsTypes");
            modelBuilder.Entity<NewsTape>().ToTable("NewsTape");
          /*  modelBuilder.Entity<NLog_Error>().ToTable("NLog_Error");
            modelBuilder.Entity<NLog_Error>().Property(r => r.ID).HasColumnName("Id");
            modelBuilder.Entity<NLog_Error>().Property(r => r.Time_stamp).HasColumnName("time_stamp");
            modelBuilder.Entity<NLog_Error>().Property(r => r.Host).HasColumnName("host");
            modelBuilder.Entity<NLog_Error>().Property(r => r.Type).HasColumnName("type");
            modelBuilder.Entity<NLog_Error>().Property(r => r.Source).HasColumnName("source");
            modelBuilder.Entity<NLog_Error>().Property(r => r.Message).HasColumnName("message");
            modelBuilder.Entity<NLog_Error>().Property(r => r.Level).HasColumnName("level");
            modelBuilder.Entity<NLog_Error>().Property(r => r.Logger).HasColumnName("logger");
            modelBuilder.Entity<NLog_Error>().Property(r => r.Stacktrace).HasColumnName("stacktrace");
            modelBuilder.Entity<NLog_Error>().Property(r => r.Allxml).HasColumnName("allxml");
            */
            base.OnModelCreating(modelBuilder);

            /*modelBuilder.Entity<Category>()
                        .HasMany(c => c.Products)
                        .WithRequired(p => p.Category)
                        .HasForeignKey(p => p.CategoryID);
            */
            modelBuilder.Entity<Product>()
                        .HasRequired(p => p.Category)
                        .WithMany(c => c.Products)
                        .HasForeignKey(p => p.CategoryID);
            
            
                
        }

 

        /*
            public void UpdateSequence(int productId, bool every)
        {
            
        if (every)
        {
           //int n = context.ProductImages.Select(x => x.ProductID).Distinct().Count();
        

            int[] z = context.ProductImages.Select(x => x.ProductID).Distinct().ToArray();


            foreach (var p in z)
            {
                int i = 1;
                //context.ProductImages.Where(x=>x.ProductID==p).Select(x=>x.ProductID);
                foreach (var productImage in context.ProductImages.Where(x=>x.ProductID==p))
                {
                    productImage.Sequence = i;
                    context.Entry(productImage).State = EntityState.Modified;
                    i++;
                }
            }
           }
         */



    }
}

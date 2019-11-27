using Domain.Abstract;

namespace Domain
{
    public class DataManager
    {
        private ICategoryRepository categoryRepository;
        private IProductRepository productRepository;
        private IOrderProcessor orderProcessor;
        private IUserRepository userRepository;
        private IOrderSummaryRepository orderSummaryRepository;
        private IOrderDetailsRepository orderDetailsRepository;
        private IProductImageRepository productImageRepository;
        private INewsTapeRepository newsTapeRepository;
        private IOrderStatusRepository orderStatusRepository;
        private IDimOrderStatusRepository dimOrderStatusRepository;
        private IDimSettingRepository dimSettingRepository;
        private IDimSettingTypeRepository dimSettingTypeRepository;
        private IDimShippingRepository dimShippingRepository;
        private IArticleRepository articleRepository;

        private PrimaryMembershipProvider provider;
        private PrimaryRoleProvider roleProvider;



        public DataManager(ICategoryRepository categoryRepository, IProductRepository productRepository, 
            IOrderProcessor orderProcessor, IUserRepository userRepository, IOrderSummaryRepository orderSummaryRepository, 
            IOrderDetailsRepository orderDetailsRepository, IProductImageRepository productImageRepository, 
            IOrderStatusRepository orderStatusRepository, 
            IDimSettingRepository dimSettingRepository, IDimSettingTypeRepository dimSettingTypeRepository, IDimShippingRepository dimShippingRepository,
            IDimOrderStatusRepository dimOrderStatusRepository,
            IArticleRepository articleRepository, INewsTapeRepository newsTapeRepository,
           PrimaryMembershipProvider provider, PrimaryRoleProvider roleProvider)
        {
            this.categoryRepository = categoryRepository;
            this.productRepository = productRepository;
            this.orderProcessor = orderProcessor;
            this.userRepository = userRepository;
            this.orderSummaryRepository = orderSummaryRepository;
            this.orderDetailsRepository = orderDetailsRepository;
            this.productImageRepository = productImageRepository;
            this.newsTapeRepository = newsTapeRepository;
            this.orderStatusRepository = orderStatusRepository;
            this.dimOrderStatusRepository = dimOrderStatusRepository;
            this.dimSettingRepository = dimSettingRepository;
            this.dimSettingTypeRepository = dimSettingTypeRepository;
            this.dimShippingRepository = dimShippingRepository;
            this.articleRepository = articleRepository;
            this.provider = provider;
            this.roleProvider = roleProvider;
        }

        public ICategoryRepository Categories {
            get { return categoryRepository; }
        }

        public IProductRepository Products {
            get { return productRepository; }
        }

        public IOrderProcessor OrdersProcessor {
            get { return orderProcessor; }
        }

        public IUserRepository UsersRepository
        {
            get { return userRepository; }
        }

        public IOrderSummaryRepository OrderSummaryRepository
        {
            get { return orderSummaryRepository; }
        }


        public IOrderDetailsRepository OrderDetailsRepository
        {
            get { return orderDetailsRepository; }
        }

        public IProductImageRepository ProductImageRepository
        {
            get { return productImageRepository;  }
        }
        
        public INewsTapeRepository NewsTapeRepository 
         {
            get { return newsTapeRepository;  }
        }

        public IOrderStatusRepository OrderStatusRepository  
         {
            get { return orderStatusRepository;  }
        }

        public IDimOrderStatusRepository DimOrderStatusRepository 
         {
            get { return dimOrderStatusRepository;  }
        }

        public IDimSettingRepository DimSettingRepository 
         {
            get { return dimSettingRepository;  }
        }

        public IDimSettingTypeRepository DimSettingTypeRepository
        {
            get { return dimSettingTypeRepository; }
        }

        public IDimShippingRepository DimShippingRepository
        {
            get { return dimShippingRepository; }
        }


        public IArticleRepository ArticleRepository
        {
            get { return articleRepository; }
        }

        public PrimaryMembershipProvider Provider
        {
            get { return provider; }
        }

        public PrimaryRoleProvider RoleProvider
        {
            get { return roleProvider; }
        }
    }
}

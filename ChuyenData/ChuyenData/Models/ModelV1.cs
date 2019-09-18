namespace ChuyenData.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelV1 : DbContext
    {
        public ModelV1()
            : base("name=ModelV1")
        {
        }

        public virtual DbSet<ListModuleItem> ListModuleItems { get; set; }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<ArticleModule> ArticleModules { get; set; }
        public virtual DbSet<ArticlePageSetting> ArticlePageSettings { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleType> ArticleTypes { get; set; }
        public virtual DbSet<BrandComment> BrandComments { get; set; }
        public virtual DbSet<ContentFile> ContentFiles { get; set; }
        public virtual DbSet<ContentPage> ContentPages { get; set; }
        public virtual DbSet<DocumentCategory> DocumentCategories { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<ExternSitePageProperty> ExternSitePageProperties { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<HomepageArticleModulePage> HomepageArticleModulePages { get; set; }
        public virtual DbSet<HomepageArticleModule> HomepageArticleModules { get; set; }
        public virtual DbSet<HtmlModule> HtmlModules { get; set; }
        public virtual DbSet<ImageModule> ImageModules { get; set; }
        public virtual DbSet<ModuleArticlePage> ModuleArticlePages { get; set; }
        public virtual DbSet<ModuleControl> ModuleControls { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<ModuleSetting> ModuleSettings { get; set; }
        public virtual DbSet<Newsletter> Newsletters { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<SitePage> SitePages { get; set; }
        public virtual DbSet<SitePagesArticle> SitePagesArticles { get; set; }
        public virtual DbSet<SitePageSetting> SitePageSettings { get; set; }
        public virtual DbSet<SitePageType> SitePageTypes { get; set; }
        public virtual DbSet<SiteSetting> SiteSettings { get; set; }
        public virtual DbSet<SiteZone> SiteZones { get; set; }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<UserArticlePage> UserArticlePages { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserSiteZone> UserSiteZones { get; set; }
        public virtual DbSet<Video> Videos { get; set; }
        public virtual DbSet<WebLink> WebLinks { get; set; }
        public virtual DbSet<WfImageSize> WfImageSizes { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<PostAttribute> PostAttributes { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostType> PostTypes { get; set; }
        public virtual DbSet<RelatedPost> RelatedPosts { get; set; }
        public virtual DbSet<Sources1> Sources1 { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<ExMerterial> ExMerterials { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<ProductFAQ> ProductFAQs { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<ProductProperty> ProductProperties { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<PropertyDefinition> PropertyDefinitions { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasMany(e => e.BrandComments)
                .WithRequired(e => e.Article)
                .HasForeignKey(e => e.BrandID);

            modelBuilder.Entity<DocumentCategory>()
                .HasMany(e => e.Documents)
                .WithRequired(e => e.DocumentCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HomepageArticleModule>()
                .HasMany(e => e.HomepageArticleModulePages)
                .WithOptional(e => e.HomepageArticleModule)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ModuleControl>()
                .HasMany(e => e.Modules)
                .WithRequired(e => e.ModuleControl)
                .HasForeignKey(e => e.ModuleType);

            modelBuilder.Entity<SitePage>()
                .HasMany(e => e.HomepageArticleModulePages)
                .WithOptional(e => e.SitePage)
                .WillCascadeOnDelete();

            modelBuilder.Entity<SitePageType>()
                .HasMany(e => e.SitePages)
                .WithRequired(e => e.SitePageType)
                .HasForeignKey(e => e.PageType);

            modelBuilder.Entity<Video>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Video)
                .HasForeignKey(e => e.ContentId);

            modelBuilder.Entity<Author>()
                .HasMany(e => e.Posts)
                .WithRequired(e => e.Author)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Post)
                .HasForeignKey(e => e.ContentId);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Posts)
                .Map(m => m.ToTable("PostsTags", "News").MapLeftKey("PostId").MapRightKey("TagId"));

            modelBuilder.Entity<Post>()
                .HasMany(e => e.SitePages)
                .WithMany(e => e.Posts)
                .Map(m => m.ToTable("SitePagesPosts", "News").MapLeftKey("PostId").MapRightKey("SitePageId"));

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Topics)
                .WithMany(e => e.Posts)
                .Map(m => m.ToTable("TopicsPosts", "News").MapLeftKey("PostId").MapRightKey("TopicId"));

            modelBuilder.Entity<PostType>()
                .HasMany(e => e.Posts)
                .WithRequired(e => e.PostType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sources1>()
                .HasMany(e => e.Posts)
                .WithRequired(e => e.Sources1)
                .HasForeignKey(e => e.SourceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Brand>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Brand)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithMany(e => e.Categories)
                .Map(m => m.ToTable("ProductsCategories", "Store").MapLeftKey("CategoryID").MapRightKey("ProductID"));

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ExMerterial>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.ExMerterial)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Property>()
                .HasMany(e => e.ProductProperties)
                .WithRequired(e => e.Property)
                .HasForeignKey(e => new { e.PropertyDefinitionID, e.Name });
        }
    }
}

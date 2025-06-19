using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessObjects
{
	public class MyStoreContext : DbContext
	{
		public MyStoreContext(DbContextOptions<MyStoreContext> options) : base(options) { }

		public DbSet<AccountMember> AccountMembers { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }

		private string GetConnectionString()
		{
			IConfiguration configuration = new ConfigurationBuilder()
										.SetBasePath(Directory.GetCurrentDirectory())
										.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
										.Build();

			string? connectionString = configuration["ConnectionStrings:DefaultConnectionString"];

			if (string.IsNullOrEmpty(connectionString))
			{
				throw new InvalidOperationException("Connection string 'DefaultConnectionString' is not configured.");
			}

			return connectionString;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(GetConnectionString());
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Cấu hình AccountMember
			modelBuilder.Entity<AccountMember>(entity =>
			{
				entity.HasKey(e => e.MemberId);
				entity.Property(e => e.MemberId).HasMaxLength(50);
				entity.Property(e => e.FullName).HasMaxLength(100).IsRequired();
				entity.Property(e => e.EmailAddress).HasMaxLength(100).IsRequired();
				entity.Property(e => e.MemberPassword).HasMaxLength(100).IsRequired();
				entity.Property(e => e.MemberRole).IsRequired();
				
				// Tạo unique index cho EmailAddress
				entity.HasIndex(e => e.EmailAddress).IsUnique();
			});

			// Cấu hình Category
			modelBuilder.Entity<Category>(entity =>
			{
				entity.HasKey(e => e.CategoryId);
				entity.Property(e => e.CategoryId).ValueGeneratedOnAdd();
				entity.Property(e => e.CategoryName).HasMaxLength(100).IsRequired();
			});

			// Cấu hình Product
			modelBuilder.Entity<Product>(entity =>
			{
				entity.HasKey(e => e.ProductId);
				entity.Property(e => e.ProductId).ValueGeneratedOnAdd();
				entity.Property(e => e.ProductName).HasMaxLength(200).IsRequired();
				entity.Property(e => e.UnitsInStock).IsRequired();
				entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)").IsRequired();
				
				// Cấu hình foreign key
				entity.HasOne(p => p.Category)
					.WithMany(c => c.Products)
					.HasForeignKey(p => p.CategoryId)
					.OnDelete(DeleteBehavior.Restrict);
			});

			// Seed data
			SeedData(modelBuilder);
		}

		private void SeedData(ModelBuilder modelBuilder)
		{
			// Seed Categories
			modelBuilder.Entity<Category>().HasData(
				new Category { CategoryId = 1, CategoryName = "Điện tử" },
				new Category { CategoryId = 2, CategoryName = "Thời trang" },
				new Category { CategoryId = 3, CategoryName = "Sách" },
				new Category { CategoryId = 4, CategoryName = "Thể thao" }
			);

			// Seed Products
			modelBuilder.Entity<Product>().HasData(
				new Product { ProductId = 1, ProductName = "Laptop Dell", CategoryId = 1, UnitsInStock = 10, UnitPrice = 15000000 },
				new Product { ProductId = 2, ProductName = "Điện thoại iPhone", CategoryId = 1, UnitsInStock = 20, UnitPrice = 25000000 },
				new Product { ProductId = 3, ProductName = "Áo thun nam", CategoryId = 2, UnitsInStock = 50, UnitPrice = 200000 },
				new Product { ProductId = 4, ProductName = "Quần jean", CategoryId = 2, UnitsInStock = 30, UnitPrice = 500000 },
				new Product { ProductId = 5, ProductName = "Sách lập trình C#", CategoryId = 3, UnitsInStock = 15, UnitPrice = 150000 },
				new Product { ProductId = 6, ProductName = "Bóng đá", CategoryId = 4, UnitsInStock = 25, UnitPrice = 300000 }
			);

			// Seed Admin account
			modelBuilder.Entity<AccountMember>().HasData(
				new AccountMember 
				{ 
					MemberId = "admin-001", 
					FullName = "Administrator", 
					EmailAddress = "admin@example.com", 
					MemberPassword = "admin123", 
					MemberRole = 1 
				}
			);
		}
	}
}

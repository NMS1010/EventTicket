using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventTicket.Repository.DBContext.Configuration
{
	public class UserConfiguration : IEntityTypeConfiguration<Entities.User>
	{
		public void Configure(EntityTypeBuilder<Entities.User> builder)
		{
			builder.HasData(new Entities.User()
			{
				Id = 1,
				Address = "Hà Nội",
				Email = "admin@admin.com",
				Name = "Admin",
				Password = "i2yBMU+FxDo=", // 123456
				Phone = "0123456789",
				UserName = "admin",
				Avatar = "default-user.png",
				Role = 0
			});
		}
	}
}
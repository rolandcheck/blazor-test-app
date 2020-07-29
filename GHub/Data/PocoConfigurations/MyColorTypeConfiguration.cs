using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GHub.Data.PocoConfigurations
{
    public class MyColorTypeConfiguration : IEntityTypeConfiguration<MyColor>
    {
        public void Configure(EntityTypeBuilder<MyColor> builder)
        {
            builder.Ignore(x => x.Color);
        }
    }
}
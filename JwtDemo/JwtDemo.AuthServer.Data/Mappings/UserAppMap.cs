using JwtDemo.AuthServer.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwtDemo.AuthServer.Data.Mappings
{
    public class UserAppMap : IEntityTypeConfiguration<UserApp>
    {
        public void Configure(EntityTypeBuilder<UserApp> builder)
        {
            builder.ToTable("@UserApps", @"dbo");
            builder.Property(x => x.City).HasColumnName("City").HasMaxLength(50);
        }
    }
}

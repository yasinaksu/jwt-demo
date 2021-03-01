using JwtDemo.AuthServer.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwtDemo.AuthServer.Data.Mappings
{
    public class UserRefreshTokenMap : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.ToTable(@"UserRefreshTokens", @"dbo");
            builder.HasKey(x => x.UserId);

            builder.Property(x => x.UserId).HasColumnName("UserId");
            builder.Property(x => x.Code).HasColumnName("Code");
            builder.Property(x => x.Expiration).HasColumnName("Expiration");
        }
    }
}

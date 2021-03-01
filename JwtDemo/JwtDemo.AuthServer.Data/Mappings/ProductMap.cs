using JwtDemo.AuthServer.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwtDemo.AuthServer.Data.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(@"Products", @"dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(200);
            builder.Property(x => x.Stock).HasColumnName("Stock").IsRequired();
            builder.Property(x => x.Price).HasColumnName("Price").IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(x => x.UserId).HasColumnName("UserId").IsRequired();
        }
    }
}

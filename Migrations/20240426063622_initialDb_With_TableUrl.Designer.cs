﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SubUrl.Data;

#nullable disable

namespace SubUrl.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240426063622_initialDb_With_TableUrl")]
    partial class initialDb_With_TableUrl
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SubUrl.Models.Url", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("FollowCount")
                        .HasColumnType("int");

                    b.Property<string>("LongValue")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ShortValue")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)");

                    b.HasKey("Id");

                    b.ToTable("Url");
                });
#pragma warning restore 612, 618
        }
    }
}
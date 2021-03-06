﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SitePerformanceTester.DataAccess;

namespace SitePerformanceTester.DataAccess.Migrations
{
    [DbContext(typeof(TesterContext))]
    partial class TesterContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("SitePerformanceTester.DataAccess.Models.SitemapRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("SitemapUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SitemapRequests");
                });

            modelBuilder.Entity("SitePerformanceTester.DataAccess.Models.SitemapUrl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<long>("ResponseTime")
                        .HasColumnType("bigint");

                    b.Property<int>("SitemapRequestId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SitemapRequestId");

                    b.ToTable("SitemapUrls");
                });

            modelBuilder.Entity("SitePerformanceTester.DataAccess.Models.SitemapUrl", b =>
                {
                    b.HasOne("SitePerformanceTester.DataAccess.Models.SitemapRequest", "SitemapRequest")
                        .WithMany("SitemapUrls")
                        .HasForeignKey("SitemapRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SitemapRequest");
                });

            modelBuilder.Entity("SitePerformanceTester.DataAccess.Models.SitemapRequest", b =>
                {
                    b.Navigation("SitemapUrls");
                });
#pragma warning restore 612, 618
        }
    }
}

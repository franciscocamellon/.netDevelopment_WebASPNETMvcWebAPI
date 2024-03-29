﻿// <auto-generated />
using System;
using Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(MobileAppDbContext))]
    partial class MobileAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Model.Models.DeveloperModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("EmployedStatus")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("GraduationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PublishedApps")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Developers");
                });

            modelBuilder.Entity("Domain.Model.Models.MobileAppModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AppName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DeveloperId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("PublishedStatus")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("DeveloperId");

                    b.ToTable("MobileApps");
                });

            modelBuilder.Entity("Domain.Model.Models.MobileAppModel", b =>
                {
                    b.HasOne("Domain.Model.Models.DeveloperModel", "Developer")
                        .WithMany("MobileApps")
                        .HasForeignKey("DeveloperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Developer");
                });

            modelBuilder.Entity("Domain.Model.Models.DeveloperModel", b =>
                {
                    b.Navigation("MobileApps");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(TaggerContext))]
    partial class TaggerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("DAL.Entities.LocationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("RootId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("RootId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("DAL.Entities.RootEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RootLocationId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RootLocationId");

                    b.ToTable("Roots");
                });

            modelBuilder.Entity("DAL.Entities.TagEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("DAL.Entities.TagGroupEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TagGroups");
                });

            modelBuilder.Entity("LocationEntityTagEntity", b =>
                {
                    b.Property<Guid>("LocationsId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TagsId")
                        .HasColumnType("TEXT");

                    b.HasKey("LocationsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("LocationEntityTagEntity");
                });

            modelBuilder.Entity("DAL.Entities.LocationEntity", b =>
                {
                    b.HasOne("DAL.Entities.LocationEntity", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.HasOne("DAL.Entities.RootEntity", "Root")
                        .WithMany()
                        .HasForeignKey("RootId");

                    b.Navigation("Parent");

                    b.Navigation("Root");
                });

            modelBuilder.Entity("DAL.Entities.RootEntity", b =>
                {
                    b.HasOne("DAL.Entities.LocationEntity", "RootLocation")
                        .WithMany()
                        .HasForeignKey("RootLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RootLocation");
                });

            modelBuilder.Entity("DAL.Entities.TagEntity", b =>
                {
                    b.HasOne("DAL.Entities.TagGroupEntity", "Group")
                        .WithMany("Tags")
                        .HasForeignKey("GroupId");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("LocationEntityTagEntity", b =>
                {
                    b.HasOne("DAL.Entities.LocationEntity", null)
                        .WithMany()
                        .HasForeignKey("LocationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.TagEntity", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Entities.LocationEntity", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("DAL.Entities.TagGroupEntity", b =>
                {
                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}

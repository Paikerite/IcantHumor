﻿// <auto-generated />
using System;
using IcantHumor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IcantHumor.Migrations
{
    [DbContext(typeof(IHDbContext))]
    partial class IHDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CategoryViewModelMediaViewModel", b =>
                {
                    b.Property<Guid>("CategoriesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PostsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CategoriesId", "PostsId");

                    b.HasIndex("PostsId");

                    b.ToTable("CategoryViewModelMediaViewModel", (string)null);
                });

            modelBuilder.Entity("IcantHumor.Models.CategoryViewModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("IcantHumor.Models.MediaViewModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateUpload")
                        .HasColumnType("datetime2");

                    b.Property<int>("Dislike")
                        .HasColumnType("int");

                    b.Property<Guid>("IdOfCreator")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Like")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeOfFile")
                        .HasColumnType("int");

                    b.Property<string>("UrlToFile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MediaFiles", (string)null);
                });

            modelBuilder.Entity("IcantHumor.Models.ReactedUserViewModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ChosenReact")
                        .HasColumnType("int");

                    b.Property<Guid>("IdReactedUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MediaViewModelId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MediaViewModelId");

                    b.ToTable("ReactedUsers", (string)null);
                });

            modelBuilder.Entity("IcantHumor.Models.UserViewModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("ConfirmEmail")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("CategoryViewModelMediaViewModel", b =>
                {
                    b.HasOne("IcantHumor.Models.CategoryViewModel", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IcantHumor.Models.MediaViewModel", null)
                        .WithMany()
                        .HasForeignKey("PostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IcantHumor.Models.ReactedUserViewModel", b =>
                {
                    b.HasOne("IcantHumor.Models.MediaViewModel", null)
                        .WithMany("WhoReacted")
                        .HasForeignKey("MediaViewModelId");
                });

            modelBuilder.Entity("IcantHumor.Models.MediaViewModel", b =>
                {
                    b.Navigation("WhoReacted");
                });
#pragma warning restore 612, 618
        }
    }
}

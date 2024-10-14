﻿// <auto-generated />
using System;
using EzTabs.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EzTabs.Data.Migrations
{
    [DbContext(typeof(EzTabsContext))]
    [Migration("20241014151442_m1")]
    partial class m1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("EzTabs.Model.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("ParentCommentId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TabId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Text")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("TabId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("EzTabs.Model.CommentRate", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CommentId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("Rate")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("UserId", "CommentId");

                    b.HasIndex("CommentId");

                    b.ToTable("CommentRate");
                });

            modelBuilder.Entity("EzTabs.Model.CommentReport", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CommentId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Text")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "CommentId");

                    b.HasIndex("CommentId");

                    b.ToTable("CommentReport");
                });

            modelBuilder.Entity("EzTabs.Model.FavouriteTab", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TabId")
                        .HasColumnType("char(36)");

                    b.HasKey("UserId", "TabId");

                    b.HasIndex("TabId");

                    b.ToTable("FavouriteTab");
                });

            modelBuilder.Entity("EzTabs.Model.Note", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int?>("Fret")
                        .HasColumnType("int");

                    b.Property<string>("String")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("TabId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("TabId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("EzTabs.Model.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Text")
                        .HasColumnType("longtext");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("EzTabs.Model.Tab", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("AuthorId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Band")
                        .HasColumnType("longtext");

                    b.Property<int>("BitsPerMinute")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Genre")
                        .HasColumnType("longtext");

                    b.Property<string>("Key")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.Property<long>("Views")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Tabs");
                });

            modelBuilder.Entity("EzTabs.Model.TabRate", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TabId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.HasKey("UserId", "TabId");

                    b.HasIndex("TabId");

                    b.ToTable("TabRate");
                });

            modelBuilder.Entity("EzTabs.Model.TabReport", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TabId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Text")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "TabId");

                    b.HasIndex("TabId");

                    b.ToTable("TabReport");
                });

            modelBuilder.Entity("EzTabs.Model.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsEmailVerified")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("VerificationCode")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EzTabs.Model.Comment", b =>
                {
                    b.HasOne("EzTabs.Model.Tab", "Tab")
                        .WithMany("Comments")
                        .HasForeignKey("TabId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EzTabs.Model.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId");

                    b.Navigation("Tab");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EzTabs.Model.CommentRate", b =>
                {
                    b.HasOne("EzTabs.Model.Comment", "Comment")
                        .WithMany("CommentRates")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EzTabs.Model.User", "User")
                        .WithMany("CommentRates")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EzTabs.Model.CommentReport", b =>
                {
                    b.HasOne("EzTabs.Model.Comment", "Comment")
                        .WithMany("CommentReports")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EzTabs.Model.User", "User")
                        .WithMany("CommentReports")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EzTabs.Model.FavouriteTab", b =>
                {
                    b.HasOne("EzTabs.Model.Tab", "Tab")
                        .WithMany("FavouriteTabs")
                        .HasForeignKey("TabId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EzTabs.Model.User", "User")
                        .WithMany("FavouriteTabs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tab");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EzTabs.Model.Note", b =>
                {
                    b.HasOne("EzTabs.Model.Tab", "Tab")
                        .WithMany("Notes")
                        .HasForeignKey("TabId");

                    b.Navigation("Tab");
                });

            modelBuilder.Entity("EzTabs.Model.Notification", b =>
                {
                    b.HasOne("EzTabs.Model.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EzTabs.Model.Tab", b =>
                {
                    b.HasOne("EzTabs.Model.User", "Author")
                        .WithMany("Tab")
                        .HasForeignKey("AuthorId");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("EzTabs.Model.TabRate", b =>
                {
                    b.HasOne("EzTabs.Model.Tab", "Tab")
                        .WithMany("TabRates")
                        .HasForeignKey("TabId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EzTabs.Model.User", "User")
                        .WithMany("TabRates")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tab");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EzTabs.Model.TabReport", b =>
                {
                    b.HasOne("EzTabs.Model.Tab", "Tab")
                        .WithMany("TabReports")
                        .HasForeignKey("TabId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EzTabs.Model.User", "User")
                        .WithMany("TabReports")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tab");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EzTabs.Model.Comment", b =>
                {
                    b.Navigation("CommentRates");

                    b.Navigation("CommentReports");
                });

            modelBuilder.Entity("EzTabs.Model.Tab", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("FavouriteTabs");

                    b.Navigation("Notes");

                    b.Navigation("TabRates");

                    b.Navigation("TabReports");
                });

            modelBuilder.Entity("EzTabs.Model.User", b =>
                {
                    b.Navigation("CommentRates");

                    b.Navigation("CommentReports");

                    b.Navigation("Comments");

                    b.Navigation("FavouriteTabs");

                    b.Navigation("Notifications");

                    b.Navigation("Tab");

                    b.Navigation("TabRates");

                    b.Navigation("TabReports");
                });
#pragma warning restore 612, 618
        }
    }
}

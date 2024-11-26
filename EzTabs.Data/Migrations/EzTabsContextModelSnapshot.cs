﻿// <auto-generated />
using System;
using EzTabs.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EzTabs.Data.Migrations
{
    [DbContext(typeof(EzTabsContext))]
    partial class EzTabsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("EzTabs.Data.Domain.Comment", b =>
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

            modelBuilder.Entity("EzTabs.Data.Domain.CommentRate", b =>
                {
                    b.Property<Guid>("CommentId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("Rate")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("CommentId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("CommentRate");
                });

            modelBuilder.Entity("EzTabs.Data.Domain.CommentReport", b =>
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

            modelBuilder.Entity("EzTabs.Data.Domain.FavouriteTab", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TabId")
                        .HasColumnType("char(36)");

                    b.HasKey("UserId", "TabId");

                    b.HasIndex("TabId");

                    b.ToTable("FavouriteTab");
                });

            modelBuilder.Entity("EzTabs.Data.Domain.Notification", b =>
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

            modelBuilder.Entity("EzTabs.Data.Domain.Tab", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("AuthorId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Band")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("BitsPerMinute")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TabText")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("Views")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Tabs");
                });

            modelBuilder.Entity("EzTabs.Data.Domain.TabRate", b =>
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

            modelBuilder.Entity("EzTabs.Data.Domain.TabReport", b =>
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

            modelBuilder.Entity("EzTabs.Data.Domain.Tuning", b =>
                {
                    b.Property<Guid?>("TabId")
                        .HasColumnType("char(36)");

                    b.Property<int>("StringOrder")
                        .HasColumnType("int");

                    b.Property<string>("StringNote")
                        .HasColumnType("longtext");

                    b.HasKey("TabId", "StringOrder");

                    b.ToTable("Tuning");
                });

            modelBuilder.Entity("EzTabs.Data.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsEmailVerified")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("VerificationCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EzTabs.Data.Domain.Comment", b =>
                {
                    b.HasOne("EzTabs.Data.Domain.Tab", "Tab")
                        .WithMany("Comments")
                        .HasForeignKey("TabId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EzTabs.Data.Domain.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId");

                    b.Navigation("Tab");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EzTabs.Data.Domain.CommentRate", b =>
                {
                    b.HasOne("EzTabs.Data.Domain.Comment", "Comment")
                        .WithMany("CommentRates")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EzTabs.Data.Domain.User", "User")
                        .WithMany("CommentRates")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EzTabs.Data.Domain.CommentReport", b =>
                {
                    b.HasOne("EzTabs.Data.Domain.Comment", "Comment")
                        .WithMany("CommentReports")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EzTabs.Data.Domain.User", "User")
                        .WithMany("CommentReports")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EzTabs.Data.Domain.FavouriteTab", b =>
                {
                    b.HasOne("EzTabs.Data.Domain.Tab", "Tab")
                        .WithMany("FavouriteTabs")
                        .HasForeignKey("TabId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EzTabs.Data.Domain.User", "User")
                        .WithMany("FavouriteTabs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tab");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EzTabs.Data.Domain.Notification", b =>
                {
                    b.HasOne("EzTabs.Data.Domain.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EzTabs.Data.Domain.Tab", b =>
                {
                    b.HasOne("EzTabs.Data.Domain.User", "Author")
                        .WithMany("Tab")
                        .HasForeignKey("AuthorId");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("EzTabs.Data.Domain.TabRate", b =>
                {
                    b.HasOne("EzTabs.Data.Domain.Tab", "Tab")
                        .WithMany("TabRates")
                        .HasForeignKey("TabId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EzTabs.Data.Domain.User", "User")
                        .WithMany("TabRates")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tab");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EzTabs.Data.Domain.TabReport", b =>
                {
                    b.HasOne("EzTabs.Data.Domain.Tab", "Tab")
                        .WithMany("TabReports")
                        .HasForeignKey("TabId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EzTabs.Data.Domain.User", "User")
                        .WithMany("TabReports")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tab");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EzTabs.Data.Domain.Tuning", b =>
                {
                    b.HasOne("EzTabs.Data.Domain.Tab", "Tab")
                        .WithMany("Tunings")
                        .HasForeignKey("TabId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tab");
                });

            modelBuilder.Entity("EzTabs.Data.Domain.Comment", b =>
                {
                    b.Navigation("CommentRates");

                    b.Navigation("CommentReports");
                });

            modelBuilder.Entity("EzTabs.Data.Domain.Tab", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("FavouriteTabs");

                    b.Navigation("TabRates");

                    b.Navigation("TabReports");

                    b.Navigation("Tunings");
                });

            modelBuilder.Entity("EzTabs.Data.Domain.User", b =>
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

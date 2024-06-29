﻿// <auto-generated />
using System;
using ADStarter.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ADStarter.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ADStarter.Models.Admin", b =>
                {
                    b.Property<int>("a_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("a_ID"));

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("a_name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("a_ID");

                    b.HasIndex("UserId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("ADStarter.Models.Announcement", b =>
                {
                    b.Property<int>("ann_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ann_ID"));

                    b.Property<int>("a_ID")
                        .HasColumnType("int");

                    b.Property<string>("ann_desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ann_media")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ann_title")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ann_ID");

                    b.HasIndex("a_ID");

                    b.ToTable("Announcements");
                });

            modelBuilder.Entity("ADStarter.Models.Child", b =>
                {
                    b.Property<string>("c_myKid")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("c_age")
                        .HasColumnType("int");

                    b.Property<DateTime>("c_dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("c_gender")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("c_name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("c_nationality")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("c_photo")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("c_race")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("c_religion")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("c_step")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("parent_ID")
                        .HasColumnType("int");

                    b.Property<int?>("t_ID")
                        .HasColumnType("int");

                    b.HasKey("c_myKid");

                    b.HasIndex("parent_ID");

                    b.HasIndex("t_ID");

                    b.ToTable("Children");
                });

            modelBuilder.Entity("ADStarter.Models.CustomerService", b =>
                {
                    b.Property<int>("cs_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("cs_ID"));

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("cs_name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("cs_ID");

                    b.HasIndex("UserId");

                    b.ToTable("CustomerServices");
                });

            modelBuilder.Entity("ADStarter.Models.Invoice", b =>
                {
                    b.Property<int>("invoice_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("invoice_ID"));

                    b.Property<string>("c_myKid")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("due_date")
                        .HasColumnType("datetime2");

                    b.Property<double>("invoice_amount")
                        .HasColumnType("float");

                    b.Property<int>("schedule_ID")
                        .HasColumnType("int");

                    b.HasKey("invoice_ID");

                    b.HasIndex("c_myKid");

                    b.HasIndex("schedule_ID");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("ADStarter.Models.Parent", b =>
                {
                    b.Property<int>("parent_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("parent_ID"));

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("f_ID")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("f_Waddress")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("f_address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("f_email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("f_name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("f_occupation")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("f_phoneNum")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("f_race")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("f_status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("fm_income")
                        .HasColumnType("float");

                    b.Property<string>("m_ID")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("m_Waddress")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("m_address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("m_email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("m_name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("m_phoneNum")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("m_race")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("m_status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("parent_ID");

                    b.ToTable("Parents");
                });

            modelBuilder.Entity("ADStarter.Models.Payment", b =>
                {
                    b.Property<int>("pay_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("pay_ID"));

                    b.Property<int?>("Therapistt_ID")
                        .HasColumnType("int");

                    b.Property<int>("a_ID")
                        .HasColumnType("int");

                    b.Property<string>("c_myKid")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("invoice_ID")
                        .HasColumnType("int");

                    b.Property<int>("parent_ID")
                        .HasColumnType("int");

                    b.Property<double>("pay_amount")
                        .HasColumnType("float");

                    b.Property<double>("pay_balance")
                        .HasColumnType("float");

                    b.Property<DateTime>("pay_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("pay_desc")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("receipt_id")
                        .HasColumnType("int");

                    b.HasKey("pay_ID");

                    b.HasIndex("Therapistt_ID");

                    b.HasIndex("a_ID");

                    b.HasIndex("c_myKid");

                    b.HasIndex("invoice_ID");

                    b.HasIndex("parent_ID");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("ADStarter.Models.Program", b =>
                {
                    b.Property<int>("prog_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("prog_ID"));

                    b.Property<string>("prog_desc")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("prog_name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("prog_price")
                        .HasColumnType("float");

                    b.Property<int>("prog_step")
                        .HasColumnType("int");

                    b.Property<string>("prog_summary")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("prog_ID");

                    b.ToTable("Programs");
                });

            modelBuilder.Entity("ADStarter.Models.Report", b =>
                {
                    b.Property<int>("rep_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("rep_ID"));

                    b.Property<string>("c_myKid")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("rep_datetime")
                        .HasColumnType("datetime2");

                    b.Property<string>("rep_title")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("t_ID")
                        .HasColumnType("int");

                    b.HasKey("rep_ID");

                    b.HasIndex("c_myKid");

                    b.HasIndex("t_ID");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("ADStarter.Models.Schedule", b =>
                {
                    b.Property<int>("schedule_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("schedule_ID"));

                    b.Property<int?>("SessionPricesession_ID")
                        .HasColumnType("int");

                    b.Property<string>("c_myKid")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("prog_ID")
                        .HasColumnType("int");

                    b.Property<int>("session_ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("session_datetime")
                        .HasColumnType("datetime2");

                    b.Property<int>("slot_ID")
                        .HasColumnType("int");

                    b.Property<double>("slot_price")
                        .HasColumnType("float");

                    b.Property<int>("t_ID")
                        .HasColumnType("int");

                    b.HasKey("schedule_ID");

                    b.HasIndex("SessionPricesession_ID");

                    b.HasIndex("c_myKid");

                    b.HasIndex("prog_ID");

                    b.HasIndex("session_ID");

                    b.HasIndex("slot_ID");

                    b.HasIndex("t_ID");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("ADStarter.Models.SessionPrice", b =>
                {
                    b.Property<int>("session_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("session_ID"));

                    b.Property<int>("prog_ID")
                        .HasColumnType("int");

                    b.Property<int>("session_bilangan")
                        .HasColumnType("int");

                    b.Property<string>("session_day")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("session_name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("sp_price")
                        .HasColumnType("float");

                    b.HasKey("session_ID");

                    b.HasIndex("prog_ID");

                    b.ToTable("SessionPrices");
                });

            modelBuilder.Entity("ADStarter.Models.Slot", b =>
                {
                    b.Property<int>("slot_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("slot_ID"));

                    b.Property<string>("slot_time")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("slot_ID");

                    b.ToTable("Slots");
                });

            modelBuilder.Entity("ADStarter.Models.Therapist", b =>
                {
                    b.Property<int>("t_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("t_ID"));

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("t_address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("t_name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("t_phoneNum")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("t_ID");

                    b.HasIndex("UserId");

                    b.ToTable("Therapists");
                });

            modelBuilder.Entity("ADStarter.Models.TreatmentHistory", b =>
                {
                    b.Property<string>("c_myKid")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("th_deadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("th_diagnosis")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("th_pediatrician")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("th_prevTherapy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("th_recommendation")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("c_myKid");

                    b.ToTable("TreatmentHistories");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ADStarter.Models.Admin", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ADStarter.Models.Announcement", b =>
                {
                    b.HasOne("ADStarter.Models.Admin", "Admin")
                        .WithMany("Announcements")
                        .HasForeignKey("a_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("ADStarter.Models.Child", b =>
                {
                    b.HasOne("ADStarter.Models.Parent", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("parent_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ADStarter.Models.Therapist", "Therapist")
                        .WithMany()
                        .HasForeignKey("t_ID");

                    b.Navigation("Parent");

                    b.Navigation("Therapist");
                });

            modelBuilder.Entity("ADStarter.Models.CustomerService", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ADStarter.Models.Invoice", b =>
                {
                    b.HasOne("ADStarter.Models.Child", "Child")
                        .WithMany("Invoices")
                        .HasForeignKey("c_myKid");

                    b.HasOne("ADStarter.Models.Schedule", "Schedule")
                        .WithMany("Invoices")
                        .HasForeignKey("schedule_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Child");

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("ADStarter.Models.Payment", b =>
                {
                    b.HasOne("ADStarter.Models.Therapist", null)
                        .WithMany("Payments")
                        .HasForeignKey("Therapistt_ID");

                    b.HasOne("ADStarter.Models.Admin", "Admin")
                        .WithMany("Payments")
                        .HasForeignKey("a_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ADStarter.Models.Child", "Child")
                        .WithMany("Payments")
                        .HasForeignKey("c_myKid")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ADStarter.Models.Invoice", "Invoice")
                        .WithMany("Payments")
                        .HasForeignKey("invoice_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ADStarter.Models.Parent", "Parent")
                        .WithMany()
                        .HasForeignKey("parent_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");

                    b.Navigation("Child");

                    b.Navigation("Invoice");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("ADStarter.Models.Report", b =>
                {
                    b.HasOne("ADStarter.Models.Child", "Child")
                        .WithMany("Reports")
                        .HasForeignKey("c_myKid")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ADStarter.Models.Therapist", "Therapist")
                        .WithMany("Reports")
                        .HasForeignKey("t_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Child");

                    b.Navigation("Therapist");
                });

            modelBuilder.Entity("ADStarter.Models.Schedule", b =>
                {
                    b.HasOne("ADStarter.Models.SessionPrice", null)
                        .WithMany("Schedules")
                        .HasForeignKey("SessionPricesession_ID");

                    b.HasOne("ADStarter.Models.Child", "Child")
                        .WithMany("Schedules")
                        .HasForeignKey("c_myKid")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ADStarter.Models.Program", "Program")
                        .WithMany("Schedules")
                        .HasForeignKey("prog_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ADStarter.Models.SessionPrice", "SessionPrice")
                        .WithMany()
                        .HasForeignKey("session_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ADStarter.Models.Slot", "Slot")
                        .WithMany("Schedules")
                        .HasForeignKey("slot_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ADStarter.Models.Therapist", "Therapist")
                        .WithMany("Schedules")
                        .HasForeignKey("t_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Child");

                    b.Navigation("Program");

                    b.Navigation("SessionPrice");

                    b.Navigation("Slot");

                    b.Navigation("Therapist");
                });

            modelBuilder.Entity("ADStarter.Models.SessionPrice", b =>
                {
                    b.HasOne("ADStarter.Models.Program", "Program")
                        .WithMany("SessionPrices")
                        .HasForeignKey("prog_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Program");
                });

            modelBuilder.Entity("ADStarter.Models.Therapist", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ADStarter.Models.TreatmentHistory", b =>
                {
                    b.HasOne("ADStarter.Models.Child", "Child")
                        .WithOne("TreatmentHistory")
                        .HasForeignKey("ADStarter.Models.TreatmentHistory", "c_myKid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Child");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ADStarter.Models.Admin", b =>
                {
                    b.Navigation("Announcements");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("ADStarter.Models.Child", b =>
                {
                    b.Navigation("Invoices");

                    b.Navigation("Payments");

                    b.Navigation("Reports");

                    b.Navigation("Schedules");

                    b.Navigation("TreatmentHistory");
                });

            modelBuilder.Entity("ADStarter.Models.Invoice", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("ADStarter.Models.Parent", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("ADStarter.Models.Program", b =>
                {
                    b.Navigation("Schedules");

                    b.Navigation("SessionPrices");
                });

            modelBuilder.Entity("ADStarter.Models.Schedule", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("ADStarter.Models.SessionPrice", b =>
                {
                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("ADStarter.Models.Slot", b =>
                {
                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("ADStarter.Models.Therapist", b =>
                {
                    b.Navigation("Payments");

                    b.Navigation("Reports");

                    b.Navigation("Schedules");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SFAccountingSystem.Core;

#nullable disable

namespace SFAccountingSystem.Core.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230613225021_AddApprovedAtInvoice")]
    partial class AddApprovedAtInvoice
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SFAccountingSystem.Core.Models.Intermediation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("InvoiceUserIds")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Intermediation");
                });

            modelBuilder.Entity("SFAccountingSystem.Core.Models.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("ApprovedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IntermediationId")
                        .HasColumnType("int");

                    b.Property<string>("NrNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("IntermediationId");

                    b.HasIndex("UserId");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("SFAccountingSystem.Core.Models.RecordOFX", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("ApprovedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Bank")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FITID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Group")
                        .HasColumnType("int");

                    b.Property<int?>("IntermediationId")
                        .HasColumnType("int");

                    b.Property<int?>("SubGroupId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("IntermediationId");

                    b.HasIndex("SubGroupId");

                    b.HasIndex("UserId");

                    b.ToTable("RecordOFX");
                });

            modelBuilder.Entity("SFAccountingSystem.Core.Models.RecordOFXSubGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Group")
                        .HasColumnType("int");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("RecordOFXSubGroups");
                });

            modelBuilder.Entity("SFAccountingSystem.Core.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CpfCnpj")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Entity")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SFAccountingSystem.Core.Models.Intermediation", b =>
                {
                    b.HasOne("SFAccountingSystem.Core.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SFAccountingSystem.Core.Models.Invoice", b =>
                {
                    b.HasOne("SFAccountingSystem.Core.Models.Intermediation", "Intermediation")
                        .WithMany("Invoices")
                        .HasForeignKey("IntermediationId");

                    b.HasOne("SFAccountingSystem.Core.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Intermediation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SFAccountingSystem.Core.Models.RecordOFX", b =>
                {
                    b.HasOne("SFAccountingSystem.Core.Models.Intermediation", "Intermediation")
                        .WithMany("RecordsOFXes")
                        .HasForeignKey("IntermediationId");

                    b.HasOne("SFAccountingSystem.Core.Models.RecordOFXSubGroup", "RecordOFXSubGroup")
                        .WithMany("RecordsOFXes")
                        .HasForeignKey("SubGroupId");

                    b.HasOne("SFAccountingSystem.Core.Models.User", "User")
                        .WithMany("RecordsOFXes")
                        .HasForeignKey("UserId");

                    b.Navigation("Intermediation");

                    b.Navigation("RecordOFXSubGroup");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SFAccountingSystem.Core.Models.RecordOFXSubGroup", b =>
                {
                    b.HasOne("SFAccountingSystem.Core.Models.RecordOFXSubGroup", "Parent")
                        .WithMany("ChildRecordsOFXes")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("SFAccountingSystem.Core.Models.Intermediation", b =>
                {
                    b.Navigation("Invoices");

                    b.Navigation("RecordsOFXes");
                });

            modelBuilder.Entity("SFAccountingSystem.Core.Models.RecordOFXSubGroup", b =>
                {
                    b.Navigation("ChildRecordsOFXes");

                    b.Navigation("RecordsOFXes");
                });

            modelBuilder.Entity("SFAccountingSystem.Core.Models.User", b =>
                {
                    b.Navigation("RecordsOFXes");
                });
#pragma warning restore 612, 618
        }
    }
}

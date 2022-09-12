﻿// <auto-generated />
using HousingRepairsOnlineApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HousingRepairsOnlineApi.Migrations
{
    [DbContext(typeof(RepairContext))]
    [Migration("20220912091113_Repairs")]
    partial class Repairs
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HousingRepairsOnlineApi.Domain.Repair", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<RepairAddress>("Address")
                        .HasColumnType("jsonb")
                        .HasColumnName("address");

                    b.Property<RepairContactDetails>("ContactDetails")
                        .HasColumnType("jsonb")
                        .HasColumnName("contact_details");

                    b.Property<string>("ContactPersonNumber")
                        .HasColumnType("text")
                        .HasColumnName("contact_person_number");

                    b.Property<RepairDescription>("Description")
                        .HasColumnType("jsonb")
                        .HasColumnName("description");

                    b.Property<RepairIssue>("Issue")
                        .HasColumnType("jsonb")
                        .HasColumnName("issue");

                    b.Property<RepairLocation>("Location")
                        .HasColumnType("jsonb")
                        .HasColumnName("location");

                    b.Property<string>("Postcode")
                        .HasColumnType("text")
                        .HasColumnName("postcode");

                    b.Property<RepairProblem>("Problem")
                        .HasColumnType("jsonb")
                        .HasColumnName("problem");

                    b.Property<string>("SOR")
                        .HasColumnType("text")
                        .HasColumnName("sor");

                    b.Property<RepairAvailability>("Time")
                        .HasColumnType("jsonb")
                        .HasColumnName("time");

                    b.HasKey("Id")
                        .HasName("pk_repair");

                    b.ToTable("repair", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
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
    [Migration("20221026150225_CreatedAtAudit")]
    partial class CreatedAtAudit
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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<RepairAddress>("Address")
                        .HasColumnType("jsonb")
                        .HasColumnName("address");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("now()");

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

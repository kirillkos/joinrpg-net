﻿// <auto-generated />
using System;
using JoinRpg.Dal.JobService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JoinRpg.Dal.JobService.Migrations
{
    [DbContext(typeof(JobScheduleDataDbContext))]
    partial class JobScheduleDataDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("JoinRpg.Dal.JobService.DailyJobRun", b =>
                {
                    b.Property<int>("DailyJobRunId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("DailyJobRunId"));

                    b.Property<DateOnly>("DayOfRun")
                        .HasMaxLength(1024)
                        .HasColumnType("date");

                    b.Property<string>("JobName")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)");

                    b.Property<int>("JobStatus")
                        .HasColumnType("integer");

                    b.Property<string>("MachineName")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)");

                    b.HasKey("DailyJobRunId");

                    b.HasIndex("JobName", "DayOfRun")
                        .IsUnique();

                    b.ToTable("DailyJobRuns");
                });
#pragma warning restore 612, 618
        }
    }
}
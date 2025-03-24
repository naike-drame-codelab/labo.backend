﻿// <auto-generated />
using System;
using Labo.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Labo.Infrastructure.Migrations
{
    [DbContext(typeof(LaboContext))]
    partial class LaboContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Labo.Domain.Entities.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Elo")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<Guid>("Salt")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Salt")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Members", t =>
                        {
                            t.HasCheckConstraint("CK_ELO", "Elo BETWEEN 0 AND 3000");
                        });

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BirthDate = new DateTime(1982, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Elo = 1500,
                            Email = "lykhun@gmail.com",
                            Gender = 1,
                            Password = "��O)�a��=o�bz�\n٠į8����P����\n'�:�Q-n��h��p�Ɋ����>A��",
                            Role = 1,
                            Salt = new Guid("00000000-0000-0000-0000-000000000000"),
                            Username = "Checkmate"
                        });
                });

            modelBuilder.Entity("Labo.Domain.Entities.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.PrimitiveCollection<string>("Categories")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("CurrentRound")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<DateTime>("EndOfRegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastUpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MaxElo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(3000);

                    b.Property<int>("MaxPlayers")
                        .HasColumnType("int");

                    b.Property<int?>("MinElo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("MinPlayers")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<bool>("WomenOnly")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Tournaments", t =>
                        {
                            t.HasCheckConstraint("CK_Tournament_EndOfRegistrationDate", "EndOfRegistrationDate > DATEADD(day, MinPlayers, GETDATE())");

                            t.HasCheckConstraint("CK_Tournament_MaxElo", "MaxElo >= 0 AND MaxElo <= 3000");

                            t.HasCheckConstraint("CK_Tournament_MaxPlayers", "MaxPlayers BETWEEN 2 AND 32");

                            t.HasCheckConstraint("CK_Tournament_MinElo", "MinElo >= 0 AND MinElo <= 3000");

                            t.HasCheckConstraint("CK_Tournament_MinPlayers", "MinPlayers BETWEEN 2 AND 32");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using MOTILab4.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MOTILab4.Migrations
{
    [DbContext(typeof(MOTILab4Context))]
    [Migration("20211220122351_f")]
    partial class f
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MOTILab4.Models.Elector", b =>
                {
                    b.Property<int>("ElectorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ElectorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ElectorRatio")
                        .HasColumnType("int");

                    b.HasKey("ElectorId");

                    b.ToTable("Elector");
                });

            modelBuilder.Entity("MOTILab4.Models.ElectorChoise", b =>
                {
                    b.Property<int>("ElectorChoiseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ElectorId")
                        .HasColumnType("int");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int>("VotingObjectId")
                        .HasColumnType("int");

                    b.HasKey("ElectorChoiseId");

                    b.HasIndex("ElectorId");

                    b.HasIndex("VotingObjectId");

                    b.ToTable("ElectorChoise");
                });

            modelBuilder.Entity("MOTILab4.Models.VotingObject", b =>
                {
                    b.Property<int>("VotingObjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("VotingObjectName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VotingObjectId");

                    b.ToTable("VotingObject");
                });

            modelBuilder.Entity("MOTILab4.Models.ElectorChoise", b =>
                {
                    b.HasOne("MOTILab4.Models.Elector", "Elector")
                        .WithMany("ElectorChoise")
                        .HasForeignKey("ElectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MOTILab4.Models.VotingObject", null)
                        .WithMany("ElectorChoise")
                        .HasForeignKey("VotingObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Elector");
                });

            modelBuilder.Entity("MOTILab4.Models.Elector", b =>
                {
                    b.Navigation("ElectorChoise");
                });

            modelBuilder.Entity("MOTILab4.Models.VotingObject", b =>
                {
                    b.Navigation("ElectorChoise");
                });
#pragma warning restore 612, 618
        }
    }
}

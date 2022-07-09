﻿// <auto-generated />
using System;
using ClientApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ClientApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.6");

            modelBuilder.Entity("BackEndProject.Models.Event_Participans", b =>
                {
                    b.Property<int>("EventId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("EventId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Event_Participans");
                });

            modelBuilder.Entity("ClientApi.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("OrganiserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlaygroundId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OrganiserId");

                    b.HasIndex("PlaygroundId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("ClientApi.Models.Playground", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Capacity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CordinatesX")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CordinatesY")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Playgrounds");
                });

            modelBuilder.Entity("ClientApi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BackEndProject.Models.Event_Participans", b =>
                {
                    b.HasOne("ClientApi.Models.Event", "Event")
                        .WithMany("Event_Participans")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClientApi.Models.User", "User")
                        .WithMany("Event_Participans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ClientApi.Models.Event", b =>
                {
                    b.HasOne("ClientApi.Models.User", "Organiser")
                        .WithMany()
                        .HasForeignKey("OrganiserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClientApi.Models.Playground", "Playground")
                        .WithMany()
                        .HasForeignKey("PlaygroundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organiser");

                    b.Navigation("Playground");
                });

            modelBuilder.Entity("ClientApi.Models.Event", b =>
                {
                    b.Navigation("Event_Participans");
                });

            modelBuilder.Entity("ClientApi.Models.User", b =>
                {
                    b.Navigation("Event_Participans");
                });
#pragma warning restore 612, 618
        }
    }
}

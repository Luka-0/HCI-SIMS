﻿// <auto-generated />
using System;
using InitialProject.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InitialProject.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20230315225511_peta")]
    partial class peta
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("InitialProject.Model.Accommodation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Available")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CancellationDeadline")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GuestLimit")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MinimumReservationDays")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("locationID")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("locationID");

                    b.ToTable("Accommodation");
                });

            modelBuilder.Entity("InitialProject.Model.AccommodationReservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccommodationId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BegginingDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndingDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("GuestId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GuestNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AccommodationId");

                    b.HasIndex("GuestId");

                    b.ToTable("AccommodationReservation");
                });

            modelBuilder.Entity("InitialProject.Model.GuestGrade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Obedience")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Tidiness")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("GuestGrade");
                });

            modelBuilder.Entity("InitialProject.Model.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AccommodationId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TourId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccommodationId");

                    b.HasIndex("TourId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("InitialProject.Model.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("InitialProject.Model.Tour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<TimeOnly>("Duration")
                        .HasColumnType("TEXT");

                    b.Property<int>("GuestLimit")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDateAndTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Started")
                        .HasColumnType("INTEGER");

                    b.Property<int>("locationID")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("locationID");

                    b.ToTable("Tour");
                });

            modelBuilder.Entity("InitialProject.Model.TourKeyPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<int>("tourID")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("tourID");

                    b.ToTable("TourKeyPoint");
                });

            modelBuilder.Entity("InitialProject.Model.TourReservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookingGuestId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GuestNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TourId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BookingGuestId");

                    b.HasIndex("TourId");

                    b.ToTable("tourReservations");
                });

            modelBuilder.Entity("InitialProject.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("InitialProject.Model.Accommodation", b =>
                {
                    b.HasOne("InitialProject.Model.Location", "Location")
                        .WithMany()
                        .HasForeignKey("locationID");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("InitialProject.Model.AccommodationReservation", b =>
                {
                    b.HasOne("InitialProject.Model.Accommodation", "Accommodation")
                        .WithMany()
                        .HasForeignKey("AccommodationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InitialProject.Model.User", "Guest")
                        .WithMany()
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Accommodation");

                    b.Navigation("Guest");
                });

            modelBuilder.Entity("InitialProject.Model.Image", b =>
                {
                    b.HasOne("InitialProject.Model.Accommodation", "Accommodation")
                        .WithMany("images")
                        .HasForeignKey("AccommodationId");

                    b.HasOne("InitialProject.Model.Tour", "Tour")
                        .WithMany("images")
                        .HasForeignKey("TourId");

                    b.Navigation("Accommodation");

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("InitialProject.Model.Tour", b =>
                {
                    b.HasOne("InitialProject.Model.Location", "Location")
                        .WithMany()
                        .HasForeignKey("locationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("InitialProject.Model.TourKeyPoint", b =>
                {
                    b.HasOne("InitialProject.Model.Tour", "Tour")
                        .WithMany("TourKeyPoints")
                        .HasForeignKey("tourID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("InitialProject.Model.TourReservation", b =>
                {
                    b.HasOne("InitialProject.Model.User", "BookingGuest")
                        .WithMany()
                        .HasForeignKey("BookingGuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InitialProject.Model.Tour", "Tour")
                        .WithMany()
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookingGuest");

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("InitialProject.Model.Accommodation", b =>
                {
                    b.Navigation("images");
                });

            modelBuilder.Entity("InitialProject.Model.Tour", b =>
                {
                    b.Navigation("TourKeyPoints");

                    b.Navigation("images");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(CongressContext))]
    [Migration("20250317105248_ulr access")]
    partial class ulraccess
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Attendance", b =>
                {
                    b.Property<int>("AttendanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("AttendanceId"));

                    b.Property<int>("AttendeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ExposureId")
                        .HasColumnType("int");

                    b.HasKey("AttendanceId");

                    b.HasIndex("AttendeeId");

                    b.HasIndex("ExposureId");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("Domain.Entities.Attendee", b =>
                {
                    b.Property<int>("AttendeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("AttendeeId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("IDNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Institution")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("AttendeeId");

                    b.ToTable("Attendees");
                });

            modelBuilder.Entity("Domain.Entities.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("AuthorId"));

                    b.Property<int>("AcademicDegree")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("IDNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("InstitutionalMail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PersonalMail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("AuthorId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Domain.Entities.Congress", b =>
                {
                    b.Property<int>("CongressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("CongressId"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("EndDateNotificationApprove")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("EndDateRegistrationAttendee")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("EndDateRegistrationExposure")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Guid")
                        .HasColumnType("longtext");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("MinHours")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("fileCertificateAttendance")
                        .HasColumnType("longtext");

                    b.Property<string>("fileCertificateConference")
                        .HasColumnType("longtext");

                    b.Property<string>("fileCertificateExposure")
                        .HasColumnType("longtext");

                    b.Property<string>("fileFlayer")
                        .HasColumnType("longtext");

                    b.HasKey("CongressId");

                    b.ToTable("Congresses");
                });

            modelBuilder.Entity("Domain.Entities.Exposure", b =>
                {
                    b.Property<int>("ExposureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ExposureId"));

                    b.Property<int>("CongressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Guid")
                        .HasColumnType("longtext");

                    b.Property<string>("GuidCert")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Observation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Presented")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ResearchLine")
                        .HasColumnType("int");

                    b.Property<int?>("RoomId")
                        .HasColumnType("int");

                    b.Property<int>("StatusExposure")
                        .HasColumnType("int");

                    b.Property<string>("SummaryFilePath")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("UrlAccess")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ExposureId");

                    b.HasIndex("CongressId");

                    b.HasIndex("RoomId");

                    b.ToTable("Exposures");
                });

            modelBuilder.Entity("Domain.Entities.ExposureAuthor", b =>
                {
                    b.Property<int>("ExposureAuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ExposureAuthorId"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int>("ExposureId")
                        .HasColumnType("int");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.HasKey("ExposureAuthorId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ExposureId");

                    b.ToTable("ExposureAuthors");
                });

            modelBuilder.Entity("Domain.Entities.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("RoomId"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("CongressId")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("RoomId");

                    b.HasIndex("CongressId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.Attendance", b =>
                {
                    b.HasOne("Domain.Entities.Attendee", "Attendee")
                        .WithMany("Attendances")
                        .HasForeignKey("AttendeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Exposure", "Exposure")
                        .WithMany("Attendances")
                        .HasForeignKey("ExposureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attendee");

                    b.Navigation("Exposure");
                });

            modelBuilder.Entity("Domain.Entities.Exposure", b =>
                {
                    b.HasOne("Domain.Entities.Congress", "Congress")
                        .WithMany()
                        .HasForeignKey("CongressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Room", "Room")
                        .WithMany("Exposures")
                        .HasForeignKey("RoomId");

                    b.Navigation("Congress");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Domain.Entities.ExposureAuthor", b =>
                {
                    b.HasOne("Domain.Entities.Author", "Author")
                        .WithMany("ExposureAuthor")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Exposure", "Exposure")
                        .WithMany("ExposureAuthor")
                        .HasForeignKey("ExposureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Exposure");
                });

            modelBuilder.Entity("Domain.Entities.Room", b =>
                {
                    b.HasOne("Domain.Entities.Congress", "Congress")
                        .WithMany("Rooms")
                        .HasForeignKey("CongressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Congress");
                });

            modelBuilder.Entity("Domain.Entities.Attendee", b =>
                {
                    b.Navigation("Attendances");
                });

            modelBuilder.Entity("Domain.Entities.Author", b =>
                {
                    b.Navigation("ExposureAuthor");
                });

            modelBuilder.Entity("Domain.Entities.Congress", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("Domain.Entities.Exposure", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("ExposureAuthor");
                });

            modelBuilder.Entity("Domain.Entities.Room", b =>
                {
                    b.Navigation("Exposures");
                });
#pragma warning restore 612, 618
        }
    }
}

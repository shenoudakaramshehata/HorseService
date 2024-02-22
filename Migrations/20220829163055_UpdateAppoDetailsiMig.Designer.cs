﻿// <auto-generated />
using System;
using HorseService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HorseService.Migrations
{
    [DbContext(typeof(HorseServiceContext))]
    [Migration("20220829163055_UpdateAppoDetailsiMig")]
    partial class UpdateAppoDetailsiMig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HorseService.Models.AdditionalType", b =>
                {
                    b.Property<int>("AdditionalTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdditionalTypeId");

                    b.ToTable("AdditionalTypes");
                });

            modelBuilder.Entity("HorseService.Models.AppointmentAdditionalTypes", b =>
                {
                    b.Property<int>("AppointmentAdditionalTypesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdditionalTypeId")
                        .HasColumnType("int");

                    b.Property<int>("AppointmentsId")
                        .HasColumnType("int");

                    b.HasKey("AppointmentAdditionalTypesId");

                    b.HasIndex("AdditionalTypeId");

                    b.HasIndex("AppointmentsId");

                    b.ToTable("AppointmentAdditionalTypes");
                });

            modelBuilder.Entity("HorseService.Models.AppointmentDetails", b =>
                {
                    b.Property<int>("AppointmentDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalTypes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AppointmentsId")
                        .HasColumnType("int");

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.Property<int>("NumberOfHorses")
                        .HasColumnType("int");

                    b.Property<int?>("ServiceId")
                        .HasColumnType("int");

                    b.Property<double>("TotalAdditionalCost")
                        .HasColumnType("float");

                    b.HasKey("AppointmentDetailsId");

                    b.HasIndex("AppointmentsId");

                    b.HasIndex("ServiceId");

                    b.ToTable("AppointmentDetails");
                });

            modelBuilder.Entity("HorseService.Models.AppointmentStatus", b =>
                {
                    b.Property<int>("AppointmentStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppointmentStatusTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AppointmentStatusId");

                    b.ToTable("AppointmentStatuses");
                });

            modelBuilder.Entity("HorseService.Models.Appointments", b =>
                {
                    b.Property<int>("AppointmentsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AppointmentStatusId")
                        .HasColumnType("int");

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.Property<string>("CustomerAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("Lat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lng")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberofHorses")
                        .HasColumnType("int");

                    b.Property<string>("OrderSerialNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PaymentMethodId")
                        .HasColumnType("int");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TimeTowill")
                        .HasColumnType("datetime2");

                    b.Property<bool>("ispaid")
                        .HasColumnType("bit");

                    b.Property<string>("payment_type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AppointmentsId");

                    b.HasIndex("AppointmentStatusId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("PaymentMethodId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("HorseService.Models.BreakTypes", b =>
                {
                    b.Property<int>("breaktypesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("breaktypesId");

                    b.ToTable("BreakTypes");

                    b.HasData(
                        new
                        {
                            breaktypesId = 1,
                            Title = "Hourly"
                        },
                        new
                        {
                            breaktypesId = 2,
                            Title = "Daily"
                        },
                        new
                        {
                            breaktypesId = 3,
                            Title = "Periodly"
                        });
                });

            modelBuilder.Entity("HorseService.Models.Configuration", b =>
                {
                    b.Property<int>("ConfigurationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.HasKey("ConfigurationId");

                    b.ToTable("Configurations");

                    b.HasData(
                        new
                        {
                            ConfigurationId = 1,
                            Cost = 100.0
                        });
                });

            modelBuilder.Entity("HorseService.Models.ContactForm", b =>
                {
                    b.Property<int>("ContactFormId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContactFormId");

                    b.ToTable("ContactForms");
                });

            modelBuilder.Entity("HorseService.Models.ContactUs", b =>
                {
                    b.Property<int>("ContactUsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Facebook")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fax")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Instgram")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LinkedIn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tele")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Twitter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhatsApp")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContactUsId");

                    b.ToTable("ContactUs");

                    b.HasData(
                        new
                        {
                            ContactUsId = 1,
                            Address = "Kwait",
                            CompanyName = "Codeware",
                            Email = "codeware@gmail.com",
                            Facebook = "https://www.facebook.com/",
                            Fax = "d23",
                            Instgram = "https://www.instagram.com/",
                            LinkedIn = "https://www.linkedin.com/feed/",
                            Mobile = "01091117381",
                            Tele = "2090555",
                            Twitter = "https://twitter.com/",
                            WhatsApp = "01091117381"
                        });
                });

            modelBuilder.Entity("HorseService.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CustomerNameEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("HorseService.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("HorseService.Models.HomeSlider", b =>
                {
                    b.Property<int>("HomeSliderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("HomeSliderPic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HomeSliderId");

                    b.ToTable("HomeSliders");
                });

            modelBuilder.Entity("HorseService.Models.OffDays", b =>
                {
                    b.Property<int>("OffDaysId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("From")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Onday")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("To")
                        .HasColumnType("datetime2");

                    b.Property<int>("breaktypesId")
                        .HasColumnType("int");

                    b.HasKey("OffDaysId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("breaktypesId");

                    b.ToTable("OffDays");
                });

            modelBuilder.Entity("HorseService.Models.PageContent", b =>
                {
                    b.Property<int>("PageContentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContentAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PageTitleAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PageTitleEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PageContentId");

                    b.ToTable("PageContents");

                    b.HasData(
                        new
                        {
                            PageContentId = 1,
                            ContentAr = "هذا النص هو مثال لنص يمكن ان يستبدل في نفس المساحة",
                            ContentEn = "This text is an example of text that can be replaced in the same space",
                            PageTitleAr = "من نحن ",
                            PageTitleEn = "About Us"
                        });
                });

            modelBuilder.Entity("HorseService.Models.PaymentMethod", b =>
                {
                    b.Property<int>("PaymentMethodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PaymentMethodTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentMethodId");

                    b.ToTable("PaymentMethods");
                });

            modelBuilder.Entity("HorseService.Models.Service", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ServiceId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("HorseService.Models.Video", b =>
                {
                    b.Property<int>("VideoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Caption")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("VideoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VideoId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("HorseService.Models.AppointmentAdditionalTypes", b =>
                {
                    b.HasOne("HorseService.Models.AdditionalType", "AdditionalType")
                        .WithMany("AppointmentAdditionalTypes")
                        .HasForeignKey("AdditionalTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HorseService.Models.Appointments", "Appointments")
                        .WithMany()
                        .HasForeignKey("AppointmentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AdditionalType");

                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("HorseService.Models.AppointmentDetails", b =>
                {
                    b.HasOne("HorseService.Models.Appointments", "Appointments")
                        .WithMany("AppointmentDetails")
                        .HasForeignKey("AppointmentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HorseService.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId");

                    b.Navigation("Appointments");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("HorseService.Models.Appointments", b =>
                {
                    b.HasOne("HorseService.Models.AppointmentStatus", "AppointmentStatus")
                        .WithMany()
                        .HasForeignKey("AppointmentStatusId");

                    b.HasOne("HorseService.Models.Customer", "Customer")
                        .WithMany("Appointments")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HorseService.Models.Employee", "Employee")
                        .WithMany("Appointments")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("HorseService.Models.PaymentMethod", "PaymentMethod")
                        .WithMany()
                        .HasForeignKey("PaymentMethodId");

                    b.Navigation("AppointmentStatus");

                    b.Navigation("Customer");

                    b.Navigation("Employee");

                    b.Navigation("PaymentMethod");
                });

            modelBuilder.Entity("HorseService.Models.OffDays", b =>
                {
                    b.HasOne("HorseService.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HorseService.Models.BreakTypes", "BreakTypes")
                        .WithMany()
                        .HasForeignKey("breaktypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BreakTypes");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("HorseService.Models.Video", b =>
                {
                    b.HasOne("HorseService.Models.Employee", "Employee")
                        .WithMany("Videos")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("HorseService.Models.AdditionalType", b =>
                {
                    b.Navigation("AppointmentAdditionalTypes");
                });

            modelBuilder.Entity("HorseService.Models.Appointments", b =>
                {
                    b.Navigation("AppointmentDetails");
                });

            modelBuilder.Entity("HorseService.Models.Customer", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("HorseService.Models.Employee", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Videos");
                });
#pragma warning restore 612, 618
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HorseService.Models;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace HorseService.Data
{
    public partial class HorseServiceContext : DbContext
    {
        public HorseServiceContext()
        {
        }

        public HorseServiceContext(DbContextOptions<HorseServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Video> Videos { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<ContactUs> ContactUs { get; set; }
        public virtual DbSet<ContactForm> ContactForms { get; set; }
        public virtual DbSet<PageContent> PageContents { get; set; }
        public virtual DbSet<Appointments> Appointments { get; set; }
        public virtual DbSet<AppointmentStatus> AppointmentStatuses { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<BreakTypes> BreakTypes { get; set; }
        public virtual DbSet<OffDays> OffDays { get; set; }
        public virtual DbSet<HomeSlider> HomeSliders { get; set; }
        public virtual DbSet<Configuration> Configurations { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<AdditionalType> AdditionalTypes { get; set; }
        public virtual DbSet<AppointmentAdditionalTypes> AppointmentAdditionalTypes { get; set; }
        public virtual DbSet<AppointmentDetails> AppointmentDetails { get; set; }
        public virtual DbSet<AppoimentsDate> AppoimentsDates { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BreakTypes>().HasData(new BreakTypes { breaktypesId = 1, Title = "Hourly" });
            modelBuilder.Entity<BreakTypes>().HasData(new BreakTypes { breaktypesId = 2, Title = "Daily" });
            modelBuilder.Entity<BreakTypes>().HasData(new BreakTypes { breaktypesId = 3, Title = "Periodly" });
            modelBuilder.Entity<PageContent>().HasData(new PageContent { PageContentId=1, ContentAr="هذا النص هو مثال لنص يمكن ان يستبدل في نفس المساحة",PageTitleAr= "من نحن ", ContentEn = "This text is an example of text that can be replaced in the same space", PageTitleEn = "About Us" });
            modelBuilder.Entity<Configuration>().HasData(new Configuration { ConfigurationId=1,Cost=100 });
            modelBuilder.Entity<ContactUs>().HasData(new ContactUs { ContactUsId = 1, CompanyName = "Codeware",Address="Kwait",Email="codeware@gmail.com",Mobile="01091117381",Tele="2090555",Fax="d23",Facebook= "https://www.facebook.com/",Twitter= "https://twitter.com/",Instgram= "https://www.instagram.com/",LinkedIn= "https://www.linkedin.com/feed/",WhatsApp="01091117381" });
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
    }

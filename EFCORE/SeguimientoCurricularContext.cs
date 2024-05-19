 using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Proyect.Entities.POCOS;

namespace Proyect.EFCore;

public partial class SeguimientoCurricularContext : DbContext
{
    public SeguimientoCurricularContext()
    {
    }

    public SeguimientoCurricularContext(DbContextOptions<SeguimientoCurricularContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CalendarEvent> CalendarEvents { get; set; }

    public virtual DbSet<RegistroAsesor> RegistroAsesors { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("server=DESKTOP-IVLDN19;database=SeguimientoCurricular;Integrated Security=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CalendarEvent>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__CALENDAR__7944C8707318FBE2");

            entity.ToTable("CALENDAR_EVENTS");

            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.AsesorId).HasColumnName("AsesorID");
            entity.Property(e => e.EventDescription).HasMaxLength(255);
            entity.Property(e => e.StudentEmail).HasMaxLength(255);
        });

        modelBuilder.Entity<RegistroAsesor>(entity =>
        {
            entity.HasKey(e => e.AsesorId).HasName("PK__REGISTRO__B4F3918C328E09B7");

            entity.ToTable("REGISTRO_ASESOR");

            entity.Property(e => e.AsesorId).HasColumnName("AsesorID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Role).HasMaxLength(255);
            entity.Property(e => e.Speciality).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

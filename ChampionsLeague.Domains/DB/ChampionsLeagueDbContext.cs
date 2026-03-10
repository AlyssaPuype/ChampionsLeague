using System;
using System.Collections.Generic;
using ChampionsLeague.Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChampionsLeague.Domains.DB;

public partial class ChampionsLeagueDbContext : DbContext
{
    public ChampionsLeagueDbContext()
    {
    }

    public ChampionsLeagueDbContext(DbContextOptions<ChampionsLeagueDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Abonnement> Abonnements { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<Club> Clubs { get; set; }

    public virtual DbSet<Competitie> Competities { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderline> Orderlines { get; set; }

    public virtual DbSet<Stadion> Stadions { get; set; }

    public virtual DbSet<Stadionvak> Stadionvaks { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    public virtual DbSet<Zitplaat> Zitplaats { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQL26_VIVES;Database=ChampionsLeagueDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Abonnement>(entity =>
        {
            entity.ToTable("Abonnement");

            entity.HasIndex(e => e.ZitplaatsId, "UQ__Abonneme__4ECF5B70B7B226DD").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClubId).HasColumnName("club_id");
            entity.Property(e => e.Orderlineid).HasColumnName("orderlineid");
            entity.Property(e => e.Prijs)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("prijs");
            entity.Property(e => e.ZitplaatsId).HasColumnName("zitplaats_id");

            entity.HasOne(d => d.Club).WithMany(p => p.Abonnements)
                .HasForeignKey(d => d.ClubId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Abonnement_Club");

            entity.HasOne(d => d.Orderline).WithMany(p => p.Abonnements)
                .HasForeignKey(d => d.Orderlineid)
                .HasConstraintName("FK_Abonnement_Orderline");

            entity.HasOne(d => d.Zitplaats).WithOne(p => p.Abonnement)
                .HasForeignKey<Abonnement>(d => d.ZitplaatsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Abonnement_Zitplaats");
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.ToTable("AspNetUser");

            entity.HasIndex(e => e.Email, "UQ__AspNetUs__AB6E616426362C5C").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__AspNetUs__F3DBC57295AE9158").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("passwordHash");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Club>(entity =>
        {
            entity.ToTable("Club");

            entity.HasIndex(e => e.StadionId, "UQ__Club__3EC28F0C79C20726").IsUnique();

            entity.HasIndex(e => e.Naam, "UQ__Club__72E1CD788D8247FA").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Naam)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("naam");
            entity.Property(e => e.StadionId).HasColumnName("stadion_id");

            entity.HasOne(d => d.Stadion).WithOne(p => p.Club)
                .HasForeignKey<Club>(d => d.StadionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Club_Stadion");
        });

        modelBuilder.Entity<Competitie>(entity =>
        {
            entity.ToTable("Competitie");

            entity.HasIndex(e => e.Naam, "UQ__Competit__72E1CD78E8C69DBC").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Naam)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("naam");
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.ToTable("Match");

            entity.HasIndex(e => new { e.ThuisclubId, e.BezoekersclubId, e.MatchDate }, "UQ_Match").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BezoekersclubId).HasColumnName("bezoekersclub_id");
            entity.Property(e => e.CompetitieId).HasColumnName("competitie_id");
            entity.Property(e => e.MatchDate).HasColumnName("match_date");
            entity.Property(e => e.StadionId).HasColumnName("stadion_id");
            entity.Property(e => e.ThuisclubId).HasColumnName("thuisclub_id");

            entity.HasOne(d => d.Bezoekersclub).WithMany(p => p.MatchBezoekersclubs)
                .HasForeignKey(d => d.BezoekersclubId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Match_Bezoekersclub");

            entity.HasOne(d => d.Competitie).WithMany(p => p.Matches)
                .HasForeignKey(d => d.CompetitieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Match_Competitie");

            entity.HasOne(d => d.Stadion).WithMany(p => p.Matches)
                .HasForeignKey(d => d.StadionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Match_Stadion");

            entity.HasOne(d => d.Thuisclub).WithMany(p => p.MatchThuisclubs)
                .HasForeignKey(d => d.ThuisclubId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Match_Thuisclub");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.TotalePrijs)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totale_prijs");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_User");
        });

        modelBuilder.Entity<Orderline>(entity =>
        {
            entity.ToTable("Orderline");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Prijs)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("prijs");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderlines)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orderline_Order");
        });

        modelBuilder.Entity<Stadion>(entity =>
        {
            entity.ToTable("Stadion");

            entity.HasIndex(e => e.Naam, "UQ__Stadion__72E1CD78DAD7BB7D").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Capaciteit).HasColumnName("capaciteit");
            entity.Property(e => e.Naam)
                .HasMaxLength(22)
                .IsUnicode(false)
                .HasColumnName("naam");
        });

        modelBuilder.Entity<Stadionvak>(entity =>
        {
            entity.ToTable("Stadionvak");

            entity.HasIndex(e => new { e.StadionId, e.Ring, e.Type, e.Partij }, "UQ_Stadionvak_Combinatie").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Capaciteit).HasColumnName("capaciteit");
            entity.Property(e => e.Naam)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("naam");
            entity.Property(e => e.Partij)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("partij");
            entity.Property(e => e.Ring)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ring");
            entity.Property(e => e.StadionId).HasColumnName("stadion_id");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("type");

            entity.HasOne(d => d.Stadion).WithMany(p => p.Stadionvaks)
                .HasForeignKey(d => d.StadionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stadionvak_Stadion");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.ToTable("Ticket");

            entity.HasIndex(e => new { e.MatchId, e.ZitplaatsId }, "UQ_Ticket_SeatMatch").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MatchId).HasColumnName("match_id");
            entity.Property(e => e.Orderlineid).HasColumnName("orderlineid");
            entity.Property(e => e.Prijs)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("prijs");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.ZitplaatsId).HasColumnName("zitplaats_id");

            entity.HasOne(d => d.Match).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.MatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Match");

            entity.HasOne(d => d.Orderline).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.Orderlineid)
                .HasConstraintName("FK_Ticket_Orderline");

            entity.HasOne(d => d.Zitplaats).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.ZitplaatsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Zitplaats");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.ToTable("Voucher");

            entity.HasIndex(e => e.Code, "UQ__Voucher__357D4CF96C9047AB").IsUnique();

            entity.HasIndex(e => e.TicketId, "UQ__Voucher__D596F96AE34F7B2C").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.TicketId).HasColumnName("ticket_id");

            entity.HasOne(d => d.Ticket).WithOne(p => p.Voucher)
                .HasForeignKey<Voucher>(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Voucher_Ticket");
        });

        modelBuilder.Entity<Zitplaat>(entity =>
        {
            entity.HasIndex(e => new { e.StadionvakId, e.Nummer }, "UQ_Zitplaats_VakNummer").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nummer).HasColumnName("nummer");
            entity.Property(e => e.StadionvakId).HasColumnName("stadionvak_id");

            entity.HasOne(d => d.Stadionvak).WithMany(p => p.Zitplaats)
                .HasForeignKey(d => d.StadionvakId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Zitplaats_Stadionvak");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataModel;

public partial class UserAccountDbContext : DbContext
{
    public UserAccountDbContext()
    {
    }

    public UserAccountDbContext(DbContextOptions<UserAccountDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Entry> Entries { get; set; }

    public virtual DbSet<Transfer> Transfers { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=mUserAccountDB;Trusted_Connection=True;MultipleActiveResultSets=true");
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }
        IConfigurationBuilder buider = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false);

        IConfigurationRoot configuration = buider.Build();

        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__accounts__46A222CD28174167");

            entity.ToTable("accounts");

            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.AccountNumber).HasColumnName("account_number");
            entity.Property(e => e.Balance)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("balance");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(3)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.RoutingNumber).HasColumnName("routing_number");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__accounts__user_i__30F848ED");
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("PK__cards__BDF201DD35D80F04");

            entity.ToTable("cards");

            entity.Property(e => e.CardId).HasColumnName("card_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Cvv)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("cvv");
            entity.Property(e => e.Expiration)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("expiration");
            entity.Property(e => e.Number)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("number");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Account).WithMany(p => p.Cards)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cards__account_i__35BCFE0A");

            entity.HasOne(d => d.User).WithMany(p => p.Cards)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cards__user_id__34C8D9D1");
        });

        modelBuilder.Entity<Entry>(entity =>
        {
            entity.HasKey(e => e.EntryId).HasName("PK__entries__810FDCE13F7A44B9");

            entity.ToTable("entries");

            entity.Property(e => e.EntryId).HasColumnName("entry_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(3)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");

            entity.HasOne(d => d.Account).WithMany(p => p.Entries)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__entries__account__33D4B598");
        });

        modelBuilder.Entity<Transfer>(entity =>
        {
            entity.HasKey(e => e.TransferId).HasName("PK__transfer__78E6FD33B9BF9DEC");

            entity.ToTable("transfers");

            entity.Property(e => e.TransferId).HasColumnName("transfer_id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(3)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.FromAccountId).HasColumnName("from_account_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.ToAccountId).HasColumnName("to_account_id");

            entity.HasOne(d => d.FromAccount).WithMany(p => p.TransferFromAccounts)
                .HasForeignKey(d => d.FromAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__transfers__from___31EC6D26");

            entity.HasOne(d => d.ToAccount).WithMany(p => p.TransferToAccounts)
                .HasForeignKey(d => d.ToAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__transfers__to_ac__32E0915F");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370F40A936FB");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Number)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("number");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

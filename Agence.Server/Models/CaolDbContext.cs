using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;

namespace Agence.Server.Models;

public class CaolDbContext : DbContext
{
    public CaolDbContext(DbContextOptions<CaolDbContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<PermissaoSistema> PermissaoSistemas => Set<PermissaoSistema>();
    public DbSet<Salario> Salarios => Set<Salario>();
    public DbSet<Sistema> Sistemas => Set<Sistema>();
    public DbSet<Fatura> Faturas => Set<Fatura>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){ }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.CoUsuario).HasName("co_usuario");
            entity.ToTable("cao_usuario");
            entity.Property(e => e.CoUsuario).HasColumnName("co_usuario").HasMaxLength(50);
            entity.Property(e => e.NoUsuario).HasColumnName("no_usuario").HasMaxLength(50);
        });

        modelBuilder.Entity<PermissaoSistema>(entity =>
        {
            entity.HasKey(e => new { e.CoUsuario, e.CoTipoUsuario, e.CoSistema }).HasName("co_usuario");
            entity.ToTable("permissao_sistema");
            entity.Property(e => e.CoUsuario).HasColumnName("co_usuario").HasMaxLength(50);
            entity.Property(e => e.CoTipoUsuario).HasColumnName("co_tipo_usuario");
            entity.Property(e => e.CoSistema).HasColumnName("co_sistema");
            entity.Property(e => e.InAtivo).HasColumnName("in_ativo").HasColumnType("char(1)");
            entity.Property(e => e.CoUsuarioAtualizacao).HasColumnName("co_usuario_atualizacao").HasMaxLength(20);
            entity.Property(e => e.DtAtualizacao).HasColumnName("dt_atualizacao").HasColumnType("datetime");
        });

        modelBuilder.Entity<Salario>(entity =>
        {
            entity.HasKey(e => new { e.CoUsuario, e.DtAlteracao });
            entity.ToTable("cao_salario");
            entity.Property(e => e.CoUsuario).HasColumnName("co_usuario").HasMaxLength(50);
            entity.Property(e => e.DtAlteracao).HasColumnName("dt_alteracao").HasColumnType("datetime");
            entity.Property(e => e.BrutSalario).HasColumnName("brut_salario").HasColumnType("float");
            entity.Property(e => e.LiqSalario).HasColumnName("liq_salario").HasColumnType("float");
        });

        modelBuilder.Entity<Sistema>(entity =>
        {
            entity.HasKey(e => e.CoSistema).HasName("co_sistema");
            entity.ToTable("cao_sistema");
            entity.Property(e => e.CoSistema).HasColumnName("co_sistema");
            entity.Property(e => e.CoCliente).HasColumnName("co_cliente");
            entity.Property(e => e.CoUsuario).HasColumnName("co_usuario").HasMaxLength(50);
            entity.Property(e => e.CoArquitetura).HasColumnName("co_arquitetura");
            entity.Property(e => e.NoSistema).HasColumnName("no_sistema").HasMaxLength(200);
        });

        modelBuilder.Entity<Fatura>(entity =>
        {
            entity.HasKey(e => e.CoFatura).HasName("co_fatura");
            entity.ToTable("cao_fatura");
            entity.Property(e => e.CoFatura).HasColumnName("co_fatura");
            entity.Property(e => e.CoSistema).HasColumnName("co_sistema");
            entity.Property(e => e.CoCliente).HasColumnName("co_cliente");
            entity.Property(e => e.CoOs).HasColumnName("co_os");
            entity.Property(e => e.NumNf).HasColumnName("num_nf");
            entity.Property(e => e.Total).HasColumnName("total").HasColumnType("float");
            entity.Property(e => e.Valor).HasColumnName("valor").HasColumnType("float");
            entity.Property(e => e.DataEmissao).HasColumnName("data_emissao").HasColumnType("datetime");
            entity.Property(e => e.CorpoNf).HasColumnName("corpo_nf").HasColumnType("text");
            entity.Property(e => e.ComissaoCn).HasColumnName("comissao_cn").HasColumnType("float");
            entity.Property(e => e.TotalImpInc).HasColumnName("total_imp_inc").HasColumnType("float");
        });

        base.OnModelCreating(modelBuilder);
    }
}



using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Exame.Models
{
    public partial class BNP_Paribas_DBContext : DbContext
    {
        public BNP_Paribas_DBContext()
        {
        }

        public BNP_Paribas_DBContext(DbContextOptions<BNP_Paribas_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MovimentoManual> MovimentosManuais { get; set; }
        public virtual DbSet<Produto> Produtos { get; set; }
        public virtual DbSet<ProdutoCosif> ProdutoCosifs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=bnp_paribas_teste;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<MovimentoManual>(entity =>
            {
                entity.HasKey(e => new { e.DatMes, e.DatAno, e.NumLancamento, e.CodProduto, e.CodCosif });

                entity.ToTable("MOVIMENTO_MANUAL");

                entity.Property(e => e.DatMes)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("DAT_MES");

                entity.Property(e => e.DatAno)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("DAT_ANO");

                entity.Property(e => e.NumLancamento)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("NUM_LANCAMENTO");

                entity.Property(e => e.CodProduto)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("COD_PRODUTO")
                    .IsFixedLength(true);

                entity.Property(e => e.CodCosif)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("COD_COSIF")
                    .IsFixedLength(true);

                entity.Property(e => e.CodUsuario)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("COD_USUARIO");

                entity.Property(e => e.DatMovimento)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("DAT_MOVIMENTO");

                entity.Property(e => e.DesDescricao)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DES_DESCRICAO");

                entity.Property(e => e.ValValor)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("VAL_VALOR");

                entity.HasOne(d => d.Cod)
                    .WithMany(p => p.MovimentosManuais)
                    .HasForeignKey(d => new { d.CodProduto, d.CodCosif })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MOVIMENTO_MANUAL__534D60F1");
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => e.CodProduto);

                entity.ToTable("PRODUTO");

                entity.Property(e => e.CodProduto)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("COD_PRODUTO")
                    .IsFixedLength(true);

                entity.Property(e => e.DesProduto)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("DES_PRODUTO");

                entity.Property(e => e.StaStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STA_STATUS")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<ProdutoCosif>(entity =>
            {
                entity.HasKey(e => new { e.CodProduto, e.CodCosif });

                entity.ToTable("PRODUTO_COSIF");

                entity.Property(e => e.CodProduto)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("COD_PRODUTO")
                    .IsFixedLength(true);

                entity.Property(e => e.CodCosif)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("COD_COSIF")
                    .IsFixedLength(true);

                entity.Property(e => e.CodClassificacao)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("COD_CLASSIFICACAO")
                    .IsFixedLength(true);

                entity.Property(e => e.StaStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STA_STATUS")
                    .IsFixedLength(true);

                entity.HasOne(d => d.CodProdutoNavigation)
                    .WithMany(p => p.ProdutoCosifs)
                    .HasForeignKey(d => d.CodProduto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PRODUTO_C__COD_P__3B75D760");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

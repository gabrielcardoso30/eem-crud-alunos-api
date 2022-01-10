using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities.Security;
using System;

namespace Infra.Data.ModelConfiguration.Security
{
    public class PermissaoGrupoModelConfiguration : IEntityTypeConfiguration<PermissaoGrupo>
    {
        public void Configure(EntityTypeBuilder<PermissaoGrupo> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable(nameof(PermissaoGrupo));
            
            entityTypeBuilder.Property(e => e.Id).ValueGeneratedOnAdd();

            entityTypeBuilder.HasOne(d => d.Grupo)
                .WithMany(p => p.PermissaoGrupo)
                .HasForeignKey(d => d.GrupoId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entityTypeBuilder.HasOne(d => d.Permissao)
                .WithMany(p => p.PermissaoGrupo)
                .HasForeignKey(d => d.PermissaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PermissaoGrupos_Permissao_PermissaoId");

            entityTypeBuilder.HasData(
                new PermissaoGrupo()
                {
                    Id = new Guid("FFFA2477-10A8-4955-9C4A-4B0251210B9D"),
                    PermissaoId = new Guid("460868E7-6B2B-4D8D-A56C-C096E236DDCD"),
                    GrupoId = new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"),
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local)
                },
                new PermissaoGrupo()
                {
                    Id = new Guid("25AE9B26-D36F-4EB6-870A-4AD5272F3821"),
                    PermissaoId = new Guid("EC7D9981-5E78-4B3E-95F4-27958C0D4F66"),
                    GrupoId = new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"),
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local)
                },
                new PermissaoGrupo()
                {
                    Id = new Guid("EBB1CEED-CDBF-4081-80AD-BDBD30F5875F"),
                    PermissaoId = new Guid("9382B87B-1401-4B51-BDFA-5BBACA41CB78"),
                    GrupoId = new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"),
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local)
                },
                new PermissaoGrupo()
                {
                    Id = new Guid("B4A4642F-C59D-4BA4-8B85-339EF9A1D784"),
                    PermissaoId = new Guid("FD1324D8-5C31-4FDB-B629-B9755B3DC9EF"),
                    GrupoId = new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"),
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local)
                },
                new PermissaoGrupo()
                {
                    Id = new Guid("1A084F91-86D1-4DFC-9A8E-04043DB99E83"),
                    PermissaoId = new Guid("D4A39D1C-C7FD-41DE-AA56-CF269F723002"),
                    GrupoId = new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"),
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local)
                },
                new PermissaoGrupo()
                {
                    Id = new Guid("45A1A818-4D5D-4F80-8E7C-9957746BDA46"),
                    PermissaoId = new Guid("7298E2AE-C46A-47FF-9AE5-D9620E0B288C"),
                    GrupoId = new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"),
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local)
                },
                new PermissaoGrupo()
                {
                    Id = new Guid("407D89AA-6EE5-43A5-B868-66319B28D48A"),
                    PermissaoId = new Guid("68943F93-71AF-4368-A57A-9F0B2D77F56E"),
                    GrupoId = new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"),
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local)
                },
                new PermissaoGrupo()
                {
                    Id = new Guid("96042D19-E425-4096-86D1-6A816B0A389D"),
                    PermissaoId = new Guid("59666E7E-320F-44E2-9D14-97A131A78618"),
                    GrupoId = new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"),
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local)
                },
                new PermissaoGrupo()
                {
                    Id = new Guid("81289871-B778-4E86-A009-2BCA85468124"),
                    PermissaoId = new Guid("1E7D4555-BE2F-421F-9FEE-7C22F4326B60"),
                    GrupoId = new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"),
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local)
                },
                new PermissaoGrupo()
                {
                    Id = new Guid("D042D1C9-36DD-4336-9A51-08096DF747D5"),
                    PermissaoId = new Guid("F9114F1D-D1A0-4AFA-B1BC-B8137A5B7640"),
                    GrupoId = new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"),
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local)
                },
                new PermissaoGrupo()
                {
                    Id = new Guid("42C698EE-8EB5-4F9E-B13B-E7B4812C0320"),
                    PermissaoId = new Guid("ED2B7A70-90AE-44D4-B81E-506EF914C34C"),
                    GrupoId = new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"),
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local)
                },
                new PermissaoGrupo()
                {
                    Id = new Guid("9D2DCE8C-B79A-41D5-9DF9-A3A8C0C7B060"),
                    PermissaoId = new Guid("684FD917-CA64-47A2-B5EA-5CF801A36A93"),
                    GrupoId = new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"),
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local)
                },
                new PermissaoGrupo()
                {
                    Id = new Guid("25A377C4-7EC7-48F6-8973-A5FE5BFE5FE7"),
                    PermissaoId = new Guid("36B7322E-42A9-4828-9D84-D0DBFAC3E27D"),
                    GrupoId = new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"),
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local)
                },
                new PermissaoGrupo()
                {
                    Id = new Guid("1982B23D-6602-4EF9-B55D-6607AA83E5E5"),
                    PermissaoId = new Guid("4B31BFDB-790B-408E-B8D6-60397CB489C3"),
                    GrupoId = new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"),
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local)
                },
                new PermissaoGrupo()
                {
                    Id = new Guid("1FF30662-6CB9-4971-AD7D-883264B939A6"),
                    PermissaoId = new Guid("9BB40F14-1EA7-4C84-A3A8-48CFFB7E921F"),
                    GrupoId = new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"),
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local)
                },
                new PermissaoGrupo()
                {
                    Id = new Guid("5B4B6DED-2693-4A14-B08B-0ED3C3B8F164"),
                    PermissaoId = new Guid("1A31DFCF-234B-4745-9C3B-2F41BBD3021D"),
                    GrupoId = new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"),
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local)
                },
                new PermissaoGrupo()
                {
                    Id = new Guid("5BF94D8E-947A-43B3-8F7C-EECAED54FD54"),
                    PermissaoId = new Guid("1E8731B6-338D-4F3F-9EBB-045A2B18156A"),
                    GrupoId = new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"),
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local)
                },

                //UNIDADES DE ACESSO
                new PermissaoGrupo()
                {
                    Id = new Guid("62EB8A29-CBAA-4319-BEFF-4A18A57F5571"),
                    PermissaoId = new Guid("E1F523A6-0217-44A8-A649-A0D1B22505C3"),
                    GrupoId = new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"),
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local)
                },
                new PermissaoGrupo()
                {
                    Id = new Guid("170E5D4A-2637-48EE-ABEF-F37F3C25F602"),
                    PermissaoId = new Guid("B3EA5DCD-B6CF-48D5-90A3-6A0972CC72AD"),
                    GrupoId = new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"),
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local)
                },
                new PermissaoGrupo()
                {
                    Id = new Guid("50CC28BA-4822-4980-928F-4CFD1BCF0F6E"),
                    PermissaoId = new Guid("8490DC09-50CD-42D4-87C6-B70EC7E488D5"),
                    GrupoId = new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"),
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local)
                },
                new PermissaoGrupo()
                {
                    Id = new Guid("B6B45DE4-6CF7-41B1-AD4B-3E12D3BC6B1B"),
                    PermissaoId = new Guid("71F80EEF-D10E-41D2-A803-E5B0F563B0DA"),
                    GrupoId = new Guid("2da65b7f-5238-4fec-a9cc-1cf3316dec11"),
                    DataCriacao = new DateTime(2021, 1, 24, 18, 6, 7, 725, DateTimeKind.Local)
                }
            );
        }
    }
}

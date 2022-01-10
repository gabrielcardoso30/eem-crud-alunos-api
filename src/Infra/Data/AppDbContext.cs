using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Core.Entities.Security;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Infra.Data.ModelConfiguration.Security;
using Infra.Data.ModelConfiguration.Gerencial;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Core.Helpers;
using Core.Interfaces.Security;
using AspNetUsers = Core.Entities.Security.AspNetUsers;
using AspNetUsersRefreshToken = Core.Entities.Security.AspNetUsersRefreshToken;
using Auditoria = Core.Entities.Security.Auditoria;
using Grupo = Core.Entities.Security.Grupo;
using GrupoAspNetUsers = Core.Entities.Security.GrupoAspNetUsers;
using ParametroSistema = Core.Entities.Security.ParametroSistema;
using Permissao = Core.Entities.Security.Permissao;
using PermissaoGrupo = Core.Entities.Security.PermissaoGrupo;
using PermissaoUsuario = Core.Entities.Security.PermissaoUsuario;
using Core.Entities.Gerencial;

namespace Infra.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        private IAuthenticatedUser _authenticatedUser;

        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> options,
            IAuthenticatedUser authenticatedUser) : base(options)
        {
            _authenticatedUser = authenticatedUser;
        }
        
        //SECURITY
        public virtual DbSet<AspNetUsersRefreshToken> AspNetUsersRefreshToken { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Auditoria> Auditoria { get; set; }
        public virtual DbSet<Grupo> Grupo { get; set; }
        public virtual DbSet<Permissao> Permissao { get; set; }
        public virtual DbSet<PermissaoGrupo> PermissaoGrupo { get; set; }
        public virtual DbSet<PermissaoUsuario> PermissaoUsuario { get; set; }
        public virtual DbSet<GrupoAspNetUsers> GrupoAspNetUsers { get; set; }
        public virtual DbSet<ParametroSistema> ParametroSistema { get; set; }
        public virtual DbSet<UnidadeAcesso> UnidadeAcesso { get; set; }
        public virtual DbSet<UnidadeAcessoModulo> UnidadeAcessoModulo { get; set; }
        public virtual DbSet<GrupoModulo> GrupoModulo { get; set; }
        public virtual DbSet<GrupoUnidadeAcesso> GrupoUnidadeAcesso { get; set; }

        //GERENCIAL
        public virtual DbSet<Aluno> Aluno { get; set; }
        public virtual DbSet<Responsavel> Responsavel { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            /******************************* Pasta Security ****************************************/
            modelBuilder.ApplyConfiguration(new AspNetUsersModelConfiguration());
            modelBuilder.ApplyConfiguration(new AspNetUsersRefreshTokenModelConfiguration());
            modelBuilder.ApplyConfiguration(new AuditModelConfiguration());
            modelBuilder.ApplyConfiguration(new GrupoAspNetUsersModelConfiguration());
            modelBuilder.ApplyConfiguration(new GrupoModelConfiguration());
            modelBuilder.ApplyConfiguration(new ParametroSistemaModelConfiguration());
            modelBuilder.ApplyConfiguration(new PermissaoGrupoModelConfiguration());
            modelBuilder.ApplyConfiguration(new PermissaoModelConfiguration());
            modelBuilder.ApplyConfiguration(new PermissaoUsuarioModelConfiguration());
            modelBuilder.ApplyConfiguration(new UnidadeAcessoModelConfiguration());
            modelBuilder.ApplyConfiguration(new UnidadeAcessoModuloModelConfiguration());
            modelBuilder.ApplyConfiguration(new GrupoModuloModelConfiguration());
            modelBuilder.ApplyConfiguration(new GrupoUnidadeAcessoModelConfiguration());

            /******************************* Pasta Gerencial ****************************************/
            modelBuilder.ApplyConfiguration(new AlunoModelConfiguration());
            modelBuilder.ApplyConfiguration(new ResponsavelModelConfiguration());


            PutDeletedFilter(modelBuilder);

        }

        public void PutDeletedFilter(ModelBuilder modelBuilder)
        {

            /******************************* Pasta Security ****************************************/
            modelBuilder.Entity<ApplicationUser>().HasQueryFilter(gc => !gc.Deletado);
            modelBuilder.Entity<Grupo>().HasQueryFilter(gc => !gc.Deletado);
            modelBuilder.Entity<UnidadeAcesso>().HasQueryFilter(gc => !gc.Deletado);

            /******************************* Pasta Gerencial ****************************************/
            modelBuilder.Entity<Aluno>().HasQueryFilter(gc => !gc.Deletado);
            modelBuilder.Entity<Responsavel>().HasQueryFilter(gc => !gc.Deletado);

        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            var auditEntries = OnBeforeSaveChanges();
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            await OnAfterSaveChanges(auditEntries);
            return result;
        }

        private List<AuditoriaEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            
            var auditEntries = new List<AuditoriaEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Auditoria ||
                    entry.Entity is AspNetUsersRefreshToken ||
                    entry.State == EntityState.Detached ||
                    entry.State == EntityState.Unchanged)
                    continue;

                Guid? aspNetUsersId = _authenticatedUser.GuidLogin();

                if (aspNetUsersId == Guid.Empty)
                    aspNetUsersId = null;
                
                var auditEntry = new AuditoriaEntry(entry)
                {
                    ParentKeyValue = GetParentKeyValue(entry),
                    Entidade = entry.Metadata.GetTableName(),
                    AspNetUsersId = aspNetUsersId,
                    EntityState = entry.State.ObterDescricaoEnum()
                };
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    var propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues = property.CurrentValue.ToString();
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:                            
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                                entry.CurrentValues["DataUltimaAtualizacao"] = DateTime.Now;
                                entry.CurrentValues["UsuarioIdUltimaAtualizacao"] = aspNetUsersId;
                            }

                            break;
                        case EntityState.Detached:
                            break;
                        case EntityState.Unchanged:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                Auditoria.Add(auditEntry.ToAuditoria());
            }
            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        private Task OnAfterSaveChanges(IReadOnlyCollection<AuditoriaEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            foreach (var auditEntry in auditEntries)
            {
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues = prop.CurrentValue.ToString();
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }
                Auditoria.Add(auditEntry.ToAuditoria());
            }

            return SaveChangesAsync();
        }
        
        private static string GetParentKeyValue(EntityEntry entry)
        {
            IEnumerable<object> key;
            switch (entry.Entity.ToString())
            {
                case string a when a.Contains("Empresa"):
                    key = entry.Properties
                        .Where(w => w.Metadata.Name.Contains("EmpresaId"))
                        .Select(s => s.CurrentValue);
                    break;
                default:
                    return null;
            }
            return key.Any() ? key.First().ToString() : null;
        }
    }
}
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using RenRen.Domain.Auth.Entity.Model;

namespace RenRen.Domain.Auth.Entity
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext()
        {
        }

        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        public DbSet<AuthSysMenu> SysMenus { get; set; }
        public DbSet<AuthSysRole> SysRoles { get; set; }
        public DbSet<AuthSysUserRole> SysUserRoles { get; set; }
        public DbSet<AuthSysUserToken> SysUserTokens { get; set; }
        public DbSet<AuthSysRoleMenu> SysRoleMenus { get; set; }
        public DbSet<AuthSysUser> Users { get; set; }
    }
}

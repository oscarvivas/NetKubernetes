using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetKubernetes.Models;

namespace NetKubernetes.Data;

public class AppDbContext : IdentityDbContext<UserApp> {

    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<UserApp>().Property(x => x.Id).HasMaxLength(36);
        builder.Entity<UserApp>().Property(x => x.NormalizedUserName).HasMaxLength(90);
        builder.Entity<IdentityRole>().Property(x => x.Id).HasMaxLength(36);
        builder.Entity<IdentityRole>().Property(x => x.Name).HasMaxLength(90);
    }

    public DbSet<Inmueble>? Inmuebles { set; get; }

    public DbSet<Interviewer>? Interviewers { set; get; }

    public DbSet<Candidate>? Candidates { set; get; }

    public DbSet<Questionary>? Questionaries { set; get; }

    public DbSet<Topic>? Topics { set; get; }

    public DbSet<Interview>? Interviews { set; get; }

    public DbSet<InterviewTopic>? InterviewTopics { set; get; }
}
﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SignalR.Models;

public partial class SignalRContext : DbContext
{
    public SignalRContext()
    {
    }

    public SignalRContext(DbContextOptions<SignalRContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chat> Chats { get; set; }
    public virtual DbSet<Group> Groups { get; set; }
    public virtual DbSet<GroupMessage> GroupMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<Chat>().HasKey(e => e.Id);

        modelBuilder.Entity<GroupMessage>()
            .HasOne(m => m.group)
            .WithMany();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
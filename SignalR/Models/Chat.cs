﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SignalR.Models;

[Keyless]
[Table("Chat")]
public partial class Chat
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string UserName { get; set; }

    public string Messege { get; set; }
}
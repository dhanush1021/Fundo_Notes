﻿using Microsoft.EntityFrameworkCore;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Context
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<UserEntity> UserTable { get; set; }
        public DbSet<NoteEntity> NotesTable { get; set; }
        public DbSet<LabelEntity> LabelsTable { get; set; }
    }
}
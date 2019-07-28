using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectSparky.Model
{
    public class ProjectSparyContext: DbContext
    {
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=ProjectSparky.db");
        }
    }

    public class Alarm
    {
        [Key]
        public string AlarmGUID { get; set; }
        public DateTime AlarmTime { get; set; }
        public string AlarmTone { get; set; }
        public bool Active { get; set; }
    }

    public class UserAccount
    {
        [Key]
        public string UserAccountGUID { get; set; }
        public string AccountName { get; set; }
        public string AccountID { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenExpiresOn { get; set; }
    }

    public class Note
    {
        [Key]
        public string NoteGUID { get; set; }
        public string NoteText { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}

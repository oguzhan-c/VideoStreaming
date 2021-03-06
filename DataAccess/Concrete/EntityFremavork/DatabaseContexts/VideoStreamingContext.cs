using Entities.Concrete;
 using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrute;

namespace DataAccess.Concrete.EntityFremavork.DatabaseContexts
{
    //Context : Db deki Tablolar İle Class ları Birbirine Bağlamak 
    public class VideoStreamingContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer
            (
                @"Server=127.0.0.1;
                Database=VideoStreaming;
                integrated security=false;
                User Id=SA;
                PASSWORD=MyPass@word"

            );
        }
        //Entities in the database are mapped to Entities in the entity class

        public DbSet<User> Users { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Communication> Communications { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Dislike> Dislikes { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Trend> Trends { get; set; }
    }
}

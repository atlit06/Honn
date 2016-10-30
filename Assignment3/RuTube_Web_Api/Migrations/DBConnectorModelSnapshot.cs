using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using RuTube_Web_Api.SQLite;

namespace RuTube_Web_Api.Migrations
{
    [DbContext(typeof(DBConnector))]
    partial class DBConnectorModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("RuTube_Web_Api.SQLite.User", b =>
                {
                    b.Property<string>("userName");

                    b.Property<string>("email");

                    b.Property<string>("password");

                    b.Property<string>("realName");

                    b.HasKey("userName");

                    b.ToTable("Users");
                });
        }
    }
}

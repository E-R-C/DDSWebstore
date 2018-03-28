﻿// <auto-generated />
using DDSWebstore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace DDSWebstore.Migrations
{
    [DbContext(typeof(MyDBContext))]
    partial class MyDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("DDSWebstore.Models.Item", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("available");

                    b.Property<string>("description");

                    b.Property<string>("location");

                    b.Property<string>("name");

                    b.Property<int>("orderID");

                    b.Property<float>("price");

                    b.Property<string>("status");

                    b.HasKey("ID");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("DDSWebstore.Models.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("city");

                    b.Property<string>("state");

                    b.Property<string>("streetAddress");

                    b.Property<int>("zipcode");

                    b.HasKey("ID");

                    b.ToTable("Order");
                });
#pragma warning restore 612, 618
        }
    }
}

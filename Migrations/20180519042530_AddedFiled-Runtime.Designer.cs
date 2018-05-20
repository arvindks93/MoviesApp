﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using MovieApp.Entities;
using System;

namespace MovieApp.Migrations
{
    [DbContext(typeof(MoviesContext))]
    [Migration("20180519042530_AddedFiled-Runtime")]
    partial class AddedFiledRuntime
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("MovieApp.Entities.Actor", b =>
                {
                    b.Property<int>("ActorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.HasKey("ActorId");

                    b.ToTable("actor");
                });

            modelBuilder.Entity("MovieApp.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)");

                    b.Property<string>("Name")
                        .HasMaxLength(45);

                    b.HasKey("CategoryId");

                    b.ToTable("category");
                });

            modelBuilder.Entity("MovieApp.Entities.Film", b =>
                {
                    b.Property<int>("FilmId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Rating")
                        .HasMaxLength(45);

                    b.Property<int?>("ReleaseYear")
                        .HasColumnType("int(11)");

                    b.Property<int?>("Runtime");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("FilmId");

                    b.ToTable("film");
                });

            modelBuilder.Entity("MovieApp.Entities.FilmActor", b =>
                {
                    b.Property<int>("FilmId")
                        .HasColumnType("int(11)");

                    b.Property<int>("ActorId")
                        .HasColumnType("int(11)");

                    b.HasKey("FilmId", "ActorId");

                    b.HasIndex("ActorId")
                        .HasName("film_actor_actor_idx");

                    b.ToTable("film_actor");
                });

            modelBuilder.Entity("MovieApp.Entities.FilmCategory", b =>
                {
                    b.Property<int>("FilmId")
                        .HasColumnType("int(11)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int(11)");

                    b.HasKey("FilmId", "CategoryId");

                    b.HasIndex("CategoryId")
                        .HasName("film_category_category_fk_idx");

                    b.ToTable("film_category");
                });

            modelBuilder.Entity("MovieApp.Entities.FilmActor", b =>
                {
                    b.HasOne("MovieApp.Entities.Actor", "Actor")
                        .WithMany("FilmActor")
                        .HasForeignKey("ActorId")
                        .HasConstraintName("film_actor_actor");

                    b.HasOne("MovieApp.Entities.Film", "Film")
                        .WithMany("FilmActor")
                        .HasForeignKey("FilmId")
                        .HasConstraintName("film_actor_film");
                });

            modelBuilder.Entity("MovieApp.Entities.FilmCategory", b =>
                {
                    b.HasOne("MovieApp.Entities.Category", "Category")
                        .WithMany("FilmCategory")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("film_category_category_fk");

                    b.HasOne("MovieApp.Entities.Film", "Film")
                        .WithMany("FilmCategory")
                        .HasForeignKey("FilmId")
                        .HasConstraintName("film_category_film_fk");
                });
#pragma warning restore 612, 618
        }
    }
}

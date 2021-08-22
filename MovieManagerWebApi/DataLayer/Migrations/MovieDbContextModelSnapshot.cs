﻿// <auto-generated />
using System;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataLayer.Migrations
{
    [DbContext(typeof(MovieDbContext))]
    partial class MovieDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ActorMovie", b =>
                {
                    b.Property<int>("ActorsId")
                        .HasColumnType("integer");

                    b.Property<int>("StarredInMoviesId")
                        .HasColumnType("integer");

                    b.HasKey("ActorsId", "StarredInMoviesId");

                    b.HasIndex("StarredInMoviesId");

                    b.ToTable("ActorMovie");
                });

            modelBuilder.Entity("ActorTVShow", b =>
                {
                    b.Property<int>("ActorsId")
                        .HasColumnType("integer");

                    b.Property<int>("StarredInTvShowsId")
                        .HasColumnType("integer");

                    b.HasKey("ActorsId", "StarredInTvShowsId");

                    b.HasIndex("StarredInTvShowsId");

                    b.ToTable("ActorTVShow");
                });

            modelBuilder.Entity("Domain.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedWhen")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ModifiedWhen")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("Domain.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CoverPath")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedWhen")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateTime>("ModifiedWhen")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Domain.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedWhen")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ModifiedWhen")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("MovieId")
                        .HasColumnType("integer");

                    b.Property<int>("SeriesId")
                        .HasColumnType("integer");

                    b.Property<int?>("TVShowId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<byte>("Value")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("TVShowId");

                    b.HasIndex("UserId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Domain.TVShow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CoverPath")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedWhen")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ModifiedWhen")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("NumberOfSeasons")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TVShows");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("About")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateTime>("CreatedWhen")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedWhen")
                        .HasColumnType("timestamp without time zone");

                    b.Property<byte[]>("PassHash")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("bytea");

                    b.Property<bool>("Private")
                        .HasColumnType("boolean");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("bytea");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ActorMovie", b =>
                {
                    b.HasOne("Domain.Actor", null)
                        .WithMany()
                        .HasForeignKey("ActorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Movie", null)
                        .WithMany()
                        .HasForeignKey("StarredInMoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ActorTVShow", b =>
                {
                    b.HasOne("Domain.Actor", null)
                        .WithMany()
                        .HasForeignKey("ActorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.TVShow", null)
                        .WithMany()
                        .HasForeignKey("StarredInTvShowsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Rating", b =>
                {
                    b.HasOne("Domain.Movie", null)
                        .WithMany("Ratings")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.TVShow", null)
                        .WithMany("Ratings")
                        .HasForeignKey("TVShowId");

                    b.HasOne("Domain.User", "User")
                        .WithMany("Ratings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Movie", b =>
                {
                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("Domain.TVShow", b =>
                {
                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}

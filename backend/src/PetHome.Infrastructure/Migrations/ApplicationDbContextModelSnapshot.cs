﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetHome.Infrastructure;

#nullable disable

namespace PetHome.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PetHome.Domain.Models.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birthday");

                    b.Property<string>("Breed")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("breed");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("color");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("create_time");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("description");

                    b.Property<string>("Health")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("health");

                    b.Property<double>("Height")
                        .HasColumnType("double precision")
                        .HasColumnName("height");

                    b.Property<int>("HelpStatus")
                        .HasColumnType("integer")
                        .HasColumnName("help_status");

                    b.Property<bool>("IsNeutered")
                        .HasColumnType("boolean")
                        .HasColumnName("is_neutered");

                    b.Property<bool>("IsVaccinated")
                        .HasColumnType("boolean")
                        .HasColumnName("is_vaccinated");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("nickname");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("phone");

                    b.Property<string>("Species")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("species");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision")
                        .HasColumnName("weight");

                    b.Property<Guid?>("voluteer_id")
                        .HasColumnType("uuid")
                        .HasColumnName("voluteer_id");

                    b.ComplexProperty<Dictionary<string, object>>("Adress", "PetHome.Domain.Models.Pet.Adress#Adress", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("AppartmentNumber")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("appartment");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("city");

                            b1.Property<string>("HouseNumber")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("house");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("street");
                        });

                    b.HasKey("Id")
                        .HasName("pk_pets");

                    b.HasIndex("voluteer_id")
                        .HasDatabaseName("ix_pets_voluteer_id");

                    b.ToTable("pets", (string)null);
                });

            modelBuilder.Entity("PetHome.Domain.Models.PetPhoto", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsMain")
                        .HasColumnType("boolean")
                        .HasColumnName("is_main");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("path");

                    b.Property<string>("Title")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("title");

                    b.Property<Guid?>("pet_id")
                        .HasColumnType("uuid")
                        .HasColumnName("pet_id");

                    b.HasKey("Id")
                        .HasName("pk_pet_photo");

                    b.HasIndex("pet_id")
                        .HasDatabaseName("ix_pet_photo_pet_id");

                    b.ToTable("pet_photo", (string)null);
                });

            modelBuilder.Entity("PetHome.Domain.Models.Requisite", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<Guid?>("pet_id")
                        .HasColumnType("uuid")
                        .HasColumnName("pet_id");

                    b.Property<Guid?>("voluteer_id")
                        .HasColumnType("uuid")
                        .HasColumnName("voluteer_id");

                    b.HasKey("Id")
                        .HasName("pk_requisite");

                    b.HasIndex("pet_id")
                        .HasDatabaseName("ix_requisite_pet_id");

                    b.HasIndex("voluteer_id")
                        .HasDatabaseName("ix_requisite_voluteer_id");

                    b.ToTable("requisite", (string)null);
                });

            modelBuilder.Entity("PetHome.Domain.Models.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("description");

                    b.Property<int>("Experience")
                        .HasColumnType("integer")
                        .HasColumnName("experience");

                    b.Property<int>("FoundHomePets")
                        .HasColumnType("integer")
                        .HasColumnName("found_home_pets");

                    b.Property<int>("NeedHomePets")
                        .HasColumnType("integer")
                        .HasColumnName("need_home_pets");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("phone");

                    b.Property<int>("TreatPets")
                        .HasColumnType("integer")
                        .HasColumnName("treat_pets");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "PetHome.Domain.Models.Volunteer.Name#FullName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("first_name");

                            b1.Property<string>("SeconNname")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("second_name");

                            b1.Property<string>("Surname")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("surname");
                        });

                    b.HasKey("Id")
                        .HasName("pk_volunteer");

                    b.ToTable("volunteer", (string)null);
                });

            modelBuilder.Entity("PetHome.Domain.Models.Pet", b =>
                {
                    b.HasOne("PetHome.Domain.Models.Volunteer", null)
                        .WithMany("Pets")
                        .HasForeignKey("voluteer_id")
                        .HasConstraintName("fk_pets_volunteer_voluteer_id");
                });

            modelBuilder.Entity("PetHome.Domain.Models.PetPhoto", b =>
                {
                    b.HasOne("PetHome.Domain.Models.Pet", null)
                        .WithMany("Photos")
                        .HasForeignKey("pet_id")
                        .HasConstraintName("fk_pet_photo_pets_pet_id");
                });

            modelBuilder.Entity("PetHome.Domain.Models.Requisite", b =>
                {
                    b.HasOne("PetHome.Domain.Models.Pet", null)
                        .WithMany("Detailes")
                        .HasForeignKey("pet_id")
                        .HasConstraintName("fk_requisite_pets_pet_id");

                    b.HasOne("PetHome.Domain.Models.Volunteer", null)
                        .WithMany("Detailes")
                        .HasForeignKey("voluteer_id")
                        .HasConstraintName("fk_requisite_volunteer_voluteer_id");
                });

            modelBuilder.Entity("PetHome.Domain.Models.Volunteer", b =>
                {
                    b.OwnsOne("PetHome.Domain.Models.SocialNetworkCollection", "SocialNetworks", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteer");

                            b1.ToJson("SocialNetworks");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteer_volunteer_id");

                            b1.OwnsMany("PetHome.Domain.Models.SocialNetwork", "SocialNetworks", b2 =>
                                {
                                    b2.Property<Guid>("SocialNetworkCollectionVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Link")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.HasKey("SocialNetworkCollectionVolunteerId", "Id")
                                        .HasName("pk_volunteer");

                                    b2.ToTable("volunteer");

                                    b2.WithOwner()
                                        .HasForeignKey("SocialNetworkCollectionVolunteerId")
                                        .HasConstraintName("fk_volunteer_volunteer_social_network_collection_volunteer_id");
                                });

                            b1.Navigation("SocialNetworks");
                        });

                    b.Navigation("SocialNetworks")
                        .IsRequired();
                });

            modelBuilder.Entity("PetHome.Domain.Models.Pet", b =>
                {
                    b.Navigation("Detailes");

                    b.Navigation("Photos");
                });

            modelBuilder.Entity("PetHome.Domain.Models.Volunteer", b =>
                {
                    b.Navigation("Detailes");

                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}

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

            modelBuilder.Entity("PetHome.Domain.Models.CommonModels.Requisite", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("DescriptionValue")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("description");

                    b.Property<Guid?>("pet_id")
                        .HasColumnType("uuid")
                        .HasColumnName("pet_id");

                    b.Property<Guid?>("voluteer_id")
                        .HasColumnType("uuid")
                        .HasColumnName("voluteer_id");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "PetHome.Domain.Models.CommonModels.Requisite.Name#NotNullableString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("name");
                        });

                    b.HasKey("Id")
                        .HasName("pk_requisite");

                    b.HasIndex("pet_id")
                        .HasDatabaseName("ix_requisite_pet_id");

                    b.HasIndex("voluteer_id")
                        .HasDatabaseName("ix_requisite_voluteer_id");

                    b.ToTable("requisite", (string)null);
                });

            modelBuilder.Entity("PetHome.Domain.Models.Pets.Breed", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("species_id")
                        .HasColumnType("uuid")
                        .HasColumnName("species_id");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "PetHome.Domain.Models.Pets.Breed.Name#NotNullableString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .ValueGeneratedOnAdd()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasDefaultValue("Unknown")
                                .HasColumnName("breed");
                        });

                    b.HasKey("Id")
                        .HasName("pk_breeds");

                    b.HasIndex("species_id")
                        .HasDatabaseName("ix_breeds_species_id");

                    b.ToTable("breeds", (string)null);
                });

            modelBuilder.Entity("PetHome.Domain.Models.Pets.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("create_time");

                    b.Property<string>("DescriptionValue")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("description");

                    b.Property<string>("Health")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("health");

                    b.Property<double>("Height")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("double precision")
                        .HasDefaultValue(0.0)
                        .HasColumnName("height");

                    b.Property<int>("HelpStatus")
                        .HasColumnType("integer")
                        .HasColumnName("help_status");

                    b.Property<bool>("IsNeutered")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_neutered");

                    b.Property<bool>("IsVaccinated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_vaccinated");

                    b.Property<double>("Weight")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("double precision")
                        .HasDefaultValue(0.0)
                        .HasColumnName("weight");

                    b.Property<Guid?>("voluteer_id")
                        .HasColumnType("uuid")
                        .HasColumnName("voluteer_id");

                    b.ComplexProperty<Dictionary<string, object>>("Address", "PetHome.Domain.Models.Pets.Pet.Address#Address", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("AppartmentNumber")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("appartment");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("city");

                            b1.Property<string>("HouseNumber")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("house");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("street");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("BirthDay", "PetHome.Domain.Models.Pets.Pet.BirthDay#DateTimeValue", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<DateTime>("Date")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("birthday");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Color", "PetHome.Domain.Models.Pets.Pet.Color#NotNullableString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("color");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Nickname", "PetHome.Domain.Models.Pets.Pet.Nickname#NotNullableString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("nick_name");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Phone", "PetHome.Domain.Models.Pets.Pet.Phone#Phone", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("PhoneNumber")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("phone");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("SpeciesBreedValue", "PetHome.Domain.Models.Pets.Pet.SpeciesBreedValue#SpeciesBreedValue", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<Guid?>("BreedId")
                                .HasColumnType("uuid")
                                .HasColumnName("breed_id");

                            b1.Property<Guid>("SpeciesId")
                                .HasColumnType("uuid")
                                .HasColumnName("species_id");
                        });

                    b.HasKey("Id")
                        .HasName("pk_pets");

                    b.HasIndex("voluteer_id")
                        .HasDatabaseName("ix_pets_voluteer_id");

                    b.ToTable("pets", (string)null);
                });

            modelBuilder.Entity("PetHome.Domain.Models.Pets.PetPhoto", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsMain")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
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

            modelBuilder.Entity("PetHome.Domain.Models.Pets.Species", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "PetHome.Domain.Models.Pets.Species.Name#NotNullableString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("name");
                        });

                    b.HasKey("Id")
                        .HasName("pk_species");

                    b.ToTable("species", (string)null);
                });

            modelBuilder.Entity("PetHome.Domain.Models.Volunteers.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("DescriptionValue")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("description");

                    b.Property<int>("Experience")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0)
                        .HasColumnName("experience");

                    b.ComplexProperty<Dictionary<string, object>>("Email", "PetHome.Domain.Models.Volunteers.Volunteer.Email#Email", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("email");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Name", "PetHome.Domain.Models.Volunteers.Volunteer.Name#FullName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("first_name");

                            b1.Property<string>("SecondNname")
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("second_name");

                            b1.Property<string>("Surname")
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("surname");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Phone", "PetHome.Domain.Models.Volunteers.Volunteer.Phone#Phone", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("PhoneNumber")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("phone");
                        });

                    b.HasKey("Id")
                        .HasName("pk_volunteer");

                    b.ToTable("volunteer", (string)null);
                });

            modelBuilder.Entity("PetHome.Domain.Models.CommonModels.Requisite", b =>
                {
                    b.HasOne("PetHome.Domain.Models.Pets.Pet", null)
                        .WithMany("Detailes")
                        .HasForeignKey("pet_id")
                        .HasConstraintName("fk_requisite_pets_pet_id");

                    b.HasOne("PetHome.Domain.Models.Volunteers.Volunteer", null)
                        .WithMany("Detailes")
                        .HasForeignKey("voluteer_id")
                        .HasConstraintName("fk_requisite_volunteer_voluteer_id");
                });

            modelBuilder.Entity("PetHome.Domain.Models.Pets.Breed", b =>
                {
                    b.HasOne("PetHome.Domain.Models.Pets.Species", null)
                        .WithMany("Breeds")
                        .HasForeignKey("species_id")
                        .HasConstraintName("fk_breeds_species_species_id");
                });

            modelBuilder.Entity("PetHome.Domain.Models.Pets.Pet", b =>
                {
                    b.HasOne("PetHome.Domain.Models.Volunteers.Volunteer", null)
                        .WithMany("Pets")
                        .HasForeignKey("voluteer_id")
                        .HasConstraintName("fk_pets_volunteer_voluteer_id");
                });

            modelBuilder.Entity("PetHome.Domain.Models.Pets.PetPhoto", b =>
                {
                    b.HasOne("PetHome.Domain.Models.Pets.Pet", null)
                        .WithMany("Photos")
                        .HasForeignKey("pet_id")
                        .HasConstraintName("fk_pet_photo_pets_pet_id");
                });

            modelBuilder.Entity("PetHome.Domain.Models.Volunteers.Volunteer", b =>
                {
                    b.OwnsOne("PetHome.Domain.Models.Volunteers.SocialNetworkCollection", "SocialNetworksValue", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteer");

                            b1.ToJson("SocialNetworksValue");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteer_volunteer_id");

                            b1.OwnsMany("PetHome.Domain.Models.Volunteers.SocialNetwork", "SocialNetworks", b2 =>
                                {
                                    b2.Property<Guid>("SocialNetworkCollectionVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Link")
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.Property<string>("Name")
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

                    b.Navigation("SocialNetworksValue")
                        .IsRequired();
                });

            modelBuilder.Entity("PetHome.Domain.Models.Pets.Pet", b =>
                {
                    b.Navigation("Detailes");

                    b.Navigation("Photos");
                });

            modelBuilder.Entity("PetHome.Domain.Models.Pets.Species", b =>
                {
                    b.Navigation("Breeds");
                });

            modelBuilder.Entity("PetHome.Domain.Models.Volunteers.Volunteer", b =>
                {
                    b.Navigation("Detailes");

                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}

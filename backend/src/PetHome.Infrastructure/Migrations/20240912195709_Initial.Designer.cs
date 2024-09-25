﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetHome.Infrastructure.DbContexts;

#nullable disable

namespace PetHome.Infrastructure.Migrations
{
    [DbContext(typeof(WriteDbContext))]
    [Migration("20240912195709_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PetHome.Domain.PetManadgement.AggregateRoot.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Requisites")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("requisites");

                    b.Property<string>("SocialNetworks")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("social_networks");

                    b.Property<bool>("_isDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_delete");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "PetHome.Domain.PetManadgement.AggregateRoot.Volunteer.Description#VolunteerDescription", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("character varying(2000)")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Email", "PetHome.Domain.PetManadgement.AggregateRoot.Volunteer.Email#Email", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("EmailValue")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("email");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Name", "PetHome.Domain.PetManadgement.AggregateRoot.Volunteer.Name#FullName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("first_name");

                            b1.Property<string>("SecondName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("second_name");

                            b1.Property<string>("Surname")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("surname");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Phone", "PetHome.Domain.PetManadgement.AggregateRoot.Volunteer.Phone#Phone", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("PhoneNumber")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("phone");
                        });

                    b.HasKey("Id")
                        .HasName("pk_volunteers");

                    b.ToTable("volunteers", (string)null);
                });

            modelBuilder.Entity("PetHome.Domain.PetManadgement.Entities.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("pet_id");

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

                    b.Property<double>("Weight")
                        .HasColumnType("double precision")
                        .HasColumnName("weight");

                    b.Property<bool>("_isDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_delete");

                    b.Property<Guid?>("volunteer_id")
                        .HasColumnType("uuid")
                        .HasColumnName("volunteer_id");

                    b.ComplexProperty<Dictionary<string, object>>("Address", "PetHome.Domain.PetManadgement.Entities.Pet.Address#Address", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("AppartmentNumber")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("appartment");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("city");

                            b1.Property<string>("HouseNumber")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("house");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("street");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("BirthDay", "PetHome.Domain.PetManadgement.Entities.Pet.BirthDay#DateValue", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<DateTime>("Date")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("birthday");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Color", "PetHome.Domain.PetManadgement.Entities.Pet.Color#PetColor", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Color")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("color");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("CreateDate", "PetHome.Domain.PetManadgement.Entities.Pet.CreateDate#DateValue", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<DateTime>("Date")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("create_date");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Description", "PetHome.Domain.PetManadgement.Entities.Pet.Description#PetDescription", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Health", "PetHome.Domain.PetManadgement.Entities.Pet.Health#HealthInfo", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Health")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("health");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Nickname", "PetHome.Domain.PetManadgement.Entities.Pet.Nickname#PetNickname", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Nickname")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("nickname");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Phone", "PetHome.Domain.PetManadgement.Entities.Pet.Phone#Phone", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("PhoneNumber")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("phone");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("SerialNumber", "PetHome.Domain.PetManadgement.Entities.Pet.SerialNumber#SerialNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("serial_number");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("SpeciesBreed", "PetHome.Domain.PetManadgement.Entities.Pet.SpeciesBreed#SpeciesBreedValue", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<Guid>("BreedId")
                                .HasColumnType("uuid")
                                .HasColumnName("breed_id");

                            b1.Property<Guid>("SpeciesId")
                                .HasColumnType("uuid")
                                .HasColumnName("species_id");
                        });

                    b.HasKey("Id")
                        .HasName("pk_pets");

                    b.HasIndex("volunteer_id")
                        .HasDatabaseName("ix_pets_volunteer_id");

                    b.ToTable("pets", (string)null);
                });

            modelBuilder.Entity("PetHome.Domain.PetManadgement.Entities.PetPhoto", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsMain")
                        .HasColumnType("boolean")
                        .HasColumnName("is_main");

                    b.Property<Guid?>("pet_id")
                        .HasColumnType("uuid")
                        .HasColumnName("pet_id");

                    b.ComplexProperty<Dictionary<string, object>>("Path", "PetHome.Domain.PetManadgement.Entities.PetPhoto.Path#FilePath", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Path")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("path");
                        });

                    b.HasKey("Id")
                        .HasName("pk_pet_photos");

                    b.HasIndex("pet_id")
                        .HasDatabaseName("ix_pet_photos_pet_id");

                    b.ToTable("pet_photos", (string)null);
                });

            modelBuilder.Entity("PetHome.Domain.SpeciesManagement.Entities.Species", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_species");

                    b.ToTable("species", (string)null);
                });

            modelBuilder.Entity("PetHome.Domain.SpeciesManagement.ValueObjects.Breed", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("breed_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<Guid?>("species_id")
                        .HasColumnType("uuid")
                        .HasColumnName("species_id");

                    b.HasKey("Id")
                        .HasName("pk_breeds");

                    b.HasIndex("species_id")
                        .HasDatabaseName("ix_breeds_species_id");

                    b.ToTable("breeds", (string)null);
                });

            modelBuilder.Entity("PetHome.Domain.PetManadgement.Entities.Pet", b =>
                {
                    b.HasOne("PetHome.Domain.PetManadgement.AggregateRoot.Volunteer", null)
                        .WithMany("Pets")
                        .HasForeignKey("volunteer_id")
                        .HasConstraintName("fk_pets_volunteers_volunteer_id");

                    b.OwnsOne("PetHome.Domain.PetManadgement.ValueObjects.PetRequisites", "Requisites", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid")
                                .HasColumnName("pet_id");

                            b1.HasKey("PetId");

                            b1.ToTable("pets");

                            b1.ToJson("requisites");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pets_pets_pet_id");

                            b1.OwnsMany("PetHome.Domain.Shared.Requisite", "Requisites", b2 =>
                                {
                                    b2.Property<Guid>("PetRequisitesPetId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Description")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.HasKey("PetRequisitesPetId", "Id")
                                        .HasName("pk_pets");

                                    b2.ToTable("pets");

                                    b2.WithOwner()
                                        .HasForeignKey("PetRequisitesPetId")
                                        .HasConstraintName("fk_pets_pets_pet_requisites_pet_id");
                                });

                            b1.Navigation("Requisites");
                        });

                    b.Navigation("Requisites")
                        .IsRequired();
                });

            modelBuilder.Entity("PetHome.Domain.PetManadgement.Entities.PetPhoto", b =>
                {
                    b.HasOne("PetHome.Domain.PetManadgement.Entities.Pet", null)
                        .WithMany("Photos")
                        .HasForeignKey("pet_id")
                        .HasConstraintName("fk_pet_photos_pets_pet_id");
                });

            modelBuilder.Entity("PetHome.Domain.SpeciesManagement.ValueObjects.Breed", b =>
                {
                    b.HasOne("PetHome.Domain.SpeciesManagement.Entities.Species", null)
                        .WithMany("Breeds")
                        .HasForeignKey("species_id")
                        .HasConstraintName("fk_breeds_species_species_id");
                });

            modelBuilder.Entity("PetHome.Domain.PetManadgement.AggregateRoot.Volunteer", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("PetHome.Domain.PetManadgement.Entities.Pet", b =>
                {
                    b.Navigation("Photos");
                });

            modelBuilder.Entity("PetHome.Domain.SpeciesManagement.Entities.Species", b =>
                {
                    b.Navigation("Breeds");
                });
#pragma warning restore 612, 618
        }
    }
}

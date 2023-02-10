﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using QuizGame.Server.DataAccess;

#nullable disable

namespace QuizGame.Server.DataAccess.Migrations
{
    [DbContext(typeof(QuizGameDbContext))]
    [Migration("20230210150817_doublyLinkedList")]
    partial class doublyLinkedList
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("QuizGame.Shared.Models.Answer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("QuizGame.Shared.Models.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PreviousId")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PreviousId")
                        .IsUnique();

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("QuizGame.Shared.Models.Quiz", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("FirstQuestionId")
                        .HasColumnType("uuid");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<Guid?>("LastQuestionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FirstQuestionId");

                    b.HasIndex("LastQuestionId");

                    b.ToTable("Quizzes");
                });

            modelBuilder.Entity("QuizGame.Shared.Models.Answer", b =>
                {
                    b.HasOne("QuizGame.Shared.Models.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("QuizGame.Shared.Models.Question", b =>
                {
                    b.HasOne("QuizGame.Shared.Models.Question", "Previous")
                        .WithOne("Next")
                        .HasForeignKey("QuizGame.Shared.Models.Question", "PreviousId");

                    b.Navigation("Previous");
                });

            modelBuilder.Entity("QuizGame.Shared.Models.Quiz", b =>
                {
                    b.HasOne("QuizGame.Shared.Models.Question", "FirstQuestion")
                        .WithMany()
                        .HasForeignKey("FirstQuestionId");

                    b.HasOne("QuizGame.Shared.Models.Question", "LastQuestion")
                        .WithMany()
                        .HasForeignKey("LastQuestionId");

                    b.Navigation("FirstQuestion");

                    b.Navigation("LastQuestion");
                });

            modelBuilder.Entity("QuizGame.Shared.Models.Question", b =>
                {
                    b.Navigation("Next");
                });
#pragma warning restore 612, 618
        }
    }
}

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
    [Migration("20230210113846_BaseQuizSetup")]
    partial class BaseQuizSetup
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("QuizGame.Shared.Answer", b =>
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

            modelBuilder.Entity("QuizGame.Shared.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("NextId")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("NextId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("QuizGame.Shared.Quiz", b =>
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

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FirstQuestionId");

                    b.ToTable("Quizzes");
                });

            modelBuilder.Entity("QuizGame.Shared.Answer", b =>
                {
                    b.HasOne("QuizGame.Shared.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("QuizGame.Shared.Question", b =>
                {
                    b.HasOne("QuizGame.Shared.Question", "Next")
                        .WithMany()
                        .HasForeignKey("NextId");

                    b.Navigation("Next");
                });

            modelBuilder.Entity("QuizGame.Shared.Quiz", b =>
                {
                    b.HasOne("QuizGame.Shared.Question", "FirstQuestion")
                        .WithMany()
                        .HasForeignKey("FirstQuestionId");

                    b.Navigation("FirstQuestion");
                });
#pragma warning restore 612, 618
        }
    }
}

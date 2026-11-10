using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SurveyLive.Data;

#nullable disable

namespace SurveyLive.Migrations
{
    [DbContext(typeof(SurveyDbContext))]
    partial class SurveyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "10.0.15");

            modelBuilder.Entity("SurveyLive.Models.AnswerOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Position")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuestionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId", "Position")
                        .IsUnique();

                    b.ToTable("AnswerOptions", t =>
                        {
                            t.HasCheckConstraint("CK_AnswerOptions_Position", "Position > 0");

                            t.HasCheckConstraint("CK_AnswerOptions_Text", "length(trim(Text)) BETWEEN 1 AND 200");
                        });

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Position = 1,
                            QuestionId = 1,
                            Text = "Кратък код"
                        },
                        new
                        {
                            Id = 2,
                            Position = 2,
                            QuestionId = 1,
                            Text = "QR код"
                        },
                        new
                        {
                            Id = 3,
                            Position = 3,
                            QuestionId = 1,
                            Text = "Директна връзка"
                        });
                });

            modelBuilder.Entity("SurveyLive.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Position")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SurveyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId", "Position")
                        .IsUnique();

                    b.ToTable("Questions", t =>
                        {
                            t.HasCheckConstraint("CK_Questions_Position", "Position > 0");

                            t.HasCheckConstraint("CK_Questions_Text", "length(trim(Text)) BETWEEN 1 AND 300");
                        });

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Position = 1,
                            SurveyId = 1,
                            Text = "Кой начин за присъединяване предпочиташ?"
                        });
                });

            modelBuilder.Entity("SurveyLive.Models.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("JoinCode")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("JoinCode")
                        .IsUnique();

                    b.ToTable("Surveys", t =>
                        {
                            t.HasCheckConstraint("CK_Surveys_JoinCode", "length(JoinCode) = 6 AND JoinCode NOT GLOB '*[^0-9]*'");

                            t.HasCheckConstraint("CK_Surveys_Title", "length(trim(Title)) BETWEEN 1 AND 150");
                        });

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2026, 11, 10, 0, 0, 0, 0, DateTimeKind.Utc),
                            IsActive = false,
                            JoinCode = "123456",
                            Title = "Участие в интерактивни анкети"
                        });
                });

            modelBuilder.Entity("SurveyLive.Models.Vote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AnswerOptionId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("ParticipantId")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("TEXT");

                    b.Property<int>("QuestionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AnswerOptionId", "QuestionId");

                    b.HasIndex("QuestionId", "ParticipantId")
                        .IsUnique();

                    b.ToTable("Votes", t =>
                        {
                            t.HasCheckConstraint("CK_Votes_ParticipantId", "length(trim(ParticipantId)) BETWEEN 1 AND 64");
                        });
                });

            modelBuilder.Entity("SurveyLive.Models.AnswerOption", b =>
                {
                    b.HasOne("SurveyLive.Models.Question", null)
                        .WithMany("Options")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SurveyLive.Models.Question", b =>
                {
                    b.HasOne("SurveyLive.Models.Survey", null)
                        .WithMany("Questions")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SurveyLive.Models.Vote", b =>
                {
                    b.HasOne("SurveyLive.Models.Question", null)
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SurveyLive.Models.AnswerOption", null)
                        .WithMany()
                        .HasForeignKey("AnswerOptionId", "QuestionId")
                        .HasPrincipalKey("Id", "QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SurveyLive.Models.Question", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("SurveyLive.Models.Survey", b =>
                {
                    b.Navigation("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}

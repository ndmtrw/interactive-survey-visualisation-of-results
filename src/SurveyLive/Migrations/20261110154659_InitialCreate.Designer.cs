using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SurveyLive.Data;

#nullable disable

namespace SurveyLive.Migrations
{
    [DbContext(typeof(SurveyDbContext))]
    [Migration("20261110154659_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "10.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SurveyLive.Models.AnswerOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId", "Position")
                        .IsUnique();

                    b.ToTable("AnswerOptions", t =>
                        {
                            t.HasCheckConstraint("CK_AnswerOptions_Position", "Position > 0");

                            t.HasCheckConstraint("CK_AnswerOptions_Text", "LEN(LTRIM(RTRIM([Text]))) > 0");
                        });
                });

            modelBuilder.Entity("SurveyLive.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<int>("SurveyId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId", "Position")
                        .IsUnique();

                    b.ToTable("Questions", t =>
                        {
                            t.HasCheckConstraint("CK_Questions_Position", "Position > 0");

                            t.HasCheckConstraint("CK_Questions_Text", "LEN(LTRIM(RTRIM([Text]))) > 0");
                        });
                });

            modelBuilder.Entity("SurveyLive.Models.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Surveys", t =>
                        {
                            t.HasCheckConstraint("CK_Surveys_Title", "LEN(LTRIM(RTRIM([Title]))) > 0");
                        });
                });

            modelBuilder.Entity("SurveyLive.Models.SurveySession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("JoinCode")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<int>("SurveyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("JoinCode")
                        .IsUnique();

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveySessions", t =>
                        {
                            t.HasCheckConstraint("CK_SurveySessions_JoinCode", "LEN([JoinCode]) = 6 AND [JoinCode] COLLATE Latin1_General_100_BIN2 NOT LIKE '%[^0-9]%'");
                        });
                });

            modelBuilder.Entity("SurveyLive.Models.Vote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnswerOptionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ParticipantId")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int>("SurveyId")
                        .HasColumnType("int");

                    b.Property<int>("SurveySessionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnswerOptionId", "QuestionId");

                    b.HasIndex("QuestionId", "SurveyId");

                    b.HasIndex("SurveySessionId", "SurveyId");

                    b.HasIndex("SurveySessionId", "QuestionId", "ParticipantId")
                        .IsUnique();

                    b.ToTable("Votes", t =>
                        {
                            t.HasCheckConstraint("CK_Votes_ParticipantId", "LEN(LTRIM(RTRIM([ParticipantId]))) > 0");
                        });
                });

            modelBuilder.Entity("SurveyLive.Models.AnswerOption", b =>
                {
                    b.HasOne("SurveyLive.Models.Question", null)
                        .WithMany("Options")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("SurveyLive.Models.Question", b =>
                {
                    b.HasOne("SurveyLive.Models.Survey", null)
                        .WithMany("Questions")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("SurveyLive.Models.SurveySession", b =>
                {
                    b.HasOne("SurveyLive.Models.Survey", null)
                        .WithMany()
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("SurveyLive.Models.Vote", b =>
                {
                    b.HasOne("SurveyLive.Models.AnswerOption", null)
                        .WithMany()
                        .HasForeignKey("AnswerOptionId", "QuestionId")
                        .HasPrincipalKey("Id", "QuestionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SurveyLive.Models.Question", null)
                        .WithMany()
                        .HasForeignKey("QuestionId", "SurveyId")
                        .HasPrincipalKey("Id", "SurveyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SurveyLive.Models.SurveySession", null)
                        .WithMany()
                        .HasForeignKey("SurveySessionId", "SurveyId")
                        .HasPrincipalKey("Id", "SurveyId")
                        .OnDelete(DeleteBehavior.NoAction)
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

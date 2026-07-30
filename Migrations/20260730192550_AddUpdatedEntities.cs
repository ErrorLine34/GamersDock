using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamersDock.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountLinks",
                columns: table => new
                {
                    AccountLinkId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProfileId = table.Column<int>(type: "INTEGER", nullable: false),
                    SteamLinked = table.Column<bool>(type: "INTEGER", nullable: false),
                    SteamAccessToken = table.Column<string>(type: "TEXT", nullable: true),
                    XboxLinked = table.Column<bool>(type: "INTEGER", nullable: false),
                    XboxAccessToken = table.Column<string>(type: "TEXT", nullable: true),
                    XboxRefreshToken = table.Column<string>(type: "TEXT", nullable: true),
                    PsnUsername = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountLinks", x => x.AccountLinkId);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExternalId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Developer = table.Column<string>(type: "TEXT", nullable: true),
                    Publisher = table.Column<string>(type: "TEXT", nullable: true),
                    BasePrice = table.Column<float>(type: "REAL", nullable: true),
                    Metascore = table.Column<float>(type: "REAL", nullable: true),
                    AverageRating = table.Column<float>(type: "REAL", nullable: false),
                    FranchiseId = table.Column<string>(type: "TEXT", nullable: true),
                    EditionLabel = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "InstanceSettings",
                columns: table => new
                {
                    InstanceSettingsId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PsnServiceAccountLinked = table.Column<bool>(type: "INTEGER", nullable: false),
                    PsnServiceAccountLabel = table.Column<string>(type: "TEXT", nullable: true),
                    PsnAccessToken = table.Column<string>(type: "TEXT", nullable: true),
                    PsnRefreshToken = table.Column<string>(type: "TEXT", nullable: true),
                    DefaultRegion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstanceSettings", x => x.InstanceSettingsId);
                });

            migrationBuilder.CreateTable(
                name: "JournalEntries",
                columns: table => new
                {
                    JournalEntryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    profileId = table.Column<int>(type: "INTEGER", nullable: false),
                    GameId = table.Column<int>(type: "INTEGER", nullable: false),
                    LibraryEntryId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HoursAtEntry = table.Column<int>(type: "INTEGER", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    Mood = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntries", x => x.JournalEntryId);
                });

            migrationBuilder.CreateTable(
                name: "LibraryEntries",
                columns: table => new
                {
                    LibraryEntryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameId = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Rating = table.Column<float>(type: "REAL", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ProfileId = table.Column<int>(type: "INTEGER", nullable: false),
                    HoursPlayed = table.Column<float>(type: "REAL", nullable: false),
                    StartedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DroppedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastPlayed = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryEntries", x => x.LibraryEntryId);
                });

            migrationBuilder.CreateTable(
                name: "PlatformSkus",
                columns: table => new
                {
                    PlatformSkuId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlatformId = table.Column<int>(type: "INTEGER", nullable: false),
                    StoreName = table.Column<string>(type: "TEXT", nullable: true),
                    ExternalId = table.Column<string>(type: "TEXT", nullable: true),
                    StoreUrl = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformSkus", x => x.PlatformSkuId);
                });

            migrationBuilder.CreateTable(
                name: "ProfileAchievements",
                columns: table => new
                {
                    ProfileAchievementId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProfileId = table.Column<int>(type: "INTEGER", nullable: false),
                    AchievementId = table.Column<string>(type: "TEXT", nullable: true),
                    Unlocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    UnlockedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileAchievements", x => x.ProfileAchievementId);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    ProfileId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProfileName = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Avatar = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.ProfileId);
                    table.ForeignKey(
                        name: "FK_Profiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameMedias",
                columns: table => new
                {
                    GameMediaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameId = table.Column<int>(type: "INTEGER", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    GamesGameId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameMedias", x => x.GameMediaId);
                    table.ForeignKey(
                        name: "FK_GameMedias_Games_GamesGameId",
                        column: x => x.GamesGameId,
                        principalTable: "Games",
                        principalColumn: "GameId");
                });

            migrationBuilder.CreateTable(
                name: "PriceHistories",
                columns: table => new
                {
                    PriceHistoryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameId = table.Column<int>(type: "INTEGER", nullable: false),
                    RecordedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Region = table.Column<string>(type: "TEXT", nullable: true),
                    StoreLinkId = table.Column<int>(type: "INTEGER", nullable: true),
                    DiscountPercent = table.Column<float>(type: "REAL", nullable: true),
                    GamesGameId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceHistories", x => x.PriceHistoryId);
                    table.ForeignKey(
                        name: "FK_PriceHistories_Games_GamesGameId",
                        column: x => x.GamesGameId,
                        principalTable: "Games",
                        principalColumn: "GameId");
                });

            migrationBuilder.CreateTable(
                name: "StoreLinks",
                columns: table => new
                {
                    StoreLinkId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameId = table.Column<int>(type: "INTEGER", nullable: false),
                    StoreName = table.Column<string>(type: "TEXT", nullable: true),
                    Url = table.Column<string>(type: "TEXT", nullable: true),
                    CurrentDiscountPercentage = table.Column<float>(type: "REAL", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    GamesGameId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreLinks", x => x.StoreLinkId);
                    table.ForeignKey(
                        name: "FK_StoreLinks_Games_GamesGameId",
                        column: x => x.GamesGameId,
                        principalTable: "Games",
                        principalColumn: "GameId");
                });

            migrationBuilder.CreateTable(
                name: "GamesGenre",
                columns: table => new
                {
                    GamesGameId = table.Column<int>(type: "INTEGER", nullable: false),
                    GenresGenreId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesGenre", x => new { x.GamesGameId, x.GenresGenreId });
                    table.ForeignKey(
                        name: "FK_GamesGenre_Games_GamesGameId",
                        column: x => x.GamesGameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamesGenre_Genres_GenresGenreId",
                        column: x => x.GenresGenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Platforms",
                columns: table => new
                {
                    PlatformId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    LibraryEntryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platforms", x => x.PlatformId);
                    table.ForeignKey(
                        name: "FK_Platforms_LibraryEntries_LibraryEntryId",
                        column: x => x.LibraryEntryId,
                        principalTable: "LibraryEntries",
                        principalColumn: "LibraryEntryId");
                });

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    AchievementId = table.Column<string>(type: "TEXT", nullable: false),
                    GameId = table.Column<int>(type: "INTEGER", nullable: false),
                    AchievementName = table.Column<string>(type: "TEXT", nullable: true),
                    AchievementPlatformPlatformId = table.Column<int>(type: "INTEGER", nullable: true),
                    AchievementType = table.Column<string>(type: "TEXT", nullable: true),
                    AchievementDescription = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.AchievementId);
                    table.ForeignKey(
                        name: "FK_Achievements_Platforms_AchievementPlatformPlatformId",
                        column: x => x.AchievementPlatformPlatformId,
                        principalTable: "Platforms",
                        principalColumn: "PlatformId");
                });

            migrationBuilder.CreateTable(
                name: "GamesPlatform",
                columns: table => new
                {
                    GamesGameId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlatformsPlatformId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesPlatform", x => new { x.GamesGameId, x.PlatformsPlatformId });
                    table.ForeignKey(
                        name: "FK_GamesPlatform_Games_GamesGameId",
                        column: x => x.GamesGameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamesPlatform_Platforms_PlatformsPlatformId",
                        column: x => x.PlatformsPlatformId,
                        principalTable: "Platforms",
                        principalColumn: "PlatformId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_AchievementPlatformPlatformId",
                table: "Achievements",
                column: "AchievementPlatformPlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_GameMedias_GamesGameId",
                table: "GameMedias",
                column: "GamesGameId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesGenre_GenresGenreId",
                table: "GamesGenre",
                column: "GenresGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesPlatform_PlatformsPlatformId",
                table: "GamesPlatform",
                column: "PlatformsPlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_Platforms_LibraryEntryId",
                table: "Platforms",
                column: "LibraryEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceHistories_GamesGameId",
                table: "PriceHistories",
                column: "GamesGameId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreLinks_GamesGameId",
                table: "StoreLinks",
                column: "GamesGameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountLinks");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "GameMedias");

            migrationBuilder.DropTable(
                name: "GamesGenre");

            migrationBuilder.DropTable(
                name: "GamesPlatform");

            migrationBuilder.DropTable(
                name: "InstanceSettings");

            migrationBuilder.DropTable(
                name: "JournalEntries");

            migrationBuilder.DropTable(
                name: "PlatformSkus");

            migrationBuilder.DropTable(
                name: "PriceHistories");

            migrationBuilder.DropTable(
                name: "ProfileAchievements");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "StoreLinks");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Platforms");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "LibraryEntries");
        }
    }
}

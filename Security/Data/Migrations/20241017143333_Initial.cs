using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Security.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "APP_SEC_PERMISSIONS",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    NormalizedName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APP_SEC_PERMISSIONS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "APP_SEC_ROLES",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APP_SEC_ROLES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "APP_SEC_USERS",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APP_SEC_USERS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "APP_SEC_ROLE_PERMISSIONS",
                columns: table => new
                {
                    PermissionsId = table.Column<string>(type: "TEXT", nullable: false),
                    RolesId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APP_SEC_ROLE_PERMISSIONS", x => new { x.PermissionsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_APP_SEC_ROLE_PERMISSIONS_APP_SEC_PERMISSIONS_PermissionsId",
                        column: x => x.PermissionsId,
                        principalTable: "APP_SEC_PERMISSIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_APP_SEC_ROLE_PERMISSIONS_APP_SEC_ROLES_RolesId",
                        column: x => x.RolesId,
                        principalTable: "APP_SEC_ROLES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "APP_SEC_ROLES_CLAIMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APP_SEC_ROLES_CLAIMS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_APP_SEC_ROLES_CLAIMS_APP_SEC_ROLES_RoleId",
                        column: x => x.RoleId,
                        principalTable: "APP_SEC_ROLES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "APP_SEC_USER_CLAIMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APP_SEC_USER_CLAIMS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_APP_SEC_USER_CLAIMS_APP_SEC_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "APP_SEC_USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "APP_SEC_USER_LOGINS",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APP_SEC_USER_LOGINS", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_APP_SEC_USER_LOGINS_APP_SEC_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "APP_SEC_USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "APP_SEC_USER_ROLES",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APP_SEC_USER_ROLES", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_APP_SEC_USER_ROLES_APP_SEC_ROLES_RoleId",
                        column: x => x.RoleId,
                        principalTable: "APP_SEC_ROLES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_APP_SEC_USER_ROLES_APP_SEC_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "APP_SEC_USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "APP_SEC_USER_TOKEN",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APP_SEC_USER_TOKEN", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_APP_SEC_USER_TOKEN_APP_SEC_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "APP_SEC_USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_APP_SEC_ROLE_PERMISSIONS_RolesId",
                table: "APP_SEC_ROLE_PERMISSIONS",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "APP_SEC_ROLES",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_APP_SEC_ROLES_CLAIMS_RoleId",
                table: "APP_SEC_ROLES_CLAIMS",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_APP_SEC_USER_CLAIMS_UserId",
                table: "APP_SEC_USER_CLAIMS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_APP_SEC_USER_LOGINS_UserId",
                table: "APP_SEC_USER_LOGINS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_APP_SEC_USER_ROLES_RoleId",
                table: "APP_SEC_USER_ROLES",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "APP_SEC_USERS",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "APP_SEC_USERS",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APP_SEC_ROLE_PERMISSIONS");

            migrationBuilder.DropTable(
                name: "APP_SEC_ROLES_CLAIMS");

            migrationBuilder.DropTable(
                name: "APP_SEC_USER_CLAIMS");

            migrationBuilder.DropTable(
                name: "APP_SEC_USER_LOGINS");

            migrationBuilder.DropTable(
                name: "APP_SEC_USER_ROLES");

            migrationBuilder.DropTable(
                name: "APP_SEC_USER_TOKEN");

            migrationBuilder.DropTable(
                name: "APP_SEC_PERMISSIONS");

            migrationBuilder.DropTable(
                name: "APP_SEC_ROLES");

            migrationBuilder.DropTable(
                name: "APP_SEC_USERS");
        }
    }
}

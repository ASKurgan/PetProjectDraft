using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetProjectDraft.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "photos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_main = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_photos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    password_hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telegram_id = table.Column<long>(type: "bigint", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    permissions = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "volunteer_applications",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    years_experience = table.Column<int>(type: "int", nullable: false),
                    number_of_pets_found_home = table.Column<int>(type: "int", nullable: true),
                    from_shelter = table.Column<bool>(type: "bit", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    patronymic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_volunteer_applications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "volunteers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    years_experience = table.Column<int>(type: "int", nullable: false),
                    number_of_pets_found_home = table.Column<int>(type: "int", nullable: true),
                    donation_info = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    from_shelter = table.Column<bool>(type: "bit", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    patronymic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    social_medias = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_volunteers", x => x.id);
                    table.ForeignKey(
                        name: "fk_volunteers_users_id",
                        column: x => x.id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nickname = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    breed = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    color = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    people_attitude = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    animal_attitude = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    health = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    castration = table.Column<bool>(type: "bit", nullable: false),
                    only_one_in_family = table.Column<bool>(type: "bit", nullable: false),
                    on_treatment = table.Column<bool>(type: "bit", nullable: false),
                    height = table.Column<int>(type: "int", nullable: true),
                    birth_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    created_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    volunteer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    building = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    city = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    index = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    street = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    contact_phone_number = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    place = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    volunteer_phone_number = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    weight = table.Column<float>(type: "real", nullable: false),
                    vaccinations = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pets", x => x.id);
                    table.ForeignKey(
                        name: "fk_pets_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "volunteers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "volunteer_photo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    volunteer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_volunteer_photo", x => x.id);
                    table.ForeignKey(
                        name: "fk_volunteer_photo_photos_id",
                        column: x => x.id,
                        principalTable: "photos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_volunteer_photo_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "volunteers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pet_photos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    pet_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_main = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pet_photos", x => x.id);
                    table.ForeignKey(
                        name: "fk_pet_photos_pets_pet_id",
                        column: x => x.pet_id,
                        principalTable: "pets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_pet_photos_pet_id",
                table: "pet_photos",
                column: "pet_id");

            migrationBuilder.CreateIndex(
                name: "ix_pets_volunteer_id",
                table: "pets",
                column: "volunteer_id");

            migrationBuilder.CreateIndex(
                name: "ix_volunteer_photo_volunteer_id",
                table: "volunteer_photo",
                column: "volunteer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pet_photos");

            migrationBuilder.DropTable(
                name: "volunteer_applications");

            migrationBuilder.DropTable(
                name: "volunteer_photo");

            migrationBuilder.DropTable(
                name: "pets");

            migrationBuilder.DropTable(
                name: "photos");

            migrationBuilder.DropTable(
                name: "volunteers");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}

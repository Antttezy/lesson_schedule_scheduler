namespace Scheduler.Migrations
{
    public partial class OptimizeWorkload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workloads_Groups_GroupId",
                table: "Workloads");

            migrationBuilder.DropIndex(
                name: "IX_Workloads_GroupId",
                table: "Workloads");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Workloads");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GroupWorkload",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                    GroupsId = table.Column<int>(type: "int", nullable: false),
                    WorkloadsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupWorkload", x => new { x.Id });
                    table.ForeignKey(
                        name: "FK_GroupWorkload_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupWorkload_Workloads_WorkloadsId",
                        column: x => x.WorkloadsId,
                        principalTable: "Workloads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_GroupId",
                table: "Lessons",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupWorkload_WorkloadsId",
                table: "GroupWorkload",
                column: "WorkloadsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Groups_GroupId",
                table: "Lessons",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Groups_GroupId",
                table: "Lessons");

            migrationBuilder.DropTable(
                name: "GroupWorkload");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_GroupId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Lessons");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Workloads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Workloads_GroupId",
                table: "Workloads",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workloads_Groups_GroupId",
                table: "Workloads",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

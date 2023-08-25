using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InsertCouriers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"insert into public.""AvailableCouriers"" (""Date"", ""Count"") 
values ('2023-08-21', 20), ('2023-08-22', 20), ('2023-08-23', 20), ('2023-08-24', 20), ('2023-08-25', 20), ('2023-08-26', 20), ('2023-08-27', 20), ('2023-08-28', 20), ('2023-08-29', 20), ('2023-08-30', 20)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

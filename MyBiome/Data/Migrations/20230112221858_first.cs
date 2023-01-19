using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBiome.Data.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            //Costumers

            //migrationBuilder.CreateTable(
            //    name: "Costumers",
            //    columns: table => new
            //    {
            //        CostumersId = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        FirtName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        District = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        City = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        phone = table.Column<int>(type: "int", nullable: true)

            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Costumers", x => x.CostumersId);

            //    });

            //Employees

            //migrationBuilder.CreateTable(
            //   name: "Employees",
            //   columns: table => new
            //   {

            //       EmployeesId = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),

            //   },
            //   constraints: table =>
            //   {
            //       table.PrimaryKey("PK_Costumers", x => x.EmployeesId);

            //   });


            //Categories


            //CategoriesProduct


            //Products


            //ProductSize


            //Shippers


            //Orders


            //OrderItems


            //Suppliers


            //Favorites


            //ProductsImages


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderClient.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Order_ClientID",
                table: "Order",
                column: "ClientID");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Client_ClientID",
                table: "Order",
                column: "ClientID",
                principalTable: "Client",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Client_ClientID",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_ClientID",
                table: "Order");
        }
    }
}

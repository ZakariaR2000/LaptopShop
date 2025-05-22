using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LapShop.Migrations
{
    /// <inheritdoc />
    public partial class addViewItmeCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create view VwItemCategory
                                    as
                                    SELECT dbo.TbItems.ItemId, dbo.TbItems.SalesPrice, dbo.TbItems.PurchasePrice, dbo.TbItems.CategoryId, dbo.TbItems.ImageName, dbo.TbItems.Gpu, dbo.TbItems.HardDisk, dbo.TbItems.ItemTypeId, dbo.TbItems.RamSize, 
                                                      dbo.TbItems.Processor, dbo.TbItems.ScreenReslution, dbo.TbItems.ScreenSize, dbo.TbItems.Weight, dbo.TbItems.OsId, dbo.TbCategories.CategoryName
                                    FROM     dbo.TbItems INNER JOIN
                                                      dbo.TbCategories ON dbo.TbItems.CategoryId = dbo.TbCategories.CategoryId");
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

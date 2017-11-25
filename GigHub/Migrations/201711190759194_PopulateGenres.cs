namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateGenres : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO GENRES(Id,Name) VALUES(1,'Jazz')");
            Sql("INSERT INTO GENRES(Id,Name) VALUES(2,'Rock')");
            Sql("INSERT INTO GENRES(Id,Name) VALUES(3,'Classical')");

        }

        public override void Down()
        {
            Sql("DELETE FROM GENRES WHERE ID IN (1,2,3)");
        }
    }
}

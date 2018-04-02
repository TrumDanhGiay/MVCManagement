namespace Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIdentity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Address", c => c.String(maxLength: 500));
            AlterColumn("dbo.AspNetUsers", "Avartar", c => c.String(maxLength: 256));
            AlterColumn("dbo.AspNetUsers", "FullName", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "FullName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Avartar", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Address", c => c.String());
        }
    }
}

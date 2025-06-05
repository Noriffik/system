using System.Data;
using ThinkingHome.Migrator.Framework;
using ThinkingHome.Migrator.Framework.Extensions;

namespace ThinkingHome.Plugins.TelegramBot.Model.Migrations;

[Migration(1)]
public class Migration01 : Migration {
    private const int MAX_LOGIN_LENGTH = 32;
    public override void Apply()
    {
        Database.AddTable("TelegramBot_Chats", 
            new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey),
            new Column("Login", DbType.String.WithSize(MAX_LOGIN_LENGTH), ColumnProperty.Null),
            new Column("ChatId", DbType.Int64, ColumnProperty.NotNull));
        Database.AddUniqueConstraint("UK_TelegramBot_Chats_ChatId", "TelegramBot_Chats", "ChatId");
        
    }
    
    public override void Revert()
    {
        Database.RemoveTable("TelegramBot_Chats");
    }
}

using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public class CustomProcedure
{
    [SqlProcedure]
    public static void GetDebitorByName(SqlString name)
    {
        using var connection = new SqlConnection("context connection = true");
        connection.Open();

        var command = new SqlCommand("Select * From Debitors " +
                                     "Where Name = @Name", connection);

        command.Parameters.AddWithValue("@Name", name);

        SqlContext.Pipe.ExecuteAndSend(command);
    }
}

using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Context;

public class AppDbContexto
{
    #region ImpressaoRemotaService

    public AppDbContexto(IConfiguration connectionString)
    {
        try
        {
            _connectionString = connectionString.GetConnectionString("VippCs");
            sqlConnection = new SqlConnection(_connectionString);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    #endregion

    #region connect

    public SqlConnection connect()
    {
        try
        {
            sqlConnection.Open();
            return sqlConnection;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return sqlConnection;
        }
    }

    #endregion

    #region desConnect

    public SqlConnection desConnect()
    {
        try
        {
            sqlConnection.Close();
            return sqlConnection;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return sqlConnection;
        }
    }

    #endregion

    #region atributos

    private readonly string _connectionString;
    private readonly SqlConnection sqlConnection;

    #endregion
}
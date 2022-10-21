using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CommonLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class DBResult : IDisposable
    {
        public string CommandText { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }

        public int AffectedRow { get; set; } = -1;
        public object ReturnedObject { get; set; }

        public DataSet DataSet { get; set; }
        public SqlParameter[] Parameters { get; set; }

        ~DBResult()
        {
            this.Dispose(false);
        }

        private bool disposed;
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed) return;
            if (disposing)
            {
                // IDisposable 인터페이스를 구현하는 멤버들을 여기서 정리합니다.
                try
                {
                    if (DataSet != null)
                    {
                        DataSet.Clear();
                        DataSet.Dispose();
                    }
                    if (Parameters != null && Parameters.Count() > 0)
                    {
                        Parameters = null;
                    }
                }
                catch { }
                finally { DataSet = null; }
            }
            // .NET Framework에 의하여 관리되지 않는 외부 리소스들을 여기서 정리합니다.
            this.disposed = true;
        }
    }
}

public class MSSQLExt
{
    public extensionObj instance { get; set; }

    public MSSQLExt(string strConnString)
    {
        if (instance != null)
        {
            instance = null;
        }
        instance = new extensionObj(strConnString);
    }

    public class extensionObj
    {
        public string ConnectionString { get; set; }
        public extensionObj(string strConnString)
        {
            ConnectionString = strConnString;
        }

        public int ExecuteNonQuery(string strQuery, params SqlParameter[] SqlParam)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(strQuery, conn))
                    {
                        cmd.CommandTimeout = 15;
                        cmd.CommandType = CommandType.Text;
                        foreach (SqlParameter param in SqlParam)
                        {
                            cmd.Parameters.AddWithValue(param.ParameterName, param.Value);
                        }
                        int affected = cmd.ExecuteNonQuery();
                        return affected;
                    }
                }
            }
            catch
            {
                return -2;
            }
        }

        public int ExecuteNonQuery(SqlCommand cmd)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    cmd.Connection = conn;
                    conn.Open();
                    int affected = cmd.ExecuteNonQuery();
                    return affected;
                }

            }
            catch
            {
                return -1;
            }
        }

        public object ExecuteScalar(string strQuery, params SqlParameter[] SqlParam)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(strQuery, conn))
                    {
                        conn.Open();
                        cmd.CommandTimeout = 5;
                        cmd.CommandType = CommandType.Text;

                        foreach (SqlParameter param in SqlParam)
                        {
                            cmd.Parameters.AddWithValue(param.ParameterName, param.Value);
                        }

                        object tmp = cmd.ExecuteScalar();
                        return tmp;
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public string ExecuteSP(string strQuery, params SqlParameter[] SqlParam)
        {
            try
            {
                //SqlParameter ret = null;
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(strQuery, conn))
                    {
                        cmd.CommandTimeout = 30;
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (SqlParameter param in SqlParam)
                        {
                            if (param.Direction == ParameterDirection.Input)
                            {
                                cmd.Parameters.AddWithValue(param.ParameterName, param.Value);
                            }
                            else
                            {
                                cmd.Parameters.Add(param);
                            }
                        }
                        cmd.Parameters.Add(new SqlParameter("@ERR_MSG", SqlDbType.NVarChar, 200));
                        cmd.Parameters["@ERR_MSG"].Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        if (cmd.Parameters["@ERR_MSG"].Value != DBNull.Value)
                        {
                            return cmd.Parameters["@ERR_MSG"].Value.ToString();
                        }

                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public DataTable GetDataTable(string strQuery, params SqlParameter[] SqlParam)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    var table = new DataTable();
                    using (SqlCommand cmd = new SqlCommand(strQuery, conn))
                    {
                        cmd.CommandTimeout = 30;
                        cmd.CommandType = CommandType.Text;
                        foreach (SqlParameter param in SqlParam)
                        {
                            cmd.Parameters.AddWithValue(param.ParameterName, param.Value);
                        }
                        using (var r = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            table.Load(r);
                        }
                    }
                    return table;
                }
            }
            catch
            {
                return null;
            }
        }

        public DataSet GetDataset(string strQuery, params SqlParameter[] SqlParam)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(strQuery, conn))
                    {
                        cmd.CommandTimeout = 3;
                        cmd.CommandType = CommandType.Text;

                        foreach (SqlParameter param in SqlParam)
                        {
                            cmd.Parameters.AddWithValue(param.ParameterName, param.Value);
                        }
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                        return ds;
                    }
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                ds?.Dispose();
            }
        }

        public List<S> ReadList<S>(string query, Func<IDataRecord, S> selector, params SqlParameter[] SqlParam)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Connection.Open();
                        foreach (SqlParameter param in SqlParam)
                        {
                            cmd.Parameters.AddWithValue(param.ParameterName, param.Value);
                        }

                        using (var r = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            var items = new List<S>();
                            while (r.Read())
                                items.Add(selector(r));
                            return items;
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public List<S> ReadSP<S>(string query, Func<IDataRecord, S> selector, params SqlParameter[] SqlParam)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection.Open();

                        foreach (SqlParameter param in SqlParam)
                        {
                            cmd.Parameters.AddWithValue(param.ParameterName, param.Value);
                        }

                        cmd.Parameters.Add(new SqlParameter("@ERR_MSG", SqlDbType.NVarChar, 200));
                        cmd.Parameters["@ERR_MSG"].Direction = ParameterDirection.Output;

                        using (var r = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            var items = new List<S>();
                            while (r.Read())
                                items.Add(selector(r));
                            return items;
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
        }
    }

}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Clariant.DTable.Validation
{
    public class DTables
    {
        static (List<string> tables, Dictionary<string, List<string>> tblclmns) GetTable(string constr)
        {
            IDbConnection con = null;
            string sql = "";
            bool onepassed = false;
            List<string> lst = new List<string>();
            Dictionary<string, List<string>> lblclms = new Dictionary<string, List<string>>();
            (new List<string>() { "MsSql", "Postgres" }).ForEach(t =>
            {
                try
                {
                    if (t == "MsSql")
                    {
                        using (con = new System.Data.SqlClient.SqlConnection(constr))
                        {
                            sql = "select name from sys.tables";
                            con.Open();
                            using (var cmd = con.CreateCommand())
                            {
                                cmd.CommandText = sql;
                                cmd.CommandType = CommandType.Text;
                                var rdr = cmd.ExecuteReader();
                                while (rdr.Read())
                                {
                                    var tblname = rdr.GetString(rdr.GetOrdinal("name"));
                                    lst.Add(tblname);
                                    if (!lblclms.ContainsKey(tblname)) lblclms.Add(tblname, new List<string>());
                                }
                            }
                        }
                        lblclms.ToList().ForEach(rr =>
                        {
                            using (con = new System.Data.SqlClient.SqlConnection(constr))
                            {
                                con.Open();
                                using (var cmd = con.CreateCommand())
                                {
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = $"select * from {rr.Key} where  1 = 2;";
                                    var rtrd = cmd.ExecuteReader();
                                    Enumerable.Range(0, rtrd.FieldCount).ToList().ForEach(t1 =>
                                    {
                                        lblclms[rr.Key].Add(rtrd.GetName(t1));
                                    });
                                }
                            }
                        });
                    }
                    if (t == "Postgres")
                    {
                        using (con = new Npgsql.NpgsqlConnection(constr))
                        {
                            sql = @"SELECT table_name as name
                                    FROM information_schema.tables
                                    WHERE table_schema = 'public'
                                    ORDER BY table_name; ";
                            con.Open();
                            using (var cmd = con.CreateCommand())
                            {
                                cmd.CommandText = sql;
                                cmd.CommandType = CommandType.Text;
                                var rdr = cmd.ExecuteReader();
                                while (rdr.Read())
                                {
                                    var tblname = rdr.GetString(rdr.GetOrdinal("name"));
                                    lst.Add(tblname);
                                    if (!lblclms.ContainsKey(tblname)) lblclms.Add(tblname, new List<string>());
                                }
                            }
                        }
                        lblclms.ToList().ForEach(rr =>
                        {
                            using (con = new Npgsql.NpgsqlConnection(constr))
                            {
                                con.Open();
                                using (var cmd = con.CreateCommand())
                                {
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = $"select * from \"{rr.Key}\" where  1 = 2;";
                                    var rtrd = cmd.ExecuteReader();
                                    Enumerable.Range(0, rtrd.FieldCount).ToList().ForEach(t1 =>
                                    {
                                        lblclms[rr.Key].Add(rtrd.GetName(t1));
                                    });
                                }
                            }
                        });
                    }
                    onepassed = true;
                }
                catch (Exception exx)
                {
                    con = null;
                }
            });
            if (!onepassed) throw new ApplicationException($"Invalid Connection String - {constr}");
            return (lst, lblclms);
        }
        public static void GetTables(DtModel model)
        {
            var constr = model.src;
            var tbls = GetTable(model.src);
            model.srctbls = tbls.tables;
            model.srctblclmns = tbls.tblclmns;
            constr = model.dest;
            tbls = GetTable(model.dest);
            model.desttbls = tbls.tables;
            model.desttblclmns = tbls.tblclmns;
        }

        public static void CompareTable(DtModel model)
        {
            var constr = model.src;
        }
    }
}

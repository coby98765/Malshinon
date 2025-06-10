using Google.Protobuf.Compiler;
using Google.Protobuf.WellKnownTypes;
using Malshinon.DB;
using Malshinon.models;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace Malshinon.models
    {
    internal class IntelReportDAL
        {
        private MySqlData _sqlData;
        public IntelReportDAL(MySqlData connData)
            {
            _sqlData = connData;
            }

        //format report into IntelReport Model
        private IntelReport ReportFormatter(MySqlDataReader data)
            {
            int id = data.GetInt32("id");
            int reporter_id = data.GetInt32("reporter_id");
            int target_id = data.GetInt32("target_id");
            string text = data.GetString("text");
            DateTime timestamp = data.GetDateTime("timestamp");

            IntelReport newReport = new IntelReport(reporter_id, target_id, text, timestamp, id);
            return newReport;
            }
        //CRUD Methods
        //Create
        public IntelReport CreateReport(IntelReport report)
            {
            string query = @"INSERT INTO `intelreports`
                           (id,reporter_id,target_id,text,timestamp)
                            VALUES(@id,@reporter_id,@target_id,@text)";
            MySqlCommand cmd = new MySqlCommand(query, _sqlData.GetConnection());
            MySqlDataReader reader = null;
            try
                {
                cmd.Parameters.AddWithValue("@SecretCode", report.ID);
                cmd.Parameters.AddWithValue("@reporter_id", report.ReporterID);
                cmd.Parameters.AddWithValue("@target_id", report.TargetID);
                cmd.Parameters.AddWithValue("@text", report.Text);

                reader = cmd.ExecuteReader();
                report = ReportFormatter(reader); 
                Console.WriteLine("Report Added.");
                }
            catch (Exception ex)
                {
                Console.WriteLine(ex.Message);
                return null;
                }
            finally
                {
                _sqlData.CloseConnection();
                }
            return report;
            }

        //Update
        public IntelReport UpdateReport(IntelReport report)
            {
            string query = $"UPDATE `intelreports` SET text = '{report.Text}' WHERE id = {report.ID};";
            MySqlCommand cmd = new MySqlCommand(query, _sqlData.GetConnection());
            MySqlDataReader reader = null;
            try
                {
                reader = cmd.ExecuteReader();
                report = ReportFormatter(reader);
                Console.WriteLine("Report Updated.");
                }
            catch (Exception ex)
                {
                Console.WriteLine(ex.Message);
                return null;
                }
            finally
                {
                _sqlData.CloseConnection();
                }
            return report;
            }

        //Read
        //Universal IntelReport Getter
        private List<IntelReport> GetReportsQuery(string queryParam)
            {
            List<IntelReport> reports = new List<IntelReport>();
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;
            string query = $"SELECT * FROM `intelreports` {queryParam};";
            try
                {
                MySqlConnection connection = _sqlData.GetConnection();
                cmd = new MySqlCommand(query, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    {
                    reports.Add(ReportFormatter(reader));
                    }
                }
            catch (Exception ex)
                {
                Console.WriteLine($"Error while fetching Report: {ex.Message}");
                }
            finally
                {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                _sqlData.CloseConnection();
                }
            return reports;
            }
        //Get Report By ID
        public IntelReport GetReportById(int id)
            {
            string query = $" WHERE intelreports.id = {id} LIMIT 1";
            List<IntelReport> reports = GetReportsQuery(query);
            if (reports.Count > 0)
                {
                return reports[0];
                }
            else
                {
                Console.WriteLine("Report not found.");
                return null;
                }
            }

        //Get Report By Reporter ID
        public List<IntelReport> GetReportsByReporter(int reprter)
            {
            string query = $"WHERE intelreports.reporter_id = '{reprter}'";
            List<IntelReport> reports = GetReportsQuery(query);
            if (reports.Count > 0)
                {
                return reports;
                }
            else
                {
                Console.WriteLine("Reports not found.");
                return null;
                }
            }

        //Get Report By Target ID
        public List<IntelReport> GetReportsByTarget(int target)
            {
            string query = $"WHERE intelreports.target_id = '{target}'";
            List<IntelReport> reports = GetReportsQuery(query);
            if (reports.Count > 0)
                {
                return reports;
                }
            else
                {
                Console.WriteLine("Reports not found.");
                return null;
                }
            }

        //Get All Reports
        public List<IntelReport> GetReportsList()
            {
            return GetReportsQuery("");
            }

        //Get Reports That contain x
        public List<IntelReport> GetReportsContaining(string search)
            {
            string query = $"WHERE intelreports.text LIKE '%{search}%'";
            List<IntelReport> reports = GetReportsQuery(query);
            if (reports.Count > 0)
                {
                return reports;
                }
            else
                {
                Console.WriteLine("Reports not found.");
                return null;
                }
            }

        //Delete
        public IntelReport DeleteReportById(int id)
            {
            string query = $"DELETE FROM intelreports WHERE intelreports.id = {id}";
            MySqlDataReader reader = null;
            IntelReport report;

            try
                {
                MySqlCommand cmd = new MySqlCommand(query, _sqlData.GetConnection());
                reader = cmd.ExecuteReader();
                report = ReportFormatter(reader);
                Console.WriteLine($"Report {id} was Deleted.");
                }
            catch (Exception ex)
                {
                Console.WriteLine(ex.Message);
                return null;
                }
            finally
                {
                _sqlData.CloseConnection();
                }
            return report;
            }
        }
    }

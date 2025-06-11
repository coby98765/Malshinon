using Malshinon.DB;
using Malshinon.models;


namespace Malshinon
    {
    class Program
        {
        static void Main()
            {
            //DB Connection
            MySqlData sqlData = new MySqlData();
            sqlData.Setup();

            //Person
            PersonDAL personDAL = new PersonDAL(sqlData);
            PersonTest personTest = new PersonTest(personDAL);

            // IntelReport
            IntelReportDAL reportDAL = new IntelReportDAL(sqlData);
            IntelReportTest intelReportTests = new IntelReportTest(reportDAL);

            //Controllers
            Controllers controllers = new Controllers(personDAL, reportDAL);

            }
        }
    }
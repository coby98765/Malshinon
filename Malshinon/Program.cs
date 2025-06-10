using Malshinon.DB;
using Malshinon.models;


namespace Malshinon
    {
    class Program
        {
        static void Main()
            {
            MySqlData sqlData = new MySqlData();
            sqlData.Setup();
            PersonDAL personDAL = new PersonDAL(sqlData);
            IntelReportDAL reportDAL = new IntelReportDAL(sqlData);

            }
        }
    }
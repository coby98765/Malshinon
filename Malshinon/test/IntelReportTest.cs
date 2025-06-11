using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.models
    {
    internal class IntelReportTest
        {
        public IntelReportTest(IntelReportDAL Report)
            {
            IntelReportDAL reportDAL = Report;
            //testing IntelReportDAL CRUD
            //Create
            IntelReport report = new IntelReport(3, 8, "Permission is hereby granted, free of charge");
            reportDAL.CreateReport(report);
            //read
            List<IntelReport> reports = reportDAL.GetReportsList();
            reports[0].Printer();
            //update
            reports[0].EditText("The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.");
            reports[0].Printer();
            reportDAL.UpdateReport(reports[0]);
            reportDAL.GetReportById(reports[0].ID).Printer();
            //delete
            reportDAL.DeleteReportById(reports[1]);
            }
        }
    }

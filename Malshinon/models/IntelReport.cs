using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Malshinon.models;


namespace Malshinon.models
    {
    internal class IntelReport
        {
        public int ID { get; private set; }
        public int ReporterID { get; private set; }
        public int TargetID { get; private set; }
        public string Text { get; private set; }
        public DateTime TimeStamp { get; private set; }


        IntelReport(int RepId, int TarID, string msg, DateTime TStamp, int id = 0)
            {
            ID = id;
            ReporterID = RepId;
            TargetID = TarID;
            Text = msg;
            TimeStamp = TStamp;
            }
        public void Printer()
            {
            Console.WriteLine($"({ID}){TimeStamp},\n" +
                $"Reporter: {ReporterID},\n" +
                $"Target: {TargetID},\n" +
                $"Message: {Text}.\n");
            }
        }
    }

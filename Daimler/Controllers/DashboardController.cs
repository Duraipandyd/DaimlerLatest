using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Daimler.Models;
using Microsoft.AspNetCore.Mvc;

namespace Daimler.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DaimlerContext _context;

        public DashboardController(DaimlerContext context)
        {
            _context = context;
        }
        public DashboardCounts GetDashboardCounts()
        {
            var dashboardCounts = new DashboardCounts();
            try
            {
                
                var dutyPaymentRequestHeaderController = new DutyPaymentRequestHeaderController(_context);
                dashboardCounts.DutyPaymentRequest = dutyPaymentRequestHeaderController.GetDutyPaymentRequestHeader().Count();
                 
                 if (dutyPaymentRequestHeaderController.GetISCRecords().ISClist!=null)
                {
                    dashboardCounts.ISC = dutyPaymentRequestHeaderController.GetISCRecords().ISClist.Count();
                }
                 else
                {
                    dashboardCounts.ISC = 0;
                }

                if (dutyPaymentRequestHeaderController.GetIDTRecords().Idtlist != null)
                {
                    dashboardCounts.IDT = dutyPaymentRequestHeaderController.GetIDTRecords().Idtlist.Count();
                }
                else
                {
                    dashboardCounts.IDT = 0;
                }

                var archiveController = new ArchiveRecordsController(_context);
                dashboardCounts.IDTComplete = GetArchiveRecords().Count();

                dashboardCounts.DutyPaymentRequestInProgress = dashboardCounts.IDT + dashboardCounts.ISC;                
                dashboardCounts.DutyPaymentRequestComplete = dashboardCounts.IDTComplete;

                dashboardCounts.ISCComplete = dashboardCounts.IDT+ dashboardCounts.IDTComplete;
                dashboardCounts.ISCInProgress = dashboardCounts.ISC;
                dashboardCounts.ISC = dashboardCounts.ISCComplete + dashboardCounts.ISCInProgress;
                
                dashboardCounts.IDTInprogress = dashboardCounts.IDT;
                dashboardCounts.IDT = dashboardCounts.IDTInprogress + dashboardCounts.IDTComplete;

                return dashboardCounts;

            }
            catch (Exception )
            {
                return dashboardCounts;
               
            }
        }

        public class DashboardCounts
        {
            public int DutyPaymentRequest { get; set; }
            public int DutyPaymentRequestInProgress { get; set; }
            public int DutyPaymentRequestComplete { get; set; }
            public int ISC { get; set; }
            public int ISCInProgress { get; set; }
            public int ISCComplete { get; set; }
            public int IDT { get; set; }
            public int IDTInprogress { get; set; }
            public int IDTComplete { get; set; }
        }

        public List<DashboardCounts> GetArchiveRecords()
        {


            var ArchiveRecordDtos = new List<DashboardCounts>();

            var DutyPaymentRequestHeaderController = new DutyPaymentRequestHeaderController(_context);
            var ISCRecords = DutyPaymentRequestHeaderController.GetISCRecords();
            var IDtRecords = DutyPaymentRequestHeaderController.GetIDTRecords();

            var IDtRecordsIds = ISCRecords.ISClist.Select(a => a.Id).ToList();
            var ISCRecordsIds = IDtRecords.Idtlist.Select(a => a.Id).ToList();

            IDtRecordsIds.AddRange(ISCRecordsIds);

            ArchiveRecordDtos = (from DutyPaymentRequestHeader in _context.DutyPaymentRequestHeaders
                                 join DutyPaymentRequestDetail in _context.DutyPaymentRequestDetail
                        on DutyPaymentRequestHeader.Id equals DutyPaymentRequestDetail.HeaderId
                                 join CHAISCDetail in _context.Chaiscdetails
                                 on DutyPaymentRequestHeader.Id equals CHAISCDetail.Boeid

                                 join CHADTDetail in _context.Chadtdetails
                        on DutyPaymentRequestHeader.Id equals CHADTDetail.Boeid
                                 where !IDtRecordsIds.Contains(DutyPaymentRequestHeader.Id)

                                 select new DashboardCounts
                                 {
                                     IDTComplete = DutyPaymentRequestHeader.Id
                                 }
                                ).ToList();


             
            
            return ArchiveRecordDtos;


        }
    }
}
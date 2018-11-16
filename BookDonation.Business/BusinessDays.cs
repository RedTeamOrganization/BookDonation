using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookDonation.Business

{
    public class BusinessDays
    {
        public static DateTime GetDueDate(DateTime receivedDatedTime, double workdays, DateTime PuDdate)
        {
            DateTime dueDate = receivedDatedTime;
            if (dueDate.DayOfWeek == DayOfWeek.Sunday)
                dueDate = dueDate.AddDays(1);
            else if (dueDate.DayOfWeek == DayOfWeek.Saturday)
                dueDate = dueDate.AddDays(2);
            double totalDays = workdays + (2 * ((workdays + (int)dueDate.DayOfWeek - 1) / 5));
            workdays = 5;
            PuDdate = dueDate.AddDays(totalDays);
            return PuDdate;
            //return dueDate.AddDays(totalDays);
        }
    }
}

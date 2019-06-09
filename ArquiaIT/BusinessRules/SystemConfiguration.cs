using ArquiaIT.Models.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArquiaIT.Models;

namespace ArquiaIT.BusinessRules
{
    public static class BusinessConfiguration
    {
        private static ArquiaEntities businessDB = new ArquiaEntities();
        private static SystemConfiguration sysConf = businessDB.SystemConfiguration.OrderByDescending(x => x.Id).FirstOrDefault();

        public static void UpdateChangeRate(decimal changeRate)
        {
            if (sysConf != null && sysConf.LastChangeRate != changeRate )
            {
                sysConf.LastChangeRate = changeRate;

                businessDB.SaveChanges();
            }
        }

        public static decimal getLastChangeRate()
        {
            return sysConf.LastChangeRate;
        }
    }
}
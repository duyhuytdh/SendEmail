using SendMail.Models;
using SendMail.ServiceMail;
using SendMail.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Quartz;
using Quartz.Impl;
using SendMail;

namespace SendMail.Util
{
    public static class mSchedule
    {
        public static IScheduler scheduler;
        public static List<JobKey> lstJobkey;
    }
}
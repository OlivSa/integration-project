using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApplication.Services.Quartz
{
    public class Scheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
            IJobDetail job = JobBuilder.Create<ScheduledJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()

                .WithIdentity("trigger", "group")
                .WithSchedule(CronScheduleBuilder
                .WeeklyOnDayAndHourAndMinute(DayOfWeek.Saturday, 7, 00))
                .ForJob(job)
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}
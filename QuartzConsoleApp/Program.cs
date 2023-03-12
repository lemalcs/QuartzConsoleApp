using Quartz;
using Quartz.Impl;
using QuartzConsoleApp;

StdSchedulerFactory factory = new StdSchedulerFactory();
IScheduler scheduler=await factory.GetScheduler();

await scheduler.Start();

// Define a job of type: HelloJob
IJobDetail jobDetail =JobBuilder.Create<HelloJob>()
    .WithIdentity("job1", "group1")
    .Build();

// This trigger makes job to run now and repeat every 5 seconds
// You can configure multiple triggers for a the same job
ITrigger trigger=TriggerBuilder.Create()
    .WithIdentity("trigger1","group1")
    .StartNow()
    .WithSimpleSchedule(x=>x
        .WithIntervalInSeconds(5)
        .RepeatForever())
    .Build();

// Schedule job for execution
await scheduler.ScheduleJob(jobDetail, trigger);

await Task.Delay(10000);

// Any running job will be killed after scheduler shutdown
await scheduler.Shutdown();


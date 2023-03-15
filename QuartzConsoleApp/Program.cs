using Quartz;
using Quartz.Impl;
using QuartzConsoleApp;

StdSchedulerFactory factory = new StdSchedulerFactory();
IScheduler scheduler=await factory.GetScheduler();


await scheduler.Start();

// Define a job of type: HelloJob
IJobDetail jobDetail =JobBuilder.Create<HelloJob>()
    // with persistent storage, the next line creates the job the first time it's
    // executed, otherwise a Quartz.JobPersistenceException exception is raised
    // because the job already exists, as a workaround you can delete the job
    // before creating a new one
    //.WithIdentity("job1", "group1")
    .UsingJobData("job1","group1") // this will create a new job when using persistent storage
    .Build();

// This trigger makes job to run now and repeat every 5 seconds
// You can configure multiple triggers for a the same job
ITrigger trigger=TriggerBuilder.Create()
    // This creates a trigger, if using persistent storage is used, a Quartz.ObjectAlreadyExistsException
    // exception is raised if trigger already exists
    //.WithIdentity("trigger1","group1") 
    .UsingJobData("trigger1","group1") // this creates a new trigger in persistent storage
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


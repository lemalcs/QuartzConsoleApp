using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace QuartzConsoleApp
{
    internal class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("This job is used to make greetings!");
        }
    }
}

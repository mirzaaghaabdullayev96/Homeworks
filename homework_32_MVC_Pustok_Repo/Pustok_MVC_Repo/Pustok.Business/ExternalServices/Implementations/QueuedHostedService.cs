using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pustok.Business.ExternalServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.ExternalServices.Implementations
{
    public class QueuedHostedService : BackgroundService
    {
        private readonly IBackgroundTaskQueue _taskQueue;

        //private readonly ILogger<QueuedHostedService> _logger;
        public QueuedHostedService(IBackgroundTaskQueue taskQueue /*ILogger<QueuedHostedService> logger*/)
        {
            _taskQueue = taskQueue;
            //_logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await _taskQueue.DequeueAsync(stoppingToken);
                await workItem(stoppingToken);
            }
        }
    }
}

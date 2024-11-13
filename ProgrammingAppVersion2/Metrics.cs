using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingApp
{
    public class Metrics
    {
        public int Total { get; set; }
        public int MaxNesting { get; set; }
        public int RepeatCount { get; set; }

        public Metrics CalculateMetrics(List<Command> commandList)
        {
            Metrics metrics = new Metrics();
            CalculateMetric(commandList, 0, metrics);
            return metrics;
        }

        private void CalculateMetric(List<Command> commands, int currentNesting, Metrics metrics)
        {
            foreach (var command in commands)
            {
                metrics.Total++;
                if (command is Repeat repeatCommand)
                {
                    metrics.RepeatCount++;
                    metrics.MaxNesting = Math.Max(metrics.MaxNesting, currentNesting);
                    CalculateMetric(repeatCommand.CommandList, currentNesting + 1, metrics);
                }
                else
                {
                    metrics.MaxNesting = Math.Max(metrics.MaxNesting, currentNesting);
                }
            }
        }
    }

}


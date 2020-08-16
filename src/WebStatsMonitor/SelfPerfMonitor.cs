using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Timers;
using System.Web;

namespace WebStatsMonitor
{
    public class SelfPerfMonitor
    {
        //https://blog.darkthread.net/blog/get-task-manager-list-with-csharp/
        static PerformanceCounter cpu;
        static PerformanceCounter mem;
        public static string GetInstanceNameForProcess(Process process)
        {
            try
            {
                string processName = Path.GetFileNameWithoutExtension(process.ProcessName);

                PerformanceCounterCategory cat = new PerformanceCounterCategory("Process");
                string[] instances = cat.GetInstanceNames()
                    .Where(inst => inst.StartsWith(processName))
                    .ToArray();

                foreach (string instance in instances)
                {
                    using (PerformanceCounter cnt = new PerformanceCounter("Process",
                        "ID Process", instance, true))
                    {
                        int val = (int)cnt.RawValue;
                        if (val == process.Id)
                        {
                            return instance;
                        }
                    }
                }
            }
            catch
            {
                //ignore
            }
            return null;
        }

        static SelfPerfMonitor()
        {
            var instanceName = GetInstanceNameForProcess(Process.GetCurrentProcess());
            cpu = new PerformanceCounter("Process", "% Processor Time", instanceName);
            mem = new PerformanceCounter("Process", "Working Set - Private", instanceName);
        }


        public static string GetCpuUsage() => $"{(cpu.NextValue() / Environment.ProcessorCount):n0}%";
        public static string GetMemUsage() => $"{mem.NextValue() / 1024:n0} K";

    }
}
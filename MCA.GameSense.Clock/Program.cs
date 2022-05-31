using MCA.GameSense.Models;
using System;
using System.Net.Http;
using System.Threading;
using System.Diagnostics;
using Microsoft.VisualBasic.Devices;
using System.Collections.Generic;
using System.Linq;

namespace MCA.GameSense.Clock
{
    class Program
    {
        private const string GAME_ID = "CLOCK";
        private const string EVENT_ID = "TICK";

        private static float TotalPhysicalMemoryGo = 16;// new ComputerInfo().TotalPhysicalMemory / 1024f / 1024f / 1024f;
        private static float TotalGpuMemoryGo = 8; // TODO: programatically find this value

        private static GameSenseClient GameSense { get; set; }
        private static PerformanceCounter CpuCounter { get; set; } = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        private static PerformanceCounter RamCounter { get; set; } = new PerformanceCounter("Memory", "Available MBytes");
        private static List<PerformanceCounter> GpuCounters { get; set; }
        private static List<PerformanceCounter> VramCounters { get; set; }

        static void Main(string[] args)
        {
            try
            {
                var coreProp = CorePropLoader.Load();
                GameSense = new GameSenseClient(coreProp.Address, GAME_ID);

                var result = GameSense.RegisterGame("Clock", "Michaël Carpentier");

                GameSense.BindEvent(EVENT_ID, handlers: new[] {
                    new Handler() {
                        Data = new ScreenFrameData[] {
                            new MultiLineFrameData()
                            {
                                //IconId = EventIcon.CPU,
                                LengthMs = 1500,
                                Lines = new []
                                {
                                    new LineData() { Bold = true},
                                    new LineData() { ContextFrameKey = "line1" },
                                    new LineData() { ContextFrameKey = "line2" }
                                }
                            }
                        }
                    }
                }); ;

                // Retrieve all counters from GPU Engine category
                var category = new PerformanceCounterCategory("GPU Engine");
                GpuCounters = category.GetInstanceNames()
                    .Where(n => n.EndsWith("_3D"))
                    .SelectMany(i => category.GetCounters(i).Where(c => c.CounterName == "Utilization Percentage"))
                    .ToList();

                // Retrieve all counters from GPU Adapter Memory category
                category = new PerformanceCounterCategory("GPU Adapter Memory");
                VramCounters = category.GetInstanceNames().SelectMany(i =>
                    category.GetCounters(i).Where(c => c.CounterName == "Dedicated Usage")
                ).ToList();

                // Start monitoring
                using (Timer timer = new Timer(UpdateScreen, null, 0, 1000))
                {
                    Thread.Sleep(Timeout.Infinite); // TODO: find a better way !
                }

                // Clean app information from SSE
                GameSense.RemoveEvent(EVENT_ID);
                GameSense.RemoveGame();
            }
            catch (HttpRequestException re)
            {
                Console.WriteLine(re.Message);
            }
            finally
            {
                GameSense.Dispose();
                CpuCounter.Dispose();
                RamCounter.Dispose();
                GpuCounters.ForEach(c => c.Dispose());
                VramCounters.ForEach(c => c.Dispose());
            }
        }

        static private void UpdateScreen(object state)
        {
            // Compute stats
            float cpuUsage = CpuCounter.NextValue();
            float gpuUsage = GpuCounters.Sum(c => c.NextValue());
            float usedRam = (TotalPhysicalMemoryGo * 1024 - RamCounter.NextValue()) / 1024;
            float usedVram = VramCounters.Sum(c => c.NextValue()) / 1024 / 1024 / 1024;

            // Send data to SSE
            GameSense.SendEvent(EVENT_ID, DateTime.Now.ToLongTimeString(), new
            {
                line1 = string.Format("CPU: {0:00.0}% - {1:0.0}/{2:0}", cpuUsage, usedRam, TotalPhysicalMemoryGo),
                line2 = string.Format("GPU: {0:00.0}% - {1:0.0}/{2:0}", gpuUsage, usedVram, TotalGpuMemoryGo)
            });
        }
    }
}

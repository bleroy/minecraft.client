using BenchmarkDotNet.Running;

namespace Decent.Minecraft.Benchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Blocks>();
        }
    }
}

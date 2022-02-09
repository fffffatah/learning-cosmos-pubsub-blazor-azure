using System;
using System.Diagnostics;
class Program{
    public static void Main(string[] args){
        var items = new List<string> { "armed" , "armed", "jazz", "jaws", "jam", "cram", "frame" };
        var stopwatch = new Stopwatch();

        var trie = new Trie();
        var hashset = new HashSet<string>();
        const string s = "cram";

        stopwatch.Start();
        trie.InsertRange(items);
        stopwatch.Stop();
        Console.WriteLine("Trie insertion in {0} ticks", stopwatch.ElapsedTicks);
        stopwatch.Reset();
            
        stopwatch.Start();
        for (int i = 0; i < items.Count; i++)
        hashset.Add(items[i]);
        stopwatch.Stop();
        Console.WriteLine("HashSet in {0} ticks", stopwatch.ElapsedTicks);
        stopwatch.Reset();

        Console.WriteLine("-------------------------------");

        stopwatch.Start();
        var prefix = trie.Prefix(s);
        var foundT = prefix.Depth == s.Length && prefix.FindChildNode('$') != null;
        stopwatch.Stop();
        Console.WriteLine("Trie search in {0} ticks found:{1}", stopwatch.ElapsedTicks, foundT);
        stopwatch.Reset();

        stopwatch.Start();

        var foundL = hashset.FirstOrDefault(str => str.StartsWith(s));
            
        stopwatch.Stop();
        Console.WriteLine("HashSet search in {0} ticks found:{1}", stopwatch.ElapsedTicks, foundL);

        trie.Delete("jazz");
        Console.Read();
    }
}
using Cosmos.Core;
using Cosmos.Core.Memory;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeBi2
{
    public class DeBi2
    {
        CosmosVFS fs;
        public DeBi2(CosmosVFS vfs)
        {
            fs = vfs;
        }
        public void ParseFile(string path)
        {
            string[] fx = File.ReadAllLines(path);
            foreach (string l in fx)
            {
                ParseLine(l);
            }
        }
        public void ParseLine(string line)
        {
            string[] l = line.Split(' ');
            switch (l[0])
            {
                case "ps":
                    if (l.Length > 1)
                    {
                        // объединяем все аргументы после команды
                        string text = string.Join(" ", l.Skip(1));

                        // убираем кавычки в начале и в конце строки, если есть
                        text = text.Trim('"');

                        Console.WriteLine(text);
                    }
                    break;
                case "cls":
                    Console.Clear();
                    break;
                case "cf":
                    if (l.Length == 2)
                    {
                        fs.CreateFile(l[1]);
                    }
                    else
                    {
                        fs.CreateFile(l[1]);
                        var ft = fs.GetFile(l[1]);
                        int s = Int32.Parse(l[2]);
                        ft.SetSize(s);
                    }
                        break;
                case "cd":
                    fs.CreateDirectory(l[1]);
                    break;
                case "wait":
                    Thread.Sleep(Int32.Parse(l[1]));
                    break;
                    
                case "df":
                    if (File.Exists(l[1]))
                        File.Delete(l[1]);
                    else
                        throw new FileNotFoundException();
                    break;
                case "dd":
                    if (Directory.Exists(l[1]))
                        Directory.Delete(l[1], true);
                    else
                        throw new FileNotFoundException();
                    break;
                case "syschk":
                    try
                    {
                        GCImplementation.Init();
                        Error.EchoDeb("Intializating GC", true);
                    }
                    catch
                    {
                        Error.EchoDeb("Initializating GC", false);
                    }
                    try
                    {
                        CPU.InitSSE();
                        Error.EchoDeb("Intializating SSE", true);
                    }
                    catch
                    {
                        Error.EchoDeb("Initializating SSE", false);
                    }
                    try
                    {
                        int cpuid = CPU.CanReadCPUID();
                        if (cpuid == 0)
                        {
                            Error.EchoDeb($"Collecting CPU Id: {cpuid}", true);
                        }
                        else
                        {
                            Error.EchoDeb("Collecting CPU Id", false);
                        }
                    }
                    catch
                    {
                        Error.EchoDeb("Collecting CPU Id", false);
                    }
                    try
                    {
                        CPU.InitFloat();
                        Error.EchoDeb("Intializating Float", true);
                    }
                    catch
                    {
                        Error.EchoDeb("Initializating Float", false);
                    }
                    try
                    {
                        ACPI.Enable();
                        Kernel.aspi = true;
                        Error.EchoDeb("Enabling ACPI", true);
                    }
                    catch
                    {
                        Error.EchoDeb("Enabling ACPI", false);
                    }
                    try
                    {
                        fs = new CosmosVFS();
                        Error.EchoDeb("Creating File System", true);
                    }
                    catch
                    {
                        Error.EchoDeb("Creating File System", false);
                    }
                    try
                    {
                        VFSManager.RegisterVFS(fs, true, true);
                        Error.EchoDeb("Registring File System", true);
                    }
                    catch
                    {
                        Error.EchoDeb("Registring File System", false);
                    }
                    try
                    {
                        Kernel.d = new DeBi2(fs);
                        Error.EchoDeb("Intializating DB2", true);
                    }
                    catch
                    {
                        Error.EchoDeb("Intializating DB2", false);
                    }
                    try
                    {
                        Kernel.fs.CreateFile(@"0:\file.txt");
                        var f = Kernel.fs.GetFile(@"0:\file.txt");
                        f.SetName("name2");
                        f.SetName("file.txt");
                        f.SetSize(1024);
                        File.Delete(@"0:\file.txt");
                        Error.EchoDeb("Checking FS", true);
                    }
                    catch
                    {
                        Error.EchoDeb("Checking FS", false);
                    }
                    break;
                case "fschk":
                    try
                    {
                        Kernel.fs.CreateFile(@"0:\file.txt");
                        var f = Kernel.fs.GetFile(@"0:\file.txt");
                        f.SetName("name2");
                        f.SetName("file.txt");
                        f.SetSize(1024);
                        File.Delete(@"0:\file.txt");
                        Error.EchoDeb("Checking FS", true);
                    }
                    catch
                    {
                        Error.EchoDeb("Checking FS", false);
                    }
                    break;
                case "erase":
                    foreach (var f in fs.GetDirectoryListing(l[1]))
                    {
                        if (f.mEntryType == Cosmos.System.FileSystem.Listing.DirectoryEntryTypeEnum.File)
                        {
                            File.Delete(f.mFullPath);
                        }
                        if (f.mEntryType == Cosmos.System.FileSystem.Listing.DirectoryEntryTypeEnum.Directory)
                        {
                            Directory.Delete(f.mFullPath, true);
                        }
                        Heap.Collect();
                    }
                    break;
                case "rins":
                    Console.WriteLine("Reinstalling. . .");
                    try
                    {
                        ParseLine(@"erase 0:\");
                        Directory.Delete(@"0:\system", true);
                        Console.WriteLine("system");
                        Directory.Delete(@"0:\recovery", true);
                        Console.WriteLine("rcv");
                        Directory.Delete(@"0:\user", true);
                        Console.WriteLine("usr");
                        Directory.Delete(@"0:\userdata", true);
                        Console.WriteLine("usrdata");
                        Directory.Delete(@"0:\commands", true);
                        Console.WriteLine("cmd");
                    }
                    catch
                    {
                        Console.WriteLine("Corrupted system. . .");
                    }
                    Directory.Delete(@"0:\db2", true);
                    Console.WriteLine("rebooting. . .");
                    Cosmos.System.Power.Reboot();
                    break;
                case "snf":
                    var ff = fs.GetFile(l[1]);
                    ff.SetName(l[2]);
                    break;
                case "snd":
                    var fd = fs.GetDirectory(l[1]);
                    fd.SetName(l[2]);
                    break;
                case "p":
                    Console.ReadKey();
                    break;
                case "shd":
                    Cosmos.System.Power.Shutdown();
                    break;
                case "rbt":
                    Cosmos.System.Power.Reboot();
                    break;
                case "ls":
                    if (l.Length < 2 || string.IsNullOrEmpty(l[1]))
                    {
                        Console.WriteLine("Usage: ls <path>");
                        break;
                    }

                    string path = l[1] == "/" ? @"0:\" : l[1];

                    if (!Directory.Exists(path))
                    {
                        throw new FileNotFoundException($"Directory not found: {path}");
                    }

                    foreach (var f in fs.GetDirectoryListing(path))
                    {
                        string displayPath = f.mFullPath.Replace(@"0:\", "/").Replace('\\', '/');
                        if (f.mEntryType == Cosmos.System.FileSystem.Listing.DirectoryEntryTypeEnum.File)
                        {
                            Console.WriteLine($"{displayPath} {f.mSize}");
                        }
                        else if (f.mEntryType == Cosmos.System.FileSystem.Listing.DirectoryEntryTypeEnum.Directory)
                        {
                            Console.WriteLine($"dir {displayPath} {f.mSize}");
                        }
                    }
                    break;
                case "format":
                    if (l[1] == @"0:\")
                    {
                        try
                        {
                            fs.Initialize(true);
                        }
                        catch
                        {
                            var d = fs.Disks[0];
                            d.FormatPartition(0, "FAT32", true);
                        }
                    }
                    else
                    {
                        throw new Exception("NoDiskFound");
                    }
                        break;
                default:
                    throw new Exception("Unknown syntax");
                    break;

            }
        }
    }
}

using System;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using DriveInformation;

[assembly: SupportedOSPlatform("windows")]

// Install-Package System.Management
internal class Program
{
    static void Main(string[] args)
    {
        List<Win32_DiskDrive> physicalDisks = GetPhysicalDiskList();

        foreach(Win32_LogicalDisk drive in GetLogicalDiskList())
        {
            Console.WriteLine($"==============================================================================================");
            Console.WriteLine($"<Logical Drive>");
            Console.WriteLine($"---------------");
            Console.WriteLine($"Name: {drive.VolumeName} ({drive.Name})");
            Console.WriteLine($"FileSystem: {drive.FileSystem}");
            Console.WriteLine($"DosDevicePath: {GetDosDevicePathFromDriveLetter(drive.Name)}");
            Console.WriteLine($"VolumeSerialNumber: {drive.VolumeSerialNumber}");
            Console.WriteLine($"Description: {drive.Description}");

            drive.PhysicalDisk = physicalDisks.Find(disk => disk.Partitions.Find(partition => partition.VolumeSerialNumber == drive.VolumeSerialNumber) != null);
            if(drive.PhysicalDisk != null)
            {
                Console.WriteLine();
                Console.WriteLine($"<Physical Disk>");
                Console.WriteLine($"---------------");
                Console.WriteLine($"Description: {drive.PhysicalDisk.Description}");
                Console.WriteLine($"DeviceID: {drive.PhysicalDisk.DeviceID}");
                Console.WriteLine($"InterfaceType: {drive.PhysicalDisk.InterfaceType}");
                Console.WriteLine($"Model: {drive.PhysicalDisk.Model}");
                Console.WriteLine($"PNPDeviceID: {drive.PhysicalDisk.PNPDeviceID}");
                Console.WriteLine($"SerialNumber: {drive.PhysicalDisk.SerialNumber}");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"<Physical Disk>");
                Console.WriteLine($"---------------");
                Console.WriteLine($"None");
            }
 
            Console.WriteLine();
        }

        Console.WriteLine($"==============================================================================================");

        Console.WriteLine($"종료하려면 아무 키나 누르세요...");
        Console.ReadKey();
    }

    public static List<Win32_LogicalDisk> GetLogicalDiskList()
    {
        List<Win32_LogicalDisk> drives = new List<Win32_LogicalDisk>();

        try
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_LogicalDisk");

            foreach(ManagementObject queryObj in searcher.Get())
            {
                Win32_LogicalDisk item = new Win32_LogicalDisk();

                item.Description = queryObj.GetAsString("Description");
                item.FileSystem = queryObj.GetAsString("FileSystem");
                item.Name = queryObj.GetAsString("Name");
                item.VolumeName = queryObj.GetAsString("VolumeName");
                item.VolumeSerialNumber = queryObj.GetAsString("VolumeSerialNumber");

                drives.Add(item);
            }
        }
        catch(ManagementException)
        {
        }

        return drives;
    }

    public static List<Win32_DiskDrive> GetPhysicalDiskList()
    {
        List<Win32_DiskDrive> drives = new List<Win32_DiskDrive>();

        try
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");

            foreach(ManagementObject queryObj in searcher.Get())
            {
                Win32_DiskDrive item = new Win32_DiskDrive();

                item.Description = queryObj.GetAsString("Description");
                item.DeviceID = queryObj.GetAsString("DeviceID");
                item.Index = queryObj.GetAsUInt32("Index");
                item.InterfaceType = queryObj.GetAsString("InterfaceType");
                item.Model = queryObj.GetAsString("Model");
                item.PNPDeviceID = queryObj.GetAsString("PNPDeviceID");
                item.SerialNumber = queryObj.GetAsString("SerialNumber").Trim();

                ListPartitions(item);
                drives.Add(item);
            }
        }
        catch(ManagementException)
        {
        }

        return drives;
    }

    // https://stackoverflow.com/questions/48116174/wmi-association-of-logicaldisk-with-diskpartition
    private static void ListPartitions(Win32_DiskDrive drive)
    {
        string assocQuery = $"Associators of {{Win32_DiskDrive.DeviceID='{drive.DeviceID}'}} where AssocClass=Win32_DiskDriveToDiskPartition";
        var assocPart = new ManagementObjectSearcher(assocQuery);
        assocPart.Options.Timeout = System.Management.EnumerationOptions.InfiniteTimeout;

        foreach(ManagementObject moPart in assocPart.Get())
        {
            string logDiskQuery = $"Associators of {{Win32_DiskPartition.DeviceID='{moPart.Properties["DeviceID"].Value.ToString()}'}} where AssocClass=Win32_LogicalDiskToPartition";

            var logDisk = new ManagementObjectSearcher(logDiskQuery);
            logDisk.Options.Timeout = System.Management.EnumerationOptions.InfiniteTimeout;

            foreach(var logDiskEnu in logDisk.Get())
            {
                Win32_DiskDriveToDiskPartition partition = new Win32_DiskDriveToDiskPartition();
                partition.VolumeSerialNumber = logDiskEnu.GetAsString("VolumeSerialNumber");

                drive.Partitions.Add(partition);
            }
        }
    }

    // QueryDosDevice 함수의 P/Invoke 선언
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    static extern uint QueryDosDevice(string lpDeviceName, StringBuilder lpTargetPath, uint uddcchMaxTargetPath);

    public static string GetDosDevicePathFromDriveLetter(string driveLetter)
    {
        // 경로를 저장할 StringBuilder
        StringBuilder targetPath = new StringBuilder(256); // 충분한 버퍼 크기

        // QueryDosDevice 함수 호출
        uint result = QueryDosDevice(driveLetter, targetPath, (uint)targetPath.Capacity);

        if(result == 0)
        {
            // 오류 처리
            Console.WriteLine($"경로를 가져오는 데 실패했습니다. 오류 코드: {Marshal.GetLastWin32Error()}");
            return null;
        }

        return targetPath.ToString();
    }
}

public class Win32_LogicalDisk
{
    public string Description = "";
    public string FileSystem = "";
    public string Name = "";
    public string VolumeName = "";
    public string VolumeSerialNumber = "";

    public Win32_DiskDrive? PhysicalDisk;
}

public class Win32_DiskDrive
{
    public string Description = "";
    public string DeviceID = "";
    public uint Index;
    public string InterfaceType = "";
    public string Model = "";
    public string PNPDeviceID = "";
    public string SerialNumber = "";

    public List<Win32_DiskDriveToDiskPartition> Partitions = new List<Win32_DiskDriveToDiskPartition>();
}

public class Win32_DiskDriveToDiskPartition
{
    public string VolumeSerialNumber = "";
}

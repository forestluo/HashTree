using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;

class MMFExample
{
    static void Main()
    {
        // Define the file path and size
        string filePath = "LargeFile.txt";
        long fileSize = 1024L * 1024L * 8192L; // 8GB

        // Create or open the memory-mapped file
        using (var mmf = MemoryMappedFile.CreateFromFile(filePath, FileMode.OpenOrCreate, null, fileSize))
        {
            // Create a memory-mapped view accessor to read and write data
            using (var accessor = mmf.CreateViewAccessor())
            {
                // Write data to the memory-mapped file
                string dataToWrite = "Hello, Memory-Mapped Files!";
                byte[] dataBytes = Encoding.UTF8.GetBytes(dataToWrite);
                accessor.WriteArray(0, dataBytes, 0, dataBytes.Length);

                // Read data from the memory-mapped file
                byte[] readData = new byte[dataBytes.Length];
                accessor.ReadArray(0, readData, 0, readData.Length);

                string readDataString = Encoding.UTF8.GetString(readData);
                Console.WriteLine("Data read from memory-mapped file: " + readDataString);
            }
        }
    }
}

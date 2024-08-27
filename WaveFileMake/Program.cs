namespace WaveFileMake
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var file = @"d:\440Hz_44100Hz_16bit_05sec.wav";
            var testFileStream = File.Open(file, FileMode.Open);

            //File.Open(file, FileMode.Open);

            using (var breader = new BinaryReader(testFileStream))
            {
                var byteOffset = 4;
                var tmp = new byte[4];


                Console.WriteLine($"'{ConvertToString(breader.ReadBytes(byteOffset))}'");

                var x = BitConverter.ToInt32(breader.ReadBytes(byteOffset));
                Console.WriteLine($"FILE SIZE: {x} bytes = {(x-8)/1024} kB");
                Console.WriteLine($"'{ConvertToString(breader.ReadBytes(byteOffset))}'");
                Console.WriteLine($"FMT: {ConvertToString(breader.ReadBytes(byteOffset))}");
                Console.WriteLine($"SIZE OF FORMAT IN BYTES: \t{BitConverter.ToInt32(breader.ReadBytes(byteOffset))}");
                Console.WriteLine($"TYPE OF WAVE: \t{BitConverter.ToInt16(breader.ReadBytes(2))}");
                Console.WriteLine($"NUMBER OF CHANNELS: \t{BitConverter.ToInt16(breader.ReadBytes(2))}");
                Console.WriteLine($"SAMPLE RATE: \t{BitConverter.ToInt32(breader.ReadBytes(byteOffset))} Hz");
                Console.WriteLine($"BYTES PER SECOND: \t{BitConverter.ToInt32(breader.ReadBytes(byteOffset))} bytes/s");
                Console.WriteLine($"BYTES PER SAMPLE: \t{BitConverter.ToInt16(breader.ReadBytes(2))} bytes/sample");
                Console.WriteLine($"BITS PER SAMPLE: \t{BitConverter.ToInt16(breader.ReadBytes(2))} bit/sample\n");
                
                Console.WriteLine($"'{ConvertToString(breader.ReadBytes(byteOffset))}'");
                Console.WriteLine($"DATA SIZE: \t{BitConverter.ToInt32(breader.ReadBytes(byteOffset))} bytes\n");
                Console.WriteLine("One sample data below:");

                //reading data (fun...)
                for(var i=0; i<441000; i++)
                {
                    var data = breader.ReadBytes(2);
                    var convertedData = BitConverter.ToInt16(data);

                    //Console.WriteLine($"[{i}]\t{BitConverter.ToInt16(data)}");
                }

            };

            testFileStream.Close();
        }
        private static string ConvertToString(byte[] input)
        {
            var result = string.Empty;

            foreach (var c in input)
            {
                result += (char)c;
            }

            return result;
        }
    }
}
using System.IO;

namespace TestApi.File
{
    public class ReservationFile
    {
        private string filePath = "C:\\Users\\m.talebi\\Desktop\\Reservation.txt";
        private StreamReader streamReader;
        
        public ReservationFile()
        {
            streamReader = new StreamReader(filePath);
        }

        // Example method using the streamReader
        public string ReadFileContent()
        {
            string fileContent = streamReader.ReadToEnd();
            return fileContent;
                       
        }
        

    }


}

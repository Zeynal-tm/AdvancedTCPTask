

namespace AdvancedTCPTask
{
    public static class Tcp
    {
        const string IP_ADDRESS = "88.212.241.115";
        const int PORT = 2013;
        public static string Request(string input)
        {
            var tcpClient = new TcpClient(IP_ADDRESS, PORT);
            var stream = tcpClient.GetStream();

            var request = Encoding.ASCII.GetBytes(input);
            stream.Write(request, 0, request.Length);

            var streamReader = new StreamReader(stream);
            var response = streamReader.ReadToEnd();

            stream.Close();
            tcpClient.Close();
            return response;
        }
    }
}

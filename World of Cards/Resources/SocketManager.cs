using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace World_of_Cards.Resources
{
    public class SocketManager
    {
        private StreamSocket socket;
        public int client;
        public delegate void StringReceivedEvent(String scope, String message, int clientIndex);
        public delegate void ByteArrayReceivedEvent(String scope, Byte[] message, int clientIndex);
        public event StringReceivedEvent StringMessage;
        public event ByteArrayReceivedEvent ByteArrayMessage;
        private DataWriter dataWriter;
        private DataReader dataReader;
        public SocketManager(StreamSocket socket, int clientIndex)
        {
            this.socket = socket;
            dataWriter = new DataWriter(socket.OutputStream);
            dataReader = new DataReader(socket.InputStream);
            client = clientIndex;
        }

        public async Task receiveMessage()
        {
            await dataReader.LoadAsync(4);
            uint scopeLen = (uint)dataReader.ReadInt32();
            await dataReader.LoadAsync(scopeLen);
            string scope = dataReader.ReadString(scopeLen);

            await dataReader.LoadAsync(4);
            uint messageLen = (uint)dataReader.ReadInt32();
            if (messageLen > 0)
            {
                await dataReader.LoadAsync(messageLen);
                if (scope.StartsWith("string"))
                {
                    StringMessage(scope, dataReader.ReadString(messageLen), client);
                }
                else if (scope.StartsWith("bytearray"))
                {
                    Byte[] temp = new Byte[messageLen];
                    dataReader.ReadBytes(temp);
                    ByteArrayMessage(scope, temp, client);
                }
            }
            else
            {
                StringMessage(scope,"", client);
            }
            Debug.WriteLine(scope);
            await receiveMessage();
        }


        public async Task sendMessage(string scope, string message)
        {
            Debug.WriteLine(scope);
            Debug.WriteLine(message);
            dataWriter.WriteInt32(scope.Length);
            await dataWriter.StoreAsync();

            dataWriter.WriteString(scope);
            await dataWriter.StoreAsync();
            Debug.WriteLine("scope sent");

            dataWriter.WriteInt32(message.Length);
            await dataWriter.StoreAsync();

            dataWriter.WriteString(message);
            await dataWriter.StoreAsync();
            Debug.WriteLine("mess sent");
            //await dataWriter.FlushAsync();
            //dataWriter.Dispose();
            Debug.WriteLine("done");

        }

        public async Task sendMessage(string scope, byte[] p)
        {
            dataWriter.WriteInt32(scope.Length);
            await dataWriter.StoreAsync();

            dataWriter.WriteString(scope);
            await dataWriter.StoreAsync();

            dataWriter.WriteInt32(p.Length);
            await dataWriter.StoreAsync();

            dataWriter.WriteBytes(p);
            await dataWriter.StoreAsync();
        }
    }
}

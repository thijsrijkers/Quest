using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quest.Minecraft.World
{
    public class ConnectWorld : IConnection
    {
        private TcpClient socket; 
        private NetworkStream stream;
        private StreamReader streamReader;
        private bool disposed = false;
        private SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        public string Address { get; set; } = "localhost";
        public int Port { get; set; } = 4711;

        /// <summary>
        /// Setting up TCP client
        /// </summary>
        public ConnectWorld()
        {
            socket = new TcpClient(AddressFamily.InterNetwork);
            Console.WriteLine("Setting TCP client");
        }

        public ConnectWorld(string address = "localhost", int port = 4711) : this()
        {
            Address = address;
            Port = port;
        }

        /// <summary>
        /// Setting connection and set stream parameters
        /// </summary>
        public async Task OpenAsync()
        {
            try
            {
                await socket.ConnectAsync(Address, Port);
                stream = socket.GetStream();
                streamReader = new StreamReader(stream);
                Console.WriteLine("Socket, stream and streamreader are preparing");
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Open()
        {
            OpenAsync().Wait();
        }

        public void Close()
        {
            Dispose();
        }

        /// <summary>
        /// Retrief data to process
        /// </summary>
        public async Task SendAsync(string value, IEnumerable data)
        {
            var s = $"{value}({data})\n";
            Debug.WriteLineIf(!value.StartsWith("events."), $"Sending: {s}");
            await semaphore.WaitAsync();

            try
            {
                var buffer = Encoding.ASCII.GetBytes(s);
                Console.WriteLine("Waiting for sync");
                await stream.WriteAsync(buffer, 0, buffer.Length);
 
            }
            finally
            {
                semaphore.Release();
            }
        }

        public async Task SendAsync(string value, params object[] data)
        {
            await SendAsync(value, (IEnumerable)data);
        }

        public void Send(string value, IEnumerable data)
        {
            SendAsync(value, data).Wait();
        }

        public void Send(string value, params object[] data)
        {
            SendAsync(value, data).Wait();
        }

        public async Task<string> ReceiveAsync()
        {
            await semaphore.WaitAsync();
            try
            {
                var response = await streamReader.ReadLineAsync();
                Debug.WriteLineIf(!string.IsNullOrEmpty(response), $"Received: {response}");
                return response;
            }
            catch (ObjectDisposedException)
            {
                return null;
            }
            finally
            {
                semaphore.Release();
            }
        }

        public string Receive()
        {
            return ReceiveAsync().Result;
        }

        public async Task<string> SendAndReceiveAsync(string value, IEnumerable data)
        {
            var s = $"{value}({data})\n";
            Debug.WriteLineIf(!value.StartsWith("events."), $"Sending: {s}");
            await semaphore.WaitAsync();
            try
            {
                var buffer = Encoding.ASCII.GetBytes(s);
                await stream.WriteAsync(buffer, 0, buffer.Length);
                var response = await streamReader.ReadLineAsync();
                Debug.WriteLineIf(!string.IsNullOrEmpty(response), $"Received: {response}");
                return response;
            }
            catch (ObjectDisposedException)
            {
                return null;
            }
            finally
            {
                semaphore.Release();
            }
        }

        public async Task<string> SendAndReceiveAsync(string value, params object[] data)
        {
            return await SendAndReceiveAsync(value, (IEnumerable)data);
        }

        public string SendAndReceive(string value, IEnumerable data)
        {
            return SendAndReceiveAsync(value, data).Result;
        }

        public string SendAndReceive(string value, params object[] data)
        {
            return SendAndReceiveAsync(value, data).Result;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    streamReader.Dispose();
                    stream.Dispose();
                    socket.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
    
}

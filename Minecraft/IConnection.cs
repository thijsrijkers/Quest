using System;
using System.Collections;
using System.Threading.Tasks;

namespace Quest.Minecraft
{
    public interface IConnection : IDisposable
    {
        /// <summary>
        /// Get IP address of instance.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The port on which the Minecraft instance communicates.
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// Closes the connection.
        /// </summary>
        void Close();

        /// <summary>
        /// Opens the connection.
        /// </summary>
        void Open();

        Task OpenAsync();

        /// <summary>
        /// Receives a response from instance.
        /// </summary>
        string Receive();

        Task<string> ReceiveAsync();

        /// <summary>
        /// Sends a command
        /// </summary>
        void Send(string command, params object[] data);

        void Send(string command, IEnumerable data);

        Task SendAsync(string command, params object[] data);

        Task SendAsync(string command, IEnumerable data);

        /// <summary>
        /// Sends a command and then receives a response.
        /// </summary>
        string SendAndReceive(string command, params object[] data);

        string SendAndReceive(string command, IEnumerable data);

        Task<string> SendAndReceiveAsync(string command, params object[] data);

        Task<string> SendAndReceiveAsync(string command, IEnumerable data);
    } 
}

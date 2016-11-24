using System;
using System.Collections;
using System.Threading.Tasks;

namespace Decent.Minecraft.Client
{
    /// <summary>
    /// The contract for a connection to a Minecraft instance.
    /// This defines the low-level communication protocol with Minecraft instances.
    /// </summary>
    public interface IConnection : IDisposable
    {
        /// <summary>
        /// The IP address of the Minecraft instance.
        /// </summary>
        string Address { get; set; }
        
        /// <summary>
        /// The port on which the Minecraft instance communicates.
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// Closes the connection. Equivalent to calling `Dispose`.
        /// </summary>
        void Close();

        /// <summary>
        /// Opens the connection. This must be called prior to starting communication.
        /// </summary>
        void Open();

        /// <summary>
        /// Asynchronously opens the connection. This must be called prior to starting communication.
        /// </summary>
        Task OpenAsync();

        /// <summary>
        /// Receives a response from the Minecraft instance.
        /// </summary>
        /// <returns>The unparsed response of the Minecraft instance.</returns>
        string Receive();

        /// <summary>
        /// Asynchronously receives a response from the Minecraft instance.
        /// </summary>
        /// <returns>The unparsed response of the Minecraft instance.</returns>
        Task<string> ReceiveAsync();

        /// <summary>
        /// Sends a command with its parameters to the Minecraft instance.
        /// </summary>
        /// <param name="command">The name of the command.</param>
        /// <param name="data">The parameters of the command.</param>
        void Send(string command, params object[] data);

        /// <summary>
        /// Sends a command with its parameters to the Minecraft instance.
        /// </summary>
        /// <param name="command">The name of the command.</param>
        /// <param name="data">The parameters of the command.</param>
        void Send(string command, IEnumerable data);

        /// <summary>
        /// Asynchronously sends a command with its parameters to the Minecraft instance.
        /// </summary>
        /// <param name="command">The name of the command.</param>
        /// <param name="data">The parameters of the command.</param>
        Task SendAsync(string command, params object[] data);

        /// <summary>
        /// Asynchronously sends a command with its parameters to the Minecraft instance.
        /// </summary>
        /// <param name="command">The name of the command.</param>
        /// <param name="data">The parameters of the command.</param>
        Task SendAsync(string command, IEnumerable data);

        /// <summary>
        /// Sends a command with its parameters to the Minecraft instance, and then receives a response.
        /// </summary>
        /// <param name="command">The name of the command.</param>
        /// <param name="data">The parameters of the command.</param>
        /// <returns>The unparsed response from the Minecraft instance.</returns>
        string SendAndReceive(string command, params object[] data);

        /// <summary>
        /// Sends a command with its parameters to the Minecraft instance, and then receives a response.
        /// </summary>
        /// <param name="command">The name of the command.</param>
        /// <param name="data">The parameters of the command.</param>
        /// <returns>The unparsed response from the Minecraft instance.</returns>
        string SendAndReceive(string command, IEnumerable data);

        /// <summary>
        /// Asynchronously sends a command with its parameters to the Minecraft instance, and then receives a response.
        /// </summary>
        /// <param name="command">The name of the command.</param>
        /// <param name="data">The parameters of the command.</param>
        /// <returns>The unparsed response from the Minecraft instance.</returns>
        Task<string> SendAndReceiveAsync(string command, params object[] data);

        /// <summary>
        /// Asynchronously sends a command with its parameters to the Minecraft instance, and then receives a response.
        /// </summary>
        /// <param name="command">The name of the command.</param>
        /// <param name="data">The parameters of the command.</param>
        /// <returns>The unparsed response from the Minecraft instance.</returns>
        Task<string> SendAndReceiveAsync(string command, IEnumerable data);
    }
}
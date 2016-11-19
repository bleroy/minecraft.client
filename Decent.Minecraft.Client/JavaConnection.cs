using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Decent.Minecraft.Client
{
    /// <summary>
    /// A connection to a Minecraft game.
    /// </summary>
    public class JavaConnection : IConnection
    {
        private TcpClient _socket;
        private NetworkStream _stream;
        private StreamReader _streamReader;
        private bool _disposedValue = false; // To detect redundant calls
        public string Address { get; set; } = "localhost";
        public int Port { get; set; } = 4711;

        public JavaConnection()
        {
            _socket = new TcpClient(AddressFamily.InterNetwork);
        }

        public JavaConnection(string address = "localhost", int port = 4711) : this()
        {
            Address = address;
            Port = port;
        }

        public async Task OpenAsync()
        {
            try
            {
                await _socket.ConnectAsync(Address, Port);
                _stream = _socket.GetStream();
                _streamReader = new StreamReader(_stream);
            }
            catch (SocketException e)
            {
                throw new FailedToConnectToMinecraftEngine(e);
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

        public async Task SendAsync(string function, IEnumerable data)
        {
            var s = $"{function}({data.FlattenToString()})\n";
            Debug.WriteLine(s);
            var buffer = Encoding.ASCII.GetBytes(s);
            await _stream.WriteAsync(buffer, 0, buffer.Length);
        }

        public async Task SendAsync(string function, params object[] data)
        {
            await SendAsync(function, (IEnumerable)data);
        }

        public void Send(string function, IEnumerable data)
        {
            SendAsync(function, data).Wait();
        }

        public void Send(string function, params object[] data)
        {
            SendAsync(function, data).Wait();
        }

        public async Task<string> ReceiveAsync()
        {
            var response = await _streamReader.ReadLineAsync();
            Debug.WriteLine(">" + response);
            return response;
        }

        public string Receive()
        {
            return ReceiveAsync().Result;
        }

        public async Task<string> SendAndReceiveAsync(string function, IEnumerable data)
        {
            await SendAsync(function, data);
            return await ReceiveAsync();
        }

        public async Task<string> SendAndReceiveAsync(string function, params object[] data)
        {
            return await SendAndReceiveAsync(function, (IEnumerable)data);
        }

        public string SendAndReceive(string function, IEnumerable data)
        {
            return SendAndReceiveAsync(function, data).Result;
        }

        public string SendAndReceive(string function, params object[] data)
        {
            return SendAndReceiveAsync(function, data).Result;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _streamReader.Dispose();
                    _stream.Dispose();
                    _socket.Dispose();
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}

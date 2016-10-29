using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Decent.Minecraft.Client
{
    /// <summary>
    /// A connection to a Minecraft game.
    /// </summary>
    public class Connection : IDisposable
    {
        private TcpClient _socket;
        private NetworkStream _stream;
        private StreamReader _streamReader;
        private string _address;
        private int _port;
        private bool _disposedValue = false; // To detect redundant calls

        public Connection(string address = "localhost", int port = 4711)
        {
            _socket = new TcpClient(AddressFamily.InterNetwork);
            _address = address;
            _port = port;
        }

        public async Task OpenAsync()
        {
            await _socket.ConnectAsync(_address, _port);
            _stream = _socket.GetStream();
            _streamReader = new StreamReader(_stream);
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
            return await _streamReader.ReadLineAsync();
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

using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft.Client
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

        public async Task OpenAsync() {
            await _socket.ConnectAsync(_address, _port);
            _stream = _socket.GetStream();
            _streamReader = new StreamReader(_stream);
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

        public async Task<string> ReceiveAsync()
        {
            return await _streamReader.ReadLineAsync();
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

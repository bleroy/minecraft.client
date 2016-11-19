using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Decent.Minecraft.Client.Test.Fakes
{
    public class FakeConnection : IConnection
    {
        public string LastPosition { get; set; } = "0,0,0";

        public void Dispose() { }
        public void Close() { }
        public void Open() { }
        public Task OpenAsync() { return Task.CompletedTask; }

        public string Address { get; set; }
        public int Port { get; set; }

        public Task<string> SendAndReceiveAsync(string function, params object[] data)
        {
            return Task.Run(() =>
            {
                var args = data.FlattenToString();
                Debug.WriteLine($"Sending and receiving: {function}({args})");

                if (function.EndsWith(".getPos"))
                {
                    return LastPosition;
                }

                return string.Empty;
            });
        }

        public Task SendAsync(string function, params object[] data)
        {
            return Task.Run(() =>
            {
                var args = data.FlattenToString();
                Debug.WriteLine($"Sending: {function}({args})");
                if (function.EndsWith(".setPos"))
                {
                    LastPosition = args;
                }
            });
        }


        public string Receive()
        {
            throw new System.NotImplementedException();
        }

        public Task<string> ReceiveAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Send(string function, params object[] data)
        {
            throw new System.NotImplementedException();
        }

        public void Send(string function, IEnumerable data)
        {
            throw new System.NotImplementedException();
        }

        public string SendAndReceive(string function, params object[] data)
        {
            throw new System.NotImplementedException();
        }

        public string SendAndReceive(string function, IEnumerable data)
        {
            throw new System.NotImplementedException();
        }

       
        public Task<string> SendAndReceiveAsync(string function, IEnumerable data)
        {
            throw new System.NotImplementedException();
        }

   

        public Task SendAsync(string function, IEnumerable data)
        {
            throw new System.NotImplementedException();
        }

    }
}
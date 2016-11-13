using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Decent.Minecraft.Client.Test.Fakes
{
    public class FakeConnection : IConnection
    {
        readonly List<KeyValuePair<string, IList>> _commandHistory = new List<KeyValuePair<string, IList>>();

        public KeyValuePair<string, IList> GetLastCommand()
        {
            return _commandHistory.Last();
        }

        public KeyValuePair<string, IList> GetLastCommand(string command)
        {
            return _commandHistory.Last(c => c.Key.EndsWith(command));
        }

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
                var command = new KeyValuePair<string, IList>(function, data);
                Console.WriteLine($"Sendng and receiving: {command.Key}");
                _commandHistory.Add(command);

                if (command.Key.EndsWith(".getPos"))
                {
                    if (_commandHistory.Any(c => c.Key.EndsWith(".setPos")))
                    {
                        var lastOrDefault = _commandHistory.LastOrDefault(c => c.Key.EndsWith(".setPos"));
                        return lastOrDefault.Value.FlattenToString();
                    }
                    return "0,0,0";
                }

                return string.Empty;
            });
        }

        public Task SendAsync(string function, params object[] data)
        {
            return Task.Run(() =>
            {
                var command = new KeyValuePair<string, IList>(function, data);
                Console.WriteLine($"Sendng: {command.Key}");
                _commandHistory.Add(command);
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
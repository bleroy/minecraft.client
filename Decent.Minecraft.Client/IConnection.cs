using System;
using System.Collections;
using System.Threading.Tasks;

namespace Decent.Minecraft.Client
{
    public interface IConnection : IDisposable
    {
        string Address { get; set; }
        int Port { get; set; }
        void Close();
        void Open();
        Task OpenAsync();
        string Receive();
        Task<string> ReceiveAsync();
        void Send(string function, params object[] data);
        void Send(string function, IEnumerable data);
        string SendAndReceive(string function, params object[] data);
        string SendAndReceive(string function, IEnumerable data);
        Task<string> SendAndReceiveAsync(string function, params object[] data);
        Task<string> SendAndReceiveAsync(string function, IEnumerable data);
        Task SendAsync(string function, params object[] data);
        Task SendAsync(string function, IEnumerable data);
    }
}
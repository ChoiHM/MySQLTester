using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CommonLibrary
{
    public class UDPSocket
    {
        public event onErrorEventHandler onError;
        public delegate void onErrorEventHandler(int errCode, string Description);

        public event onDataArrivalHandler onDataArrival;
        public delegate void onDataArrivalHandler(int bytesToRead, string receivedString);


        private Socket _socket;
        private const int bufSize = 1024;
        private State state = new State();
        private EndPoint epFrom = new IPEndPoint(IPAddress.Any, 0);
        private AsyncCallback recv = null;

        public class State : IDisposable
        {
            public byte[] buffer = new byte[bufSize];

            private bool disposedValue;

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        // 관리형 상태(관리형 개체)를 삭제합니다.
                        Array.Clear(buffer, 0, buffer.Length);
                    }

                    // 비관리형 리소스(비관리형 개체)를 해제하고 종료자를 재정의합니다.
                    // 큰 필드를 null로 설정합니다.
                    disposedValue = true;
                }
            }

            public void Dispose()
            {
                // 이 코드를 변경하지 마세요. 'Dispose(bool disposing)' 메서드에 정리 코드를 입력합니다.
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }

        public bool IsBounded
        {
            get
            {
                if (_socket.IsBound)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void Server(string address, int port)
        {
            try
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
                _socket.Bind(new IPEndPoint(IPAddress.Parse(address), port));
                Receive();
            }
            catch (Exception ex)
            {
                if (onError != null) { onError(1, ex.Message); }
            }
        }

        public void Close()
        {
            _socket.Close();
        }

        public void Client(string address, int port)
        {
            try
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                _socket.Connect(IPAddress.Parse(address), port);
                Receive();
            }
            catch (Exception ex)
            {
                if (onError != null) { onError(2, ex.Message); }
            }
        }

        public void Send(string text)
        {
            try
            {
                byte[] data = Encoding.ASCII.GetBytes(text);
                _socket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
                {
                    State so = (State)ar.AsyncState;
                    int bytes = _socket.EndSend(ar);
                    Console.WriteLine("SEND: {0}, {1}", bytes, text);
                    so?.Dispose();
                }, state);
            }
            catch (Exception ex)
            {
                if (onError != null) { onError(11, ex.Message); }
            }
        }

        private void Receive()
        {
            try
            {
                _socket.BeginReceiveFrom(state.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv = (ar) =>
                {
                    State so = (State)ar.AsyncState;
                    int bytes = _socket.EndReceiveFrom(ar, ref epFrom);
                    _socket.BeginReceiveFrom(so.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv, so);
                    if (onDataArrival != null) { onDataArrival(bytes, Encoding.Default.GetString(so.buffer, 0, bytes)); }
                    //Console.WriteLine("RECV: {0}: {1}, {2}", epFrom.ToString(), bytes, Encoding.ASCII.GetString(so.buffer, 0, bytes));
                    so?.Dispose();
                }, state);
            }
            catch (Exception ex)
            {
                if (onError != null) { onError(12, ex.Message); }
            }
        }
    }
}

using System;
using System.Net.Sockets;
using SiriusScientific.Core.Threading;

namespace SiriusScientific.Core.TcpIp
{
	public class TcpIpClient : ThreadObject, IDisposable
	{
		private readonly TcpClient _client;
		private readonly string _server;
		private readonly int _port;
		private const int MaxBuf = 16380;

		private readonly byte[] _rxbuffer;

		private NetworkStream _stream;

		public delegate void TcpIpConnectionChanged(ConnectionStates connectState, string reason);

		public event TcpIpConnectionChanged OnTcpIpConnectionChanged;

		public delegate void RxNotify(byte[] rxBuffer, int byteCount);

		public event RxNotify OnRxNotify;
		
		private readonly object _sendPadlock;
		
		public enum ConnectionStates
		{
			Connected,
			Disconnected,
			RemoteTerminated,
			Aborted,
		}

		public  TcpIpClient(string server, int port)
		{
			_server = server;

			_port = port;

			_rxbuffer = new byte[MaxBuf];

			_sendPadlock = new object();

			_client = new TcpClient();
		}

		public bool Connect()
		{
			_client.Connect(_server, _port);

			if(_client.Connected)
			{
				if(OnTcpIpConnectionChanged != null)
					OnTcpIpConnectionChanged(ConnectionStates.Connected, "connection opened.");

				_stream = _client.GetStream();

				base.Start(null);

				return true;
			}
			return false;
		}

		public void Disconnect()
		{
			if (_client.Connected)
			{
				base.Stop();

				_client.Close();
			}
		}

		public int Send(Byte[] txData)
		{
			lock (_sendPadlock)
			{
				if (_client != null)
				{
					if (_client.Connected)
					{
						_stream.Write(txData, 0, txData.Length);

						return txData.Length;
					}
				}
				return -1;
			}
		}

		public override void WorkerThread(object parameters)
		{
			int responseCount = 0;

			while(_client.Connected)
			{
				try
				{
					responseCount = _stream.Read(_rxbuffer, 0, MaxBuf);
					
					byte[] rxBuffer = new byte[responseCount];

					Buffer.BlockCopy(_rxbuffer, 0, rxBuffer, 0, responseCount);
					
					if (responseCount > 0)
					{
					if (OnRxNotify != null)
							OnRxNotify(rxBuffer, responseCount);
					}
					else
					{
						if(OnTcpIpConnectionChanged != null)
							OnTcpIpConnectionChanged(ConnectionStates.RemoteTerminated, "remotely closed due to no communication from client");

						break;
					}
				}
				catch (Exception ex)
				{
					if(OnTcpIpConnectionChanged != null)
						OnTcpIpConnectionChanged(ConnectionStates.RemoteTerminated, ex.ToString());
				
					break;
				}
			}
		}

		public void Dispose()
		{
		}
	}
}

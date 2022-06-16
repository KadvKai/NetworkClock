using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class NetworkTime : MonoBehaviour
{
    [SerializeField] private List<string> _ntpServers;
    private DateTime _networkTime;
    private bool _networkTimeSet;
    public event UnityAction<DateTime, string> NewNetworkTime;
    public void Synchronization()
    {
        _networkTimeSet = false;
        foreach (var server in _ntpServers)
        {
            Thread potok = new(new ParameterizedThreadStart(GetNetworkTime));
            potok.Start(server);
        }

    }

#nullable enable
    private void GetNetworkTime(object? server)
    {
        if (server is string ntpServer)
        {
            _networkTime = new DateTime();
            var ntpData = new byte[48];
            ntpData[0] = 0x1B;
            try
            {
                var addresses = Dns.GetHostEntry(ntpServer).AddressList;
                var ipEndPoint = new IPEndPoint(addresses[0], 123);
                using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                socket.Connect(ipEndPoint);
                socket.ReceiveTimeout = 3000;
                socket.Send(ntpData);
                socket.Receive(ntpData);
            }
            catch
            {
                return;
            }
            const byte serverReplyTime = 40;
            ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);
            ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);
            intPart = SwapEndianness(intPart);
            fractPart = SwapEndianness(fractPart);
            var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
            var networkDateTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);
            _networkTime = networkDateTime.ToLocalTime();
            if (!_networkTimeSet)
            {
                _networkTimeSet = true;
                NewNetworkTime?.Invoke(_networkTime, ntpServer);
            }
        }

    }
    static uint SwapEndianness(ulong x)
    {
        return (uint)(((x & 0x000000ff) << 24) +
                       ((x & 0x0000ff00) << 8) +
                       ((x & 0x00ff0000) >> 8) +
                       ((x & 0xff000000) >> 24));
    }
}


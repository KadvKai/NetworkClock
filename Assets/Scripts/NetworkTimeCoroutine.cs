using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkTimeCoroutine : MonoBehaviour
{
    const string ntpServer = "pool.ntp.org";
    private void OnEnable()
    {
        StartCoroutine(GetNetworkTime());
    }

    private IEnumerator GetNetworkTime()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("http://google.com"))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            Debug.Log("webRequest");
            Debug.Log(webRequest);
        }
       /* // NTP message size - 16 bytes of the digest (RFC 2030)
        var ntpData = new byte[48];
        //Setting the Leap Indicator, Version Number and Mode values
        ntpData[0] = 0x1B; //LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)
        var addresses = Dns.GetHostEntry(ntpServer).AddressList;
       */
    }
}


using Onvif.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Discovery;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Onvif
{
    public class OnvifCls
    {
        Func<string, string, int> callbackFunctionLocal;

        public OnvifCls(Func<string, string, int> callbackFunction)
        {
            this.callbackFunctionLocal = callbackFunction;
        }


        public void getRtspUrl()
        {
            var endPoint = new UdpDiscoveryEndpoint(DiscoveryVersion.WSDiscoveryApril2005);

            //((CustomBinding)endPoint.Binding).Elements.Insert(0,
            //        new MulticastCapabilitiesBindingElement(true));

            var discoveryClient = new DiscoveryClient(endPoint);

            discoveryClient.FindProgressChanged += discoveryClient_FindProgressChanged;

            //  discoveryClient.FindCompleted += discoveryClient_FindCompleted;

            FindCriteria findCriteria = new FindCriteria();
            findCriteria.Duration = TimeSpan.FromSeconds(15); //TimeSpan.MaxValue;
            findCriteria.MaxResults = 10; // int.MaxValue;
            // Edit: optionally specify contract type, ONVIF v1.0
            findCriteria.ContractTypeNames.Add(new XmlQualifiedName("NetworkVideoTransmitter",
                "http://www.onvif.org/ver10/network/wsdl"));

            discoveryClient.FindAsync(findCriteria);



            //return rtspUrl;
        }

        string serviceUrl = "";

        void discoveryClient_FindProgressChanged(object sender, FindProgressChangedEventArgs e)
        {
            var a = e.EndpointDiscoveryMetadata;
            var b = e.MessageSequence;

            serviceUrl = a.ListenUris[0].ToString();
            //var c = "http://192.168.31.12:5000/onvif/device_service";

            System.Net.ServicePointManager.Expect100Continue = false;

            var wsBinding = new WSHttpBinding(SecurityMode.None);
            wsBinding.Name = "My WSHttpBinding";

            wsBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;

            CustomBinding l_CustomBinding = new CustomBinding(wsBinding);
            MessageEncodingBindingElement l_EncodingElement =
                 l_CustomBinding.Elements.Find<MessageEncodingBindingElement>();
            l_EncodingElement.MessageVersion = MessageVersion.Soap12;

            EndpointAddress endpointAddress = new EndpointAddress(serviceUrl);

            //HttpTransportBindingElement transport = new HttpTransportBindingElement();
            //transport.MaxReceivedMessageSize = Int32.MaxValue; //100L * 1024L * 1024L
            //transport.KeepAliveEnabled = false;
            //transport.MaxBufferSize = Int32.MaxValue;
            //transport.ProxyAddress = null;
            //transport.BypassProxyOnLocal = true;
            ////transport.ManualAddressing = true
            //transport.UseDefaultWebProxy = false;
            //transport.TransferMode = TransferMode.StreamedResponse;
            ////transport.TransferMode = TransfeStreamedResponse
            ////transport.AuthenticationScheme = AuthenticationSchemes.None;
            ////transport.AuthenticationScheme = AuthenticationSchemes.Digest;


            //TextMessageEncodingBindingElement encoding = new TextMessageEncodingBindingElement
            //{
            //    MessageVersion = MessageVersion.CreateVersion(EnvelopeVersion.Soap12, AddressingVersion.None),
            //    WriteEncoding = System.Text.Encoding.UTF8,
            //    ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max
            //};

            //var binding = new CustomBinding(encoding, transport);

            //binding.Name = "RemoteDiscoveryBinding";


            //binding.CloseTimeout = TimeSpan.FromSeconds(30.0);
            //binding.OpenTimeout = TimeSpan.FromSeconds(30.0);
            ////binding.SendTimeout = TimeSpan.FromMinutes(3.0);
            //binding.SendTimeout = TimeSpan.FromMinutes(10.0);
            //binding.ReceiveTimeout = TimeSpan.FromMinutes(3.0);
            ////binding.Security.Mode = SecurityMode.Transport;
            ////binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;

            ////var factory = new ChannelFactory(binding);
            ////factory.Credentials.UserName.UserName = "admin";
            ////factory.Credentials.UserName.Password = "123";

            DeviceClient cl = new ServiceReference1.DeviceClient(l_CustomBinding, endpointAddress);
            GetDeviceInformationRequest inValue = new GetDeviceInformationRequest();
            string Model; string FirmwareVersion; string SerialNumber; string HardwareId;
            var Q = cl.GetDeviceInformation(out Model, out FirmwareVersion, out SerialNumber, out HardwareId);

            var cl2 = new MediaClient(l_CustomBinding, endpointAddress);
            var profiles = cl2.GetProfiles();

            var rtspIP = profiles[0].VideoEncoderConfiguration.Multicast.Address.IPv4Address;
            var rtspPort = profiles[0].VideoEncoderConfiguration.Multicast.Port;
            var rtspUrl = "rtsp://admin:q1234567@" + rtspIP + ":" + rtspPort + "/onvif1";

            callbackFunctionLocal.DynamicInvoke(new object[] { rtspUrl, serviceUrl });

            Console.WriteLine();
        }

        public static void sendPtzMove(string serviceUrl, float x, float y)
        {

            System.Net.ServicePointManager.Expect100Continue = false;

            var wsBinding = new WSHttpBinding(SecurityMode.None);
            wsBinding.Name = "My WSHttpBinding";

            wsBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;

            CustomBinding l_CustomBinding = new CustomBinding(wsBinding);
            MessageEncodingBindingElement l_EncodingElement =
                 l_CustomBinding.Elements.Find<MessageEncodingBindingElement>();
            l_EncodingElement.MessageVersion = MessageVersion.Soap12;

            EndpointAddress endpointAddress = new EndpointAddress(serviceUrl);

            PTZClient cl = new PTZClient(l_CustomBinding, endpointAddress);
            
            PTZSpeed velocity = new PTZSpeed();
            velocity.PanTilt = new Vector2D();
            velocity.PanTilt.x = 0;
            velocity.PanTilt.y = y;



            cl.ContinuousMove("IPCProfilesToken0", velocity, "0.5");

            velocity = new PTZSpeed();
            velocity.PanTilt = new Vector2D();
            velocity.PanTilt.x = x;
            velocity.PanTilt.y = 0;


            cl.ContinuousMove("IPCProfilesToken0", velocity, "0.5");



            //DeviceClient cl = new ServiceReference1.DeviceClient(l_CustomBinding, endpointAddress);
            //GetDeviceInformationRequest inValue = new GetDeviceInformationRequest();
            //string Model; string FirmwareVersion; string SerialNumber; string HardwareId;
            //var Q = cl.GetDeviceInformation(out Model, out FirmwareVersion, out SerialNumber, out HardwareId);

            //var cl2 = new MediaClient(l_CustomBinding, endpointAddress);
            //var profiles = cl2.GetProfiles();

            //var rtspIP = profiles[0].VideoEncoderConfiguration.Multicast.Address.IPv4Address;
            //var rtspPort = profiles[0].VideoEncoderConfiguration.Multicast.Port;
            //var rtspUrl = "rtsp://admin:q1234567@" + rtspIP + ":" + rtspPort + "/onvif1";

            //callbackFunctionLocal.DynamicInvoke(new object[] { rtspUrl, serviceUrl });

            Console.WriteLine();

        }

        void discoveryClient_FindCompleted(object sender, FindCompletedEventArgs e)
        {
            //Check endpoint metadata here for required types.
            Console.WriteLine();
        }

    }
}

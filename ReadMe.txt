This app requires two https://github.com/videolan/libvlcsharp components: https://www.nuget.org/packages/VideoLAN.LibVLC.Windows/ to working functional and https://www.nuget.org/packages/LibVLCSharp.Forms/ for MS VS 2019 form editor.

Onvif Service protocol located in ./ThirdParty/Specifications/ .
The main WSDL file of Onvif service: ./Onvif/Connected Services/ServiceReference1/onvif.wsdl

Filter for Wireshark:
ip.src == 192.168.31.12 || ip.dst == 192.168.31.12 || ip.src == 192.168.31.14


See also:

- Good app for sending HTTP requests: ./ThirdParty/ThirdPartyApps/Insomnia.Setup.7.1.1.part*.rar
- Good examples of communications with Onvif cameras: https://sourceforge.net/p/onvifdm/code/HEAD/tree/branches/v2.2.208/odm/odm.infra/ . Compiled version located ./ThirdParty/ThirdPartyApps/odm-v2.2.250.msi
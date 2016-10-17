using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace Vendas.View
{    public class RemoteStatic
    {
        public static ServicesInterface remoteObject;

        public static void RegistrarCanal()
        {
            TcpChannel tcpChannel = new TcpChannel();

            ChannelServices.RegisterChannel(tcpChannel);

            Type requiredType = typeof(ServicesInterface);

            //remoteObject = (ServicesInterface)Activator.GetObject(requiredType, "tcp://192.168.2.100:8080/Services");

            remoteObject = (ServicesInterface)Activator.GetObject(requiredType, "tcp://localhost:8080/Services");
        }
    }
}

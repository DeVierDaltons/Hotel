using Hotel.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Proxy
{
    public class GuestCallbackServiceProxy : DuplexClientBase<ICallback>, ICallback
    {
        public GuestCallbackServiceProxy(InstanceContext callbackInstance) : base(callbackInstance)
        {
            SubscribeClient();
        }

        public void Add(object item)
        {
            Channel.Add(item);
        }

        public void Edit(object item)
        {
            Channel.Edit(item);
        }

        public void Remove(object item)
        {
            Channel.Remove(item);
        }

        public void SubscribeClient()
        {
            Channel.SubscribeClient();
        }
    }

    public class RoomCallbackServiceProxy : DuplexClientBase<ICallback>, ICallback
    {
        public RoomCallbackServiceProxy(InstanceContext callbackInstance) : base(callbackInstance)
        {
            SubscribeClient();
        }

        public void Add(object item)
        {
            Channel.Add(item);
        }

        public void Edit(object item)
        {
            Channel.Edit(item);
        }

        public void Remove(object item)
        {
            Channel.Remove(item);
        }

        public void SubscribeClient()
        {
            Channel.SubscribeClient();
        }
    }

    public class BookingCallbackServiceProxy : DuplexClientBase<ICallback>, ICallback
    {
        public BookingCallbackServiceProxy(InstanceContext callbackInstance) : base(callbackInstance)
        {
            SubscribeClient();
        }

        public void Add(object item)
        {
            Channel.Add(item);
        }

        public void Edit(object item)
        {
            Channel.Edit(item);
        }

        public void Remove(object item)
        {
            Channel.Remove(item);
        }

        public void SubscribeClient()
        {
            Channel.SubscribeClient();
        }
    }
}

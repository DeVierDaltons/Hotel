using Hotel.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
    public class GuestCallbackService : ICallback
    {
        List<ICallback> CallbackChannels = new List<ICallback>();

        public void Add(object item)
        {
            var callingClient = OperationContext.Current.GetCallbackChannel<ICallback>();
            foreach (ICallback client in CallbackChannels)
            {
                if (callingClient != client)
                {
                    client.Add(item);
                }
            }
        }

        public void Edit(object item)
        {
            var callingClient = OperationContext.Current.GetCallbackChannel<ICallback>();
            foreach (ICallback client in CallbackChannels)
            {
                if (callingClient != client)
                {
                    client.Edit(item);
                }
            }
        }

        public void Remove(object item)
        {
            var callingClient = OperationContext.Current.GetCallbackChannel<ICallback>();
            foreach (ICallback client in CallbackChannels)
            {
                if (callingClient != client)
                {
                    client.Remove(item);
                }
            }
        }


        /// <summary>
        /// client should call this method before being notified to some event
        /// </summary>
        public void SubscribeClient()
        {
            var channel = OperationContext.Current.GetCallbackChannel<ICallback>();

            if (!CallbackChannels.Contains(channel)) //if CallbackChannels not contain current one.
            {
                (channel as ICommunicationObject).Closed += (object sender, EventArgs e) => UnSubscribeClient(channel);
                CallbackChannels.Add(channel);
            }
        }

        public void UnSubscribeClient(ICallback callback)
        {
            CallbackChannels.Remove(callback);
        }
    }

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
    public class BookingCallbackService : ICallback
    {
        List<ICallback> CallbackChannels = new List<ICallback>();

        public void Add(object item)
        {
            var callingClient = OperationContext.Current.GetCallbackChannel<ICallback>();
            foreach (ICallback client in CallbackChannels)
            {
                if (callingClient != client)
                {
                    client.Add(item);
                }
            }
        }

        public void Edit(object item)
        {
            var callingClient = OperationContext.Current.GetCallbackChannel<ICallback>();
            foreach (ICallback client in CallbackChannels)
            {
                if (callingClient != client)
                {
                    client.Edit(item);
                }
            }
        }

        public void Remove(object item)
        {
            var callingClient = OperationContext.Current.GetCallbackChannel<ICallback>();
            foreach (ICallback client in CallbackChannels)
            {
                if (callingClient != client)
                {
                    client.Remove(item);
                }
            }
        }


        /// <summary>
        /// client should call this method before being notified to some event
        /// </summary>
        public void SubscribeClient()
        {
            var channel = OperationContext.Current.GetCallbackChannel<ICallback>();

            if (!CallbackChannels.Contains(channel)) //if CallbackChannels not contain current one.
            {
                (channel as ICommunicationObject).Closed += (object sender, EventArgs e) => UnSubscribeClient(channel);
                CallbackChannels.Add(channel);
            }
        }

        public void UnSubscribeClient(ICallback callback)
        {
            CallbackChannels.Remove(callback);
        }
    }

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
    public class RoomCallbackService : ICallback
    {
        List<ICallback> CallbackChannels = new List<ICallback>();

        public void Add(object item)
        {
            var callingClient = OperationContext.Current.GetCallbackChannel<ICallback>();
            foreach (ICallback client in CallbackChannels)
            {
                if (callingClient != client)
                {
                    client.Add(item);
                }
            }
        }

        public void Edit(object item)
        {
            var callingClient = OperationContext.Current.GetCallbackChannel<ICallback>();
            foreach (ICallback client in CallbackChannels)
            {
                if (callingClient != client)
                {
                    client.Edit(item);
                }
            }
        }

        public void Remove(object item)
        {
            var callingClient = OperationContext.Current.GetCallbackChannel<ICallback>();
            foreach (ICallback client in CallbackChannels)
            {
                if (callingClient != client)
                {
                    client.Remove(item);
                }
            }
        }


        /// <summary>
        /// client should call this method before being notified to some event
        /// </summary>
        public void SubscribeClient()
        {
            var channel = OperationContext.Current.GetCallbackChannel<ICallback>();

            if (!CallbackChannels.Contains(channel)) //if CallbackChannels not contain current one.
            {
                (channel as ICommunicationObject).Closed += (object sender, EventArgs e) => UnSubscribeClient(channel);
                CallbackChannels.Add(channel);
            }
        }

        public void UnSubscribeClient(ICallback callback)
        {
            CallbackChannels.Remove(callback);
        }
    }


}

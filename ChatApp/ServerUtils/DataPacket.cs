using ChatAppLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerUtils
{
    public class DataPacket<T> : DAbstract where T : DAbstract
    {
        public string sender;
        public string type; // Id can for example be "chatMessage" or "LoginStatus"
        public T data; // Content of the message
    }

    class DataPacket : DAbstract
    {
        public string sender;
        public string type;
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JObject data;

        public DataPacket<T> GetData<T>() where T : DAbstract
        {
            return new DataPacket<T> {
                sender = this.sender,
                type = this.type, 
                data = this.data.ToObject<T>() 
            };
        }
    }

    class RegisterPacket : DAbstract
    {
        public string username;
        public string password;
    }

    class RegisterResponsePacket : DAbstract
    {
        public string status;
    }

    class LoginPacket : DAbstract
    {
        public string username;
        public string password;
    }

    class LoginResponse : DAbstract
    {
        public string status;
    }

    class ChatPacket : DAbstract
    {
        public string receiver;
        public string chatMessage;
    }

}

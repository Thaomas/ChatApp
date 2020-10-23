using ChatAppLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ServerUtils
{
    public class DataPacket<T> : DAbstract where T : DAbstract
    {
        public string type;
        public T data;
    }

    class DataPacket : DAbstract
    {
        public string type;
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JObject data;

        public DataPacket<T> GetData<T>() where T : DAbstract
        {
            return new DataPacket<T>
            {
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
        public List<string> chatLog;
    }

    class LoginPacket : DAbstract
    {
        public string username;
        public string password;
    }

    class LoginResponsePacket : DAbstract
    {
        public string status;
        public List<string> chatLog;
    }

    class ChatPacket : DAbstract
    {
        public string chatMessage;
    }

    class DisconnectPacket : DAbstract
    {

    }

    class DisconnectResponsePacket : DAbstract
    {
        public string status;
    }

}

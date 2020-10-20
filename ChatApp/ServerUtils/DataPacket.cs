using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RHApplicationLib.Abstract;
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

    class LoginPacket : DAbstract
    {
        public bool isClient;
        public string username;
        public string password;
    }

    class LoginResponse : DAbstract
    {
        public bool isClient;
        public string status;
    }

    class ChatPacket : DAbstract
    {
        public string receiver;
        public string chatMessage;
    }

    class ClientListPacket : DAbstract
    {
        public Dictionary<string, bool> clientList;
    }

    class UserNamePacket : DAbstract
    {
        public string clientUserName;
    }

    class UserNamePacketResponse : DAbstract
    {
        public string doctorUserName;
    }

    class ResistancePacket : DAbstract
    {
        public string receiver;
        public int resistance;
    }

    class RequestClientDataPacket : DAbstract
    {

    }

    public class StartStopPacket : DAbstract
    {
        public string receiver;
        public bool startSession;
    }

    class RequestSessionStatePacket : DAbstract
    {
        public string receiver;
    }

    class ResponseSessionStatePacket : DAbstract
    {
        public string receiver;
        public string sessionId;
        public bool sessionState;
        public DateTime startTimeSession;
    }

    //Eventueel nog responsePackets

    public class BikeDataPacket : DAbstract
    {
        public string receiver;
        public double speed;
        public int heartbeat;
        public double elapsedTime;
        public int distanceTraveled;
        public int power;
        public DateTime timestamp;
    }
}

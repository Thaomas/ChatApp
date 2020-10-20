using Newtonsoft.Json;

namespace ChatAppLib
{
    public abstract class DAbstract
    {
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

namespace Anlis.Service.Web
{
    public class WorkerStatus
    {
        public string Name { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public string Key { get { return IP + ":" + Port; } }
        public override string ToString()
        {
            return string.Format("{0} : {1}", Key, Name);
        }
    }
} 
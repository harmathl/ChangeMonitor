using System.Text.Json;

namespace ChangeMonitor.Models {
    public class SignalRMessage {

        public string CreateDate { get; set; }
        public string ChangeType { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public SignalRMessage(string createDate, string changeType, string key, string value) {
            CreateDate = createDate;
            ChangeType = changeType;
            Key = key;
            Value = value;
        }

        public string Serialize() {
            return JsonSerializer.Serialize(this,null);
        }
    }
}

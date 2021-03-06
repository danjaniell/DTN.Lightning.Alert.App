using System.Collections.Generic;

namespace DTN.Lightning.Alert.App.Data.Models
{
    public class JsonObjWrapper
    {
        public Dictionary<object, object> Values { get; set; }
        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
        public string ErrorMessage { get; set; }
    }
}

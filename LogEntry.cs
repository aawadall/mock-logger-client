using System;
using System.Text.Json.Serialization;

namespace exp11
{
    public class LogEntry
    {

        public string SenderId { get; set; }
        public DateTime Timestamp { get; set; }
        public string CorrelationId { get; set; }
        public SeverityType Severity { get; set; }

        [JsonInclude]
        public Payload Payload { get; set; }
    }


    public enum SeverityType
    {
        Information,
        Warning,
        Error,
        Fatal,
        Debug,
    }
}
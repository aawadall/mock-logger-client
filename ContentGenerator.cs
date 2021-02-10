using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace exp11
{
    public static class LogEntryExtensions
    {
        static Random random = new Random();
        public static string Serialize(this LogEntry logEntry)
        {
            return JsonSerializer.Serialize(logEntry, typeof(LogEntry), new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            });
        }

        public static LogEntry MakeLogEntry()
        {
            return new LogEntry
            {
                //_id = RandomInner(),
                SenderId = RandomInner(),
                Timestamp = DateTime.UtcNow,
                CorrelationId = RandomCorrelation(),
                Severity = RandomSeverity(),
                Payload = MakePayload()
            };
        }

        private static SeverityType RandomSeverity()
        {

            var values = Enum.GetValues(typeof(SeverityType));
            return (SeverityType)(values).GetValue(random.Next(values.Length));
        }

        private static string RandomCorrelation()
        {
            return $"id-{random.Next(99)}";
        }

        public static Payload MakePayload()
        {
            return new SimplePayload
            {
                Inner = RandomInner()
            };
        }

        private static string RandomInner()
        {
            var inner = Guid.NewGuid().ToString();
            System.Console.WriteLine("inner: " + inner);
            return inner;
        }
    }
}
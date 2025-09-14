using Newtonsoft.Json;
using SfCompulsory_cs.Data;
using SfCompulsory_cs.Models;
using System.Text;

namespace SfCompulsory_cs.Services
{
    public class LogService
    {
        private readonly ApplicationDbContext _context;
        private readonly string logDirectory = "Logs";
        private readonly string logFileJson = "Logs/logs.json";
        private readonly string logFileTxt = "Logs/logs.txt";

        public LogService(ApplicationDbContext context)
        {
            _context = context;

            if (!Directory.Exists(logDirectory))
                Directory.CreateDirectory(logDirectory);
        }

        public async Task AddLog(string level, string message, long? userId = null)
        {
            var log = new Log
            {
                Level = level,
                Message = message,
                Timestamp = DateTime.UtcNow,
                UserId = userId
            };

            _context.Logs.Add(log);
            await _context.SaveChangesAsync();

            AppendToTxt(log);
            AppendToJson();
        }

        private void AppendToTxt(Log log)
        {
            var logLine = $"{log.Timestamp:yyyy-MM-dd HH:mm:ss} [{log.Level}] {log.Message}";
            File.AppendAllText(logFileTxt, logLine + Environment.NewLine, Encoding.UTF8);
        }

        private void AppendToJson()
        {
            var allLogs = _context.Logs.ToList();
            var json = JsonConvert.SerializeObject(allLogs, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(logFileJson, json, Encoding.UTF8);
        }

        public List<Log> GetAllLogs()
        {
            return _context.Logs.ToList();
        }
    }
}

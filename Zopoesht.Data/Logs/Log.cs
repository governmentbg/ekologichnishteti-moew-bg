﻿using Zopoesht.Data.Logs.Enums;

namespace Zopoesht.Data.Logs
{
    public class Log
    {
        public int Id { get; set; }
        public LogType Type { get; set; }
        public DateTime LogDate { get; set; }
        public string IP { get; set; }
        public string Verb { get; set; }
        public string Url { get; set; }
        public string UserAgent { get; set; }
        public int? UserId { get; set; }
        public string Message { get; set; }
        public string RequestBody { get; set; }
    }
}
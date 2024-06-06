﻿namespace DataBase.Models
{
    public class AddressDBModel
    {
        public Guid Id { get; set; }
        public string Article { get; set; } = string.Empty;
        public string Zone { get; set; } = string.Empty;
        public string Row { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public string Qty { get; set; } = string.Empty;
    }
}

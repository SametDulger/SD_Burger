using System;
using System.Text.Json.Serialization;
using SD_Burger.Core.Entities;

namespace SD_Burger.Application.DTOs
{
    public class TableDto
    {
        public int Id { get; set; }
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TableStatus Status { get; set; }
        
        public int BranchId { get; set; }
        public string? BranchName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class CreateTableDto
    {
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TableStatus Status { get; set; }
        
        public int BranchId { get; set; }
    }

    public class UpdateTableDto
    {
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TableStatus Status { get; set; }
        
        public int BranchId { get; set; }
        public bool IsActive { get; set; }
    }
} 
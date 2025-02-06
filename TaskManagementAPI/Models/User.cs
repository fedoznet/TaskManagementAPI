﻿namespace TaskManagementAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;   
        public string Role { get; set; } = "Admin";  // "Admin" or "User"
        public ICollection<TaskItem> AssignedTasks { get; set; } = new List<TaskItem>();
    }
}

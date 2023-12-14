﻿using System.ComponentModel.DataAnnotations;

namespace project_for_space_app.Models;

public class Purchase
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Пользователь не указан")]
    public int userId { get; set; }
    public User? user { get; set; }
    public float price { get; set; }
    public DateTime date { get; set; }
}

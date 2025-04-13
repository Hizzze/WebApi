using App.Enums;

namespace App.Models;

public class ChangeRoleDto
{
    public Guid Id { get; set; }
    public string NewRole { get; set; } = string.Empty;
}
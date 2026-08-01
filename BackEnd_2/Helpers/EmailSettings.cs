using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class EmailSettings
{
    public string? Port {get; set;}
    public string? Host {get; set;}
    public string? Password {get; set;}
    public string? From {get; set;}
    public  string? Name {get; set;}
}
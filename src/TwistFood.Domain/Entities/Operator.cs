namespace TwistFood.Api.Models;

public class Operator
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Imagepath { get; set; } = string.Empty;
    public double Salary { get; set; }
    public string PassportSeriaNumber { get; set; } = string.Empty;

}

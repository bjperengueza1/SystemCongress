using Application.Commands;

namespace Application.Congress.Commands;

public class CreateCongressCommand : ICommand
{
    public string Name { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public string Location { get; set; }
}
using Timesheets.Models;
using Timesheets.Services;

namespace Timesheets.Test;
public class CsvServiceTest
{

    [Fact]
    public void GivenAListOfTimesheets_RetrunAStringinCSVFormat()
    {
        var csvService = new CsvService();
        var timesheets = new List<Timesheet>()
        { 
            new Timesheet
            { 
                TimesheetEntry = new TimesheetEntry
                   { 
                        Date ="01/01/2001", 
                        FirstName="first",
                        Hours = "8",
                        LastName ="last",
                        Project ="proj" 
                    },
                TotalHours = "8" 
            } 
        };
        var csv = csvService.CreateCSV(timesheets);
        Assert.Equal("Id, FirstName, LastName, Date, Project, Hours, TotalHours\r\n0, first, last, 01/01/2001, proj, 8, 8\r\n", csv);
    }

    [Fact]
    public void GivenAnEmptyListOfTimesheets_RetrunAnEmptyString()
    {
        var csvService = new CsvService();
        var timesheets = new List<Timesheet>();
        var csv = csvService.CreateCSV(timesheets);
        Assert.Equal(string.Empty,csv);
    }
}

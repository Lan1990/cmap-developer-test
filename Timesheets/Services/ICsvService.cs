using Timesheets.Models;

namespace Timesheets.Services;
public interface ICsvService
{
    string CreateCSV(IList<Timesheet> timesheets);
}
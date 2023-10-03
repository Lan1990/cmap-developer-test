should a project be an object and then you can categories them and have them in a dropdown when adding a time sheet so you don't have mispellings


TimesheetEntry.cs 
Hours should be a decimal 
Date should be a datetime
can the first name, last name and project be null, if not should be given a default value if so should be marked as nullable?

Timesheet.cs 
TotalHours should be a decimal 


TimesheetRepository.cs
should the get be async?

TimesheetController.cs
post gets the list of all timesheets but does nothing with it
no validation to check values are valid
timesheetEntry could be null

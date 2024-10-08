using Castle.Core.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using Timesheets.Infrastructure;
using Timesheets.Models;
using Timesheets.Repositories;
using Timesheets.Services;

namespace Timesheets.Test
{
    public class TimesheetTests
    {
        [Fact]
        public void GivenAValidTimesheet_ThenAddTimesheetToInMemoryDatabase()
        {
            //Arrange
            var timesheet = new Timesheet();
            var timesheetEntry = new TimesheetEntry()
            {
                Id = 1,
                Date = "01/09/2023",
                Project = "Test Project",
                FirstName = "Test",
                LastName = "Test",
                Hours = "7.5"
            };
            timesheet.Id = 1;
            timesheet.TimesheetEntry = timesheetEntry;
            timesheet.TotalHours = timesheetEntry.Hours;

            var mockRepository = new Mock<ITimesheetRepository>();
            var timesheetService = new TimesheetService(mockRepository.Object);

            // Act
            timesheetService.Add(timesheet);

            // Assert
            mockRepository.Verify(repo => repo.AddTimesheet(It.IsAny<Timesheet>()), Times.Once);
        }

        [Fact]
        public void WhenRequestingTimesheets_ReturnTimesheetsInMemoryDatabase()
        {
            //Arrange
            var timesheet = new Timesheet();
            var timesheetEntry = new TimesheetEntry()
            {
                Id = 1,
                Date = "01/09/2023",
                Project = "Test Project",
                FirstName = "Test",
                LastName = "Test",
                Hours = "7.5"
            };
            timesheet.Id = 1;
            timesheet.TimesheetEntry = timesheetEntry;
            timesheet.TotalHours = timesheetEntry.Hours;

            var mockRepository = new Mock<ITimesheetRepository>();
            mockRepository.Setup(x => x.GetAllTimesheets()).Returns(new[] { timesheet });   
            var timesheetService = new TimesheetService(mockRepository.Object);

            // Act
            var timesheets= timesheetService.GetAll();

            // Assert
            Assert.Equal(1, timesheets.Count);
            Assert.Equal(timesheetEntry.Id, timesheets[0].Id);
            Assert.Equal(timesheetEntry.FirstName, timesheets[0].TimesheetEntry.FirstName);
            Assert.Equal(timesheetEntry.LastName, timesheets[0].TimesheetEntry.LastName);
            Assert.Equal(timesheetEntry.Project, timesheets[0].TimesheetEntry.Project);
            Assert.Equal(timesheetEntry.Date, timesheets[0].TimesheetEntry.Date);
        }

    }
}

namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Globalization;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;
    class CreateEvent : ICommand
    {
        public string Execute(string[] data)
        {
            Check.CheckLength(6, data);
            var creator = AuthenticationManager.GetCurrentUser();
            var name = data[0];
            var description = data[1];
            var startDate = this.ParseDate(data[2], data[3]);
            var endDate = this.ParseDate(data[4], data[5]);

            if (startDate > endDate)
            {
                throw new ArgumentException("Start date should be before end date.");
            }

            if (!AuthenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }
          
            using (var db = new TeamBuilderContext())
            {
                var createEvent = new Event()
                {
                    CreatorId = creator.Id,
                    Name = name,
                    Description = description,
                    StartDate = startDate,
                    EndDate = endDate
                };
                db.Events.Add(createEvent);
                db.SaveChanges();
            }
            return $"Event {name} was created successfully!";
        }
        private DateTime ParseDate(string date, string hour)
        {
            var completeDateString = $"{date} {hour}";

            DateTime parsedDate;
            var isDateValid = DateTime.TryParseExact(completeDateString,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out parsedDate);

            if (!isDateValid)
            {
                throw new ArgumentException("Please insert the dates in format: [dd/MM/yyyy HH:mm]!");
            }

            return parsedDate;
        }
    }
}

using MedicApp.Database;
using MedicApp.Models;

namespace MedicApp.Integrations
{
    public interface IWorkingHoursIntegration
    {
        WorkingHours SaveWorkingHours(SaveWorkingHoursModel wokringHoursModel);
        WorkingHours LoadDBWorkingHourById(Guid Id);
        LoadWorkingHoursModel LoadWorkingHourById(Guid Id)
    }

    public class WorkingHoursIntegration : IWorkingHoursIntegration
    {
        private readonly AppDbContext _appDbContext;

        public WorkingHoursIntegration(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public WorkingHours SaveWorkingHours(SaveWorkingHoursModel wokringHoursModel)
        {
            var dbWorkingHour = _appDbContext.WorkingHours.Where(x => !x.IsDeleted && x.Id == wokringHoursModel.Id).FirstOrDefault();
            if(dbWorkingHour == null)
            {
                dbWorkingHour = new WorkingHours();
            }

            dbWorkingHour.Start = wokringHoursModel.Start.TimeOfDay;
            dbWorkingHour.End = wokringHoursModel.End.TimeOfDay;
            dbWorkingHour.IsMonday = wokringHoursModel.IsMonday;
            dbWorkingHour.IsTuesday = wokringHoursModel.IsTuesday;
            dbWorkingHour.IsWednesday = wokringHoursModel.IsWednesday;
            dbWorkingHour.IsThursday = wokringHoursModel.IsThursday;
            dbWorkingHour.IsFriday = wokringHoursModel.IsFriday;
            dbWorkingHour.IsSaturday = wokringHoursModel.IsSaturday;

            _appDbContext.WorkingHours.Add(dbWorkingHour);
            _appDbContext.SaveChanges();
            return dbWorkingHour;
        }

        public WorkingHours LoadDBWorkingHourById(Guid Id)
        {
            var dbWorkingHour = _appDbContext.WorkingHours.FirstOrDefault(x => !x.IsDeleted && x.Id == Id);
            if(dbWorkingHour == null)
            {
                throw new KeyNotFoundException("Working hours does not exist");
            }
            
            return dbWorkingHour;
        }
        public LoadWorkingHoursModel LoadWorkingHourById(Guid Id)
        {
            var dbWorkingHour = _appDbContext.WorkingHours.FirstOrDefault(x => !x.IsDeleted && x.Id == Id);
            if(dbWorkingHour == null)
            {
                throw new KeyNotFoundException("Working hours does not exist");
            }
            var result = new LoadWorkingHoursModel()
            {
                Id = dbWorkingHour.Id,
                Start = dbWorkingHour.Start,
                End = dbWorkingHour.End,
                IsMonday = dbWorkingHour.IsMonday,
                IsTuesday = dbWorkingHour.IsTuesday,
                IsWednesday = dbWorkingHour.IsWednesday,
                IsThursday = dbWorkingHour.IsThursday,
                IsFriday = dbWorkingHour.IsFriday,
                IsSaturday = dbWorkingHour.IsSaturday,
            };
            return result;
        }
    }
}

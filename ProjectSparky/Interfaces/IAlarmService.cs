using ProjectSparky.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectSparky.Interfaces
{
    public interface IAlarmService
    {
        Task<Alarm> GetAlarm(string AlarmGUID);
        //List<Alarm> GetAlarms();
        Task<List<Alarm>> GetAlarms();
        //List<Alarm> GetAlarms(bool Active);
        Task<List<Alarm>> GetAlarms(bool Active);
        void AddAlarm(Alarm addAlarm);
        void UpdateAlarm(Alarm modAlarm);
        void DeleteAlarm(Alarm delAlarm);
    }
}

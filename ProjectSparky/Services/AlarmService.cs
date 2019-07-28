using Microsoft.EntityFrameworkCore;
using ProjectSparky.Interfaces;
using ProjectSparky.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSparky.Services
{
    public class AlarmService : IAlarmService
    {
        public async Task<Alarm> GetAlarm(string AlarmGUID)
        {
            Alarm rAlarm = null;

            using (var db = new ProjectSparyContext())
            {
                rAlarm = await db.Alarms.FirstOrDefaultAsync(x => x.AlarmGUID == AlarmGUID);
            }

            return rAlarm;
        }

        public async Task<List<Alarm>> GetAlarms()
        {
            List<Alarm> rAlarms = null;

            using (var db = new ProjectSparyContext())
            {
                rAlarms = await db.Alarms.ToListAsync();
            }

            return rAlarms;
        }

        public async Task<List<Alarm>> GetAlarms(bool Active)
        {
            List<Alarm> rAlarms = null;

            using (var db = new ProjectSparyContext())
            {
                rAlarms = await db.Alarms.Where(x => x.Active == Active).ToListAsync();
            }

            return rAlarms;
        }

        /*
        public List<Alarm> GetAlarms()
        {
            List<Alarm> rAlarms = null;

            using (var db = new ProjectSparyContext())
            {
                rAlarms = db.Alarms.ToList();
            }

            return rAlarms;
        }

        public List<Alarm> GetAlarms(bool Active)
        {
            List<Alarm> rAlarms = null;

            using (var db = new ProjectSparyContext())
            {
                rAlarms = db.Alarms.Where(x => x.Active == Active).ToList();
            }

            return rAlarms;
        }*/
         
        public async void AddAlarm(Alarm addAlarm)
        {
            using (var db = new ProjectSparyContext())
            {
                db.Alarms.Add(addAlarm);
                await db.SaveChangesAsync();
            }
        }

        public async void UpdateAlarm(Alarm modAlarm)
        {
            using (var db = new ProjectSparyContext())
            {
                db.Alarms.Update(modAlarm);
                await db.SaveChangesAsync();
            }
        }

        public async void DeleteAlarm(Alarm modAlarm)
        {
            using (var db = new ProjectSparyContext())
            {
                db.Alarms.Remove(modAlarm);
                await db.SaveChangesAsync();
            }
        }
    }
}

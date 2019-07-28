using ProjectSparky.Model.BingDailyImage;
using System.Threading.Tasks;

namespace ProjectSparky.Interfaces
{
    public interface IBingDailyImageService
    {
        Task<DailyImage> GetTodaysImage();
    }
}

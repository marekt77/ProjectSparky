using Newtonsoft.Json;
using ProjectSparky.Interfaces;
using ProjectSparky.Model.BingDailyImage;
using ProjectSparky.Model.BingDailyImage.Raw;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Web.Http;

namespace ProjectSparky.Services
{
    public class BingDailyImageService : IBingDailyImageService
    {
        private string baseURL = "https://www.bing.com/";

        public async Task<DailyImage> GetTodaysImage()
        {
            DailyImage _DailyImage = new DailyImage();

            Uri myURI;

            myURI = new Uri(baseURL + "HPImageArchive.aspx?format=js&idx=0&n=1");
            
            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {
                httpResponse = await App.BingClient.GetAsync(myURI);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var RawBingDailyImage = JsonConvert.DeserializeObject<RawDailyImage>(httpResponse.Content.ToString());

                    if(RawBingDailyImage.images.Count >= 1)
                    {
                        _DailyImage.Image = new BitmapImage(new Uri(baseURL + RawBingDailyImage.images[0].urlbase + "_800x480.jpg"));
                    }
                }
                else
                {
                    throw new Exception("ERROR! Cannot retrive Daily Bing Image.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR! Cannot retrive Daily Bing Image.", ex);
            }

            return _DailyImage;
        }
    }
}

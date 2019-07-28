using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Display;

namespace ProjectSparky.Tools
{
    public static class DeviceControls
    {
        public static void SetBrightnessLevel(double brightnessLevel)
        {
            BrightnessOverride bo = BrightnessOverride.GetForCurrentView();

            if(bo.IsSupported) 
            {
                bo.SetBrightnessLevel(brightnessLevel, DisplayBrightnessOverrideOptions.None);

                bo.StartOverride();
            }
        }
    }
}

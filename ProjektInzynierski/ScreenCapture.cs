﻿using SlimDX.Direct3D9;
using System;

namespace ProjektInzynierski
{
    public class ScreenCapture
    {
        private  Device _device;
       
        public ScreenCapture()
        {
            PresentParameters _presentParameters =new PresentParameters();
            _presentParameters.Windowed = true;
            _presentParameters.SwapEffect=SwapEffect.Discard;
            _device = new Device(new Direct3D(), 0, DeviceType.Hardware, IntPtr.Zero, CreateFlags.MixedVertexProcessing, _presentParameters);
        }

        public Surface CaptureScreen()
        {
            
            Surface _surface = Surface.CreateOffscreenPlain(_device,Convert.ToInt32(System.Windows.SystemParameters.PrimaryScreenWidth) , Convert.ToInt32(System.Windows.SystemParameters.PrimaryScreenHeight), Format.A8R8G8B8, Pool.Scratch);
            try
            {
                _device.GetFrontBufferData(0, _surface);
            }
            catch (Exception)
            {
                
               
            }
           
            return _surface;
        }
    }
}
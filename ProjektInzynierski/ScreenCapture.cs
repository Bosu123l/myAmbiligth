using SlimDX.Direct3D9;
using System;

namespace ProjektInzynierski
{
    public class ScreenCapture
    {
        private Device device;
        private Surface surface;
        public ScreenCapture()
        {
            PresentParameters _presentParameters =new PresentParameters();
            _presentParameters.Windowed = true;
            _presentParameters.SwapEffect=SwapEffect.Discard;
            device = new Device(new Direct3D(), 0, DeviceType.Hardware, IntPtr.Zero, CreateFlags.SoftwareVertexProcessing, _presentParameters);
        }

        public Surface CaptureScreen()
        {
            
            surface = Surface.CreateOffscreenPlain(device,Convert.ToInt32(System.Windows.SystemParameters.WorkArea.Width) , Convert.ToInt32(System.Windows.SystemParameters.WorkArea.Height), Format.A8R8G8B8, Pool.Scratch);
            device.GetFrontBufferData(0, surface);
            return surface;
        }
    }
}
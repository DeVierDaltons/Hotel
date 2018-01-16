using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hotel.View
{
    //see here: https://www.mesta-automation.com/tecniques-scaling-wpf-application/
    public class DpiDecorator : Decorator
    {
        //this DPIDecorator ignores all set DPI settings.
        public DpiDecorator()
        {
            this.Loaded += (s, e) =>
            {
                Matrix m = PresentationSource.FromVisual(this).CompositionTarget.TransformToDevice;
                ScaleTransform dpiTransform = new ScaleTransform(1 / m.M11, 1 / m.M22);
                if (dpiTransform.CanFreeze)
                    dpiTransform.Freeze();
                this.LayoutTransform = dpiTransform;
            };
        }
    }
}

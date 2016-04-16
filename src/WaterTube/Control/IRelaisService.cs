using System;

namespace WaterTube.Control
{
    public interface IRelaisService : IDisposable
    {
        void ActivateAlarmLight();
        void DeactivateAlarmLight();
        void OpenWaterTap();
        void CloseWaterTap();
    }
}

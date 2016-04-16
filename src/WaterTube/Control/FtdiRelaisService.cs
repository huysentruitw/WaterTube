using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterTube.Control
{
    public class FtdiRelaisService : IRelaisService
    {
        public FtdiRelaisService()
        {
        }

        ~FtdiRelaisService()
        {
            this.Dispose();
        }

        public void ActivateAlarmLight()
        {
            throw new NotImplementedException();
        }

        public void DeactivateAlarmLight()
        {
            throw new NotImplementedException();
        }

        public void OpenWaterTap()
        {
            throw new NotImplementedException();
        }

        public void CloseWaterTap()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iService_Admin.Models
{
    public static class ImageStorage
    {
        private static ImageSource _serviceLogo;

        public static ImageSource ServiceLogo
        {
            get { return _serviceLogo; }
            set { _serviceLogo = value; }
        }
        public static void RemoveServiceLogo()
        {
            _serviceLogo = null;
        }
    }

}

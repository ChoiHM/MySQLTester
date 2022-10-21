using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public static class global
    {
        public static string Version
        {
            get
            {
                return Properties.Resources.build.Replace(Environment.NewLine, "").Trim();
            }
        }
    }
}

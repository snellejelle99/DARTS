using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DARTS
{
    class LegRangeRule : IDataErrorInfo
    {
        private int leg;
        private int set;

        public int Leg
        {
            get { return leg; }
            set { leg = value; }
        }

        public int Set
        {
            get { return set; }
            set { set = value; }
        }

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string name]
        {
            get
            {
                string result = null;

                if (name == "Leg")
                {
                    if (this.leg < 1 || this.leg > 99)
                        result = "Leg cannot be smaller than 1 or higher than 99.";
                }
                else if (name == "Set")
                {
                    if(this.set < 1 || this.set > 99)
                        result = "Set cannot be smaller than 1 or higher than 99.";
                }
                return result;
            }
        }
        
    }
}

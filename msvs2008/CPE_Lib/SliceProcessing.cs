using System;
using System.Collections.Generic;
using System.Text;

namespace CPE_Lib
{
    public class SliceProcessing
    {
        public virtual void Config()
        {
        }

        public virtual void Config(object data)
        {
        }

        public virtual void Init()
        {
        }

        public virtual void Activate()
        {
        }

        public virtual bool Process(Slice input)
        {
            bool result = true;
            return result;
        }

        public virtual void DeActivate()
        {
        }


        public virtual void DeInit()
        {

        }

    }
}

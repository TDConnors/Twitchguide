using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Modules;

namespace TwitchGuide.Models
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ITimeBlock>().To<Timeblock>();
        }
    }
}
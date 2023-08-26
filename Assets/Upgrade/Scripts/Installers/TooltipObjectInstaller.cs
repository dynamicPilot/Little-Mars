using LittleMars.TooltipSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace LittleMars.Installers
{
    public class TooltipObjectInstaller : MonoInstaller<TooltipObjectInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<TooltipMenu>().AsSingle();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace LittleMars.SaveSystem
{
    public class ProjectManager : IInitializable
    {
        SavesSystemManager.Factory _savesManagerFactory;

        public ProjectManager(SavesSystemManager.Factory savesManagerFactory)
        {
            _savesManagerFactory = savesManagerFactory;
        }

        public void Initialize()
        {
            Start();
        }

        void Start()
        {
            using var manager = _savesManagerFactory.Create();
            manager.SetSavesSystem();
        }
    }
}

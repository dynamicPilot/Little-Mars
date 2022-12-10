using LittleMars.Models;
using LittleMars.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleMars.Controllers
{
    public class Controller : IController
    {
        IModel _model;
        IView _view;
        public Controller(IModel model, IView view)
        {
            _model = model;
            _view = view;
        }

        public void StartGame()
        {

        }
    }
}

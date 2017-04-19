using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class ExitSceneCommand : Controller
{
    public override void Execute(object data)
    {
        Game.Instance.ObjectPool.UnspawnAll();
    }
}
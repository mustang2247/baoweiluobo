using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class EndLevelCommand : Controller
{
    public override void Execute(object data)
    {
        EndLevelArgs e = data as EndLevelArgs;
        GameModel gm = GetModel<GameModel>();
        RoundModel rModel = GetModel<RoundModel>();

        //停止出怪
        rModel.StopRound();

        //停止游戏
        gm.EndLevel(e.IsSuccess);

        //胜利
        if (e.IsSuccess)
        {
            //显示胜利面板
            GetView<UIWin>().Show();
        }
        else
        {
            //显示失败面板
            GetView<UILost>().Show();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Round
{
    public int Monster; //怪物类型ID
    public int Count;   //怪物数量

    public Round(int monster, int count)
    {
        this.Monster = monster;
        this.Count = count;
    }
}
using System.Collections;
using System.Collections.Generic;
using PlayerIOClient;
using UnityEngine;

public class MoveC2S : IFunction
{
    public void Execute(Message m)
    {
        int oldPosX = m.GetInt(1);
        int oldPosY = m.GetInt(2);
        int newPosX = m.GetInt(3);
        int newPosY = m.GetInt(4);
        GridManager.Instance.MovePawn(new Vector2Int(newPosX, newPosY), new Vector2Int(oldPosX, oldPosY));
    }
}

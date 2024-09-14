using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase
{
    public virtual void OnStateEnter(object o = null)
    {
        Debug.Log("Entrou em OnStateEnter");
    }

    public virtual void OnStateStay()
    {
        Debug.Log("Entrou em OnStateStay");
    }

    public virtual void OnStateExit()
    {
        Debug.Log("Entrou em OnStateExit");
    }
}
public class StateIdle : StateBase
{
    public PlayerNave player;

    public override void OnStateEnter(object o = null)
    {
        player = (PlayerNave)o;
        player.ChangeColor(Color.white);
        base.OnStateEnter(o);
    }
}
public class StateRunning : StateBase
{
    public PlayerNave player;
    public override void OnStateEnter(object o = null)
    {
        player = (PlayerNave)o;
        player.canMove = true;
        player.ChangeColor(Color.blue);
        base.OnStateEnter(o);
    }

    public override void OnStateExit()
    {
        player.canMove = false;
        base.OnStateExit();
    }
}
public class StateDead : StateBase
{
    public PlayerNave player;

    public override void OnStateEnter(object o = null)
    {
        player = (PlayerNave)o;
        player.ChangeColor(Color.magenta);
        base.OnStateEnter(o);
    }
}

public class StateWin : StateBase
{
    public PlayerNave player;

    public override void OnStateEnter(object o = null)
    {
        player = (PlayerNave)o;
        player.canMove = false;
        base.OnStateEnter(o);
    }
}
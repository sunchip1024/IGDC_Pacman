using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    public enum COMMAND { FORWARD, ROTATE, STOP, NULL };

    protected Rigidbody rigid;
    protected float time;
    protected float unit;

    public Command(Rigidbody rigid, float time, float unit) 
    { 
        this.rigid = rigid;
        this.time = time;
        this.unit = unit;
    }

    public float GetTime() { return time; }
    public float GetUnit() { return unit; }

    /// <summary>
    /// action method
    /// </summary>
    public abstract void execute();
}

public class Forward : Command
{
    public Forward(Rigidbody rigid, float time, float unit) : base(rigid, time, unit) { }

    public override void execute()
    {
        Vector3 direction = rigid.transform.forward * (unit / time);
        rigid.MovePosition(rigid.position + direction * Time.deltaTime);
    }
}

public class Rotate : Command
{
    public Rotate(Rigidbody rigid, float time, float unit) : base(rigid, time, unit) { }
    public override void execute()
    {
        Vector3 EulerAngularVector = rigid.rotation.eulerAngles + Vector3.down * (unit / time) * Time.deltaTime;
        Quaternion rot = Quaternion.Euler(EulerAngularVector);
        rigid.MoveRotation(rot);
    }
}

public class Stop : Command
{
    public Stop(Rigidbody rigid, float time) : base(rigid, time, 0) { }
    public override void execute()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }
}

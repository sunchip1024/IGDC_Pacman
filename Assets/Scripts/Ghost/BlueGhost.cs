using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGhost : Ghost
{
    private Timer timer;

    private Rigidbody rigid;

    private enum STATE { TURN, GO };
    private STATE state = STATE.GO;
    private float rotSpeed = 0;

    private void Awake()
    {
        timer = gameObject.AddComponent<Timer>();
        rigid = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();

        speed = Random.Range(4f, 6f);
        chaseDistance = 0;
    }

    private void Update()
    {
        Act();
    }

    protected override void Patrol()
    {
        switch(state)
        {
            case STATE.TURN:
                PatrolTurn();
                break;

            case STATE.GO:
                PatrolGo();
                break;
        }
    }

    private void PatrolTurn()
    {
        if(timer.isTimerEnd()) {
            timer.startTimer(1.2f);
            state = STATE.GO;
        } else {
            base.ghostTr.Rotate(Vector3.up * Time.deltaTime * rotSpeed, Space.World);
        }
    }

    private void PatrolGo()
    {
        if(timer.isTimerEnd()) {
            timer.startTimer(0.3f);
            rotSpeed = Random.Range(-10f, 10f) * 100;
            state = STATE.TURN;
        } else {
            rigid.velocity = base.ghostTr.forward * speed;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGhost : Ghost
{
    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();

        // Random speed range : 2.0 ~ 4.0 (player speed : 5.0)
        base.speed = Random.Range(2f, 4f);
        
        // Red Ghost must chase player
        base.chaseDistance = Mathf.Infinity;
    }

    private void Update()
    {
        base.Act();
    }

    protected override void Patrol()
    {
        // RedGhost do not patrol
        return;
    }
}

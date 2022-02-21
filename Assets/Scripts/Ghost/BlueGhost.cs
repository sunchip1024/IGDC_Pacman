using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGhost : Ghost
{
    // Start is called before the first frame update
    private void Start()
    {
        speed = Random.Range(4f, 6f);
        chaseDistance = 0;

        handler.addCommand(Command.COMMAND.FORWARD, 0.9f, Random.Range(5.0f, 10.0f));

        // rotation range : -180 ~ -15 / 15 ~ 180 (unit : degree)
        float randomRot = Random.Range(15f, 180f) * (Random.Range(0, 1) == 0 ? 1 : -1);
        handler.addCommand(Command.COMMAND.ROTATE, 0.1f, randomRot);
    }
}

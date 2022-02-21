using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandHandler
{
    private Rigidbody rigid;
    private Timer timer;
    private Queue<Command> commands;
    private Command nowCommand;

    public CommandHandler(Rigidbody rigid)
    {
        this.rigid = rigid;
        timer = rigid.gameObject.AddComponent<Timer>();
        commands = new Queue<Command>();
    }

    public void addCommand(Command.COMMAND cmd, float time, float unit) {
        Command command = null;
        switch(cmd) {
            case Command.COMMAND.FORWARD:
                command = new Forward(rigid, time, unit);
                break;

            case Command.COMMAND.ROTATE:
                command = new Rotate(rigid, time, unit);
                break;

            case Command.COMMAND.STOP:
                command = new Stop(rigid, time);
                break;
        }
        commands.Enqueue(command); 
        //Debug.LogFormat("[ {0} ] Add Command : {1} (Size : {2})", rigid.name, cmd, commands.Count);
    }
    public void emptyCommand() { commands.Clear(); }
    public Command.COMMAND GetNowCommand()
    {
        switch(nowCommand.GetType().Name)
        {
            case "Forward":
                return Command.COMMAND.FORWARD;

            case "Rotate":
                return Command.COMMAND.ROTATE;

            case "Stop":
                return Command.COMMAND.STOP;

            default:
                return Command.COMMAND.NULL;
        }
    }

    public void Next()
    {
        if (commands.Count == 0) return;

        if (nowCommand != null) commands.Enqueue(nowCommand);
        //Debug.LogFormat("[ {0} ] Enqueue command : {1}", rigid.name, nowCommand);

        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        
        nowCommand = commands.Dequeue();
        Debug.LogFormat("[ {0} ] now command : {1} (time : {2}, unit : {3})", rigid.name, nowCommand, nowCommand.GetTime(), nowCommand.GetUnit());
    }

    public void Act()
    {
        if (!timer.isTimerEnd()) { nowCommand.execute(); return; }

        timer.stopTimer();
        Next();
        timer.startTimer(nowCommand.GetTime());
    }
}

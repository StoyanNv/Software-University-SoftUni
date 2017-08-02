using System;

public class State
{
    private Lights state;

    public State(Lights state)
    {
        this.state = state;
    }
    public void ChangeState()
    {
        this.state = (Lights)(((int)this.state + 1) % Enum.GetNames(typeof(Lights)).Length);
    }

    public override string ToString()
    {
        return this.state.ToString();
    }
}
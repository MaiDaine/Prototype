using System;

[Serializable]
public class NPCStateReference
{
    public bool useConstant = true;
    public NPCStateVariable.State constantValue;
    public NPCStateVariable variable;

    public NPCStateReference() { }

    public NPCStateReference(NPCStateVariable.State value)
    {
        useConstant = true;
        constantValue = value;
    }

    public NPCStateVariable.State Value
    {
        get { return useConstant ? constantValue : variable.currentState; }
    }

    public static implicit operator NPCStateVariable.State(NPCStateReference reference)
    {
        return reference.Value;
    }
}
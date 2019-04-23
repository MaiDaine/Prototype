using System;

[Serializable]
public class BoardStateReference
{
    public bool useConstant = true;
    public BoardStateVariable.State constantValue;
    public BoardStateVariable variable;

    public BoardStateReference() { }

    public BoardStateReference(BoardStateVariable.State value)
    {
        useConstant = true;
        constantValue = value;
    }

    public BoardStateVariable.State Value
    {
        get { return useConstant ? constantValue : variable.currentState; }
    }

    public static implicit operator BoardStateVariable.State(BoardStateReference reference)
    {
        return reference.Value;
    }
}
using Godot;
using System;

public partial class Interactable : Area2D
{
    [Signal]
    public delegate void OnInteractSceneEventHandler();

    public override void _InputEvent(Viewport viewport, InputEvent @event, int shapeIdx)
    {
        if (!@event.IsActionPressed("interact"))
        {
            return;
        }

        Interact();
    }

    public virtual void Interact()
    {
        EmitSignal(SignalName.OnInteractScene);
    }
}

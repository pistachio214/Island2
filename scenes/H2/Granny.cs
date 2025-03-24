using Godot;
using System;

public partial class Granny : Area2D
{

	private Resource grannyDialogueResource = GD.Load<Resource>("res://addons/dialogue_text/h2-1.dialogue");

	public override void _InputEvent(Viewport viewport, InputEvent @event, int shapeIdx)
	{
		if (!@event.IsActionPressed("interact"))
		{
			return;
		}

		GrannySayDialogue();
	}

	private void GrannySayDialogue()
	{
		DiaManager.Instance.EmitSignal(DiaManager.SignalName.OnGrannyDialogue, grannyDialogueResource);
	}
}

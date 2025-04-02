using Godot;
using System;

public partial class Granny : Area2D
{

	private Resource grannyDialogueResource = GD.Load<Resource>("res://addons/dialogue_text/h2-1.dialogue");
	private Resource grannyMailDialogueResource = GD.Load<Resource>("res://addons/dialogue_text/h2-2.dialogue");

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
		string flag = "mail_acceped";
		Item item = Game.Inventory.AcctiveItem;
		if (item != null)
		{
			if (item == GD.Load<Item>("res://items/mail.tres"))
			{
				Game.Flags.Add(flag);
				Game.Inventory.RemoveItem(item);
			}
			else
			{
				return;
			}
		}

		Resource sayResource = grannyDialogueResource;
		if (Game.Flags.Has(flag))
		{
			sayResource = grannyMailDialogueResource;
		}

		DialogueBus.Instance.EmitSignal(DialogueBus.SignalName.OnGrannyDialogue, sayResource);
	}
}

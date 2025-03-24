using DialogueManagerRuntime;
using Godot;
using System;

public partial class H2 : Scene
{

	public override void _Ready()
	{
		base._Ready();

		DiaManager.Instance.Connect(DiaManager.SignalName.OnGrannyDialogue, Callable.From<Resource>(OnGrannyDialogueAction));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnGrannyDialogueAction(Resource resource)
	{
		DialogueManager.ShowDialogueBalloon(resource);
	}
}

using DialogueManagerRuntime;
using Godot;
using System;

public partial class H2 : Scene
{

	public override void _Ready()
	{
		base._Ready();

		DialogueBus.Instance.Connect(DialogueBus.SignalName.OnGrannyDialogue, Callable.From<Resource>(OnGrannyDialogueAction));
	}

	public override void _Process(double delta)
	{
	}

	private void OnGrannyDialogueAction(Resource resource)
	{
		DialogueManager.ShowDialogueBalloon(resource);
	}
}

using Godot;
using System;

public partial class Teleporter : Interactable
{
    
    [Export(hint: PropertyHint.File, hintString: "*.tscn")]
    public string targetPath;

    public override void Interact()
    {
        base.Interact();

        SceneChange.Instance.ChangeScene(targetPath);
    }
}

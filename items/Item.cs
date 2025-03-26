using Godot;
using System;

public partial class Item : Resource
{
    [Export]
    public string description;

    [Export]
    public Texture propTexture;

    [Export]
    public Texture sceneTexture;
}

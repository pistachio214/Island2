using Godot;
using System;

[GlobalClass] // 让Godot 识别此类为可导出类型
public partial class Item : Resource
{
    [Export]
    public string Description { get; set; }

    [Export]
    public Texture2D PropTexture { get; set; }

    [Export]
    public Texture2D SceneTexture { get; set; }
}

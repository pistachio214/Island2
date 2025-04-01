using Godot;
using System;

[Tool] // 启用工具模式
[GlobalClass]
public partial class Interactable : Area2D
{
    [Signal]
    public delegate void OnInteractSceneEventHandler();

    [Export]
    public bool allowItem = false;

    private Texture2D _texture; // 私有字段存储实际值
    [Export]
    public Texture2D Texture
    {
        get => _texture;
        set => SetTexture(value);
    }

    public void SetTexture(Texture2D value)
    {
        _texture = value;

        foreach (Node node in GetChildren())
        {
            // 说明不是用编辑器添加的，就释放掉
            if (node.Owner == null)
            {
                node.QueueFree();
            }
        }

        if (_texture == null)
        {
            return;
        }


        Sprite2D sprite = new()
        {
            Texture = value
        };

        AddChild(sprite);

        RectangleShape2D rectangle = new()
        {
            Size = value.GetSize()
        };

        CollisionShape2D collision = new()
        {
            Shape = rectangle
        };

        AddChild(collision);
    }

    public override void _Ready()
    {
    }

    public override void _InputEvent(Viewport viewport, InputEvent @event, int shapeIdx)
    {
        if (!@event.IsActionPressed("interact")) return;

        if (allowItem && Game.Inventory.AcctiveItem != null) return;

        Interact();
    }

    public virtual void Interact()
    {
        EmitSignal(SignalName.OnInteractScene);
    }
}

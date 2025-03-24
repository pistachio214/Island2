using Godot;
using System;

public partial class Scene : Node2D
{
    public override void _Ready()
    {
        Tween tween = CreateTween();

        tween.SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Sine);
        tween.TweenProperty(this, "scale", Vector2.One, 0.3).From(Vector2.One * (float)1.05);
    }

    public override void _Process(double delta)
    {
    }
}

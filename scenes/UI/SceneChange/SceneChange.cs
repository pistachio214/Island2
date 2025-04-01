using Godot;
using System;

public partial class SceneChange : CanvasLayer
{
    public static SceneChange Instance { get; private set; }

    private ColorRect colorRect;

    public override void _Ready()
    {
        // 初始化单例
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			QueueFree(); // 防止重复创建
		}
        
        colorRect = GetNode<ColorRect>("ColorRect");
    }

    public void ChangeScene(string path)
    {
        Tween tween = CreateTween();

        tween.TweenCallback(Callable.From(colorRect.Show));
        tween.TweenProperty(colorRect, "color:a", 1.0, 0.2);
        tween.TweenCallback(Callable.From(() => GetTree().ChangeSceneToFile(path)));
        tween.TweenProperty(colorRect, "color:a", 0.0, 0.3);
        tween.TweenCallback(Callable.From(colorRect.Hide));
    }
}

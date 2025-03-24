using Godot;
using System;

public partial class Game : Node
{
	public static Game Instance { get; private set; }

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
	}
}

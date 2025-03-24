using Godot;
using System;

public partial class DiaManager : Node
{
	public static DiaManager Instance { get; private set; }

	[Signal]
	public delegate void OnGrannyDialogueEventHandler(Resource resource); // 老奶奶对话信号

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

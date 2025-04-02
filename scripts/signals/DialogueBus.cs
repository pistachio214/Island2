using Godot;
using System;

/**
 * 单例实现对话事件总线
 */
public partial class DialogueBus : Node
{
	private static DialogueBus _instance;
	public static DialogueBus Instance => _instance ?? throw new Exception("DialogueBus 未初始化！");

	[Signal]
	public delegate void OnGrannyDialogueEventHandler(Resource resource); // 老奶奶对话信号

	public override void _EnterTree()
	{
		if (_instance != null)
		{
			QueueFree();
			return;
		}
		_instance = this;
		GD.Print("DialogueBus 初始化完成");
	}

	public override void _ExitTree()
	{
		if (_instance == this)
			_instance = null;
	}
}

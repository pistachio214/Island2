using Godot;
using System;

/**
 * 单例实现事件总线
 */
public partial class SignalBus : Node
{
	// 单例访问
	private static SignalBus _instance;
	public static SignalBus Instance => _instance ?? throw new Exception("SignalBus 未初始化！");

	public override void _EnterTree()
	{
		if (_instance != null)
		{
			QueueFree();
			return;
		}
		_instance = this;
		GD.Print("SignalBus 初始化完成");
	}

	public override void _ExitTree()
	{
		if (_instance == this)
			_instance = null;
	}
}

using Godot;
using System;

/**
 * 单例实现背包事件总线
 */
public partial class InventoryBus : Node
{
	[Signal]
	public delegate void OnInteractSceneEventHandler();

	// 单例访问
	private static InventoryBus _instance;
	public static InventoryBus Instance => _instance ?? throw new Exception("InventoryBus 未初始化！");

	public override void _EnterTree()
	{
		if (_instance != null)
		{
			QueueFree();
			return;
		}
		_instance = this;
		GD.Print("InventoryBus 初始化完成");
	}

	public override void _ExitTree()
	{
		if (_instance == this)
			_instance = null;
	}
}

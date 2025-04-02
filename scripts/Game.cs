using Godot;
using System;
using System.Collections.Generic;

public partial class Game : Node
{
	// 公共静态属性，暴露 Flags 实例
	public static Flags Flags { get; } = new Flags();

	// 公共静态属性，暴露 Inventory 实例
	public static Inventory Inventory { get; } = new Inventory();

	// 单例访问
	private static Game _instance;
	public static Game Instance => _instance;

	public override void _EnterTree()
	{
		// 确保单例唯一性
		if (_instance != null)
			QueueFree();
		else
			_instance = this;
	}
}

/**
 * 背包(库存)类
 */
public partial class Inventory : Node
{
	[Signal]
	public delegate void OnInventoryChangedEventHandler();

	public Item AcctiveItem = null; // 激活的道具

	private readonly List<Item> _items = [];

	private dynamic _currentItemIndex = -1; // 当前道具在库存中的索引号(默认-1即没有) 

	public int GetItemCount()
	{
		return _items.Count;
	}

	public Item GetCurrentItem()
	{
		if (_currentItemIndex == -1) return null;

		return _items[_currentItemIndex];
	}

	public void AddItem(Item item)
	{
		if (_items.Contains(item)) return;

		_items.Add(item);

		// 将当前道具设置为最新添加的道具
		_currentItemIndex = _items.Count - 1;

		EmitSignal(SignalName.OnInventoryChanged, false);
	}

	public void RemoveItem(Item item)
	{
		// 尝试移除项目，如果成功返回true，否则false
		bool removed = _items.Remove(item);

		if (!removed) return; // 如果没找到要移除的项目，直接返回

		// 检查当前索引是否越界
		if (_currentItemIndex >= _items.Count)
		{
			_currentItemIndex = _items.Count > 0 ? 0 : -1;
		}

		EmitSignal(SignalName.OnInventoryChanged, false);
	}

	// 下一个道具
	public void SelectNext()
	{
		if (_currentItemIndex == -1) return;

		_currentItemIndex = (_currentItemIndex + 1) % _items.Count;

		EmitSignal(SignalName.OnInventoryChanged, false);
	}

	// 上一个道具
	public void SelectPrev()
	{
		if (_currentItemIndex == -1) return;

		_currentItemIndex = (_currentItemIndex - 1 + _items.Count) % _items.Count;

		EmitSignal(SignalName.OnInventoryChanged, false);
	}
}

/**
 * 状态类
 */
public partial class Flags : Node
{
	[Signal]
	public delegate void OnFlagsChangedEventHandler();

	private readonly List<string> _flags = [];

	public bool Has(string flag)
	{

		return _flags.Contains(flag);
	}

	public void Add(string flag)
	{
		if (_flags.Contains(flag)) return;

		_flags.Add(flag);

		EmitSignal(SignalName.OnFlagsChanged);
	}
}
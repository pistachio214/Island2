using Godot;
using System;

[Tool]
[GlobalClass]
public partial class SceneItem : Interactable
{
	private Resource _currentItem;

	[Export(PropertyHint.ResourceType, "Item")]
	private Resource ItemResource
	{
		get => _currentItem;
		set => SetCurrentItem(value); // 强制转换并触发逻辑
	}

	// 类型安全的属性访问器
	public Item CurrentItem => _currentItem as Item;

	private void SetCurrentItem(Resource value)
	{
		// 类型检查
		if (value != null && value is not Item)
		{
			GD.PushError($"类型错误！需要Item类型，但得到 {value.GetType().Name}");
			return;
		}

		// 更新资源引用
		_currentItem = value;

		// 编辑器模式和运行时都执行
		if (CurrentItem != null)
		{
			SetTexture(CurrentItem.SceneTexture);

			// 通知编辑器更新属性列表
			if (Engine.IsEditorHint())
			{
				NotifyPropertyListChanged();
			}
		}
	}

	// private void SetCurrentItem(Item value)
	// {
	// 	if (_currentItem == value)
	// 	{
	// 		return;
	// 	}

	// 	_currentItem = value;


	// 	SetTexture(_currentItem != null ? _currentItem.SceneTexture : null);

	// 	NotifyPropertyListChanged(); // 手动要求检查器修改全部的属性

	// }

	public override void Interact()
	{
		base.Interact();

		QueueFree();
	}
}

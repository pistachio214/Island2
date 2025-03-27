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
		if (value == null) return;

		// 更新资源引用
		_currentItem = value;

		Item item = value as Item;

		SetTexture(item.SceneTexture);
		NotifyPropertyListChanged(); // 手动要求检查器修改全部的属性
	}

	public override void Interact()
	{
		base.Interact();

		Sprite2D sprite = new()
		{
			Texture = CurrentItem.SceneTexture
		};

		GetParent().AddChild(sprite);
		sprite.GlobalPosition = GlobalPosition;

		Tween tween = sprite.CreateTween();
		tween.SetEase(Tween.EaseType.In).SetTrans(Tween.TransitionType.Back);
		tween.TweenProperty(sprite, "scale", Vector2.Zero, 0.15);
		tween.TweenCallback(Callable.From(sprite.QueueFree));

		QueueFree();
	}
}

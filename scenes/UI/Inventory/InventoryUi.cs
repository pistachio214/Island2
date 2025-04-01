using Godot;
using System;

public partial class InventoryUi : VBoxContainer
{
	private Label _label;

	private Timer _labelTimer;

	private TextureButton _prevButton;

	private TextureButton _nextButton;

	private Sprite2D _propSprite;

	private Sprite2D _handSprite;

	private Tween _handOutroTween = null;

	private Tween _labelOutroTween = null;

	public override void _Ready()
	{
		InitialArgs();

		Game.Inventory.Connect(Inventory.SignalName.OnInventoryChanged, Callable.From<bool>(UpdateUi)); // 链接背包的修改信号

		UpdateUi(true);
	}

	// 集中初始化组件变量
	private void InitialArgs()
	{
		_label = GetNode<Label>("Label");
		_labelTimer = _label.GetNode<Timer>("Timer");

		BoxContainer itemBarContainer = GetNode<BoxContainer>("ItemBar");

		_prevButton = itemBarContainer.GetNode<TextureButton>("Prev");
		_nextButton = itemBarContainer.GetNode<TextureButton>("Next");
		_propSprite = itemBarContainer.GetNode<Sprite2D>("Use/Prop");
		_handSprite = itemBarContainer.GetNode<Sprite2D>("Use/Hand");

		_handSprite.Hide();
		// 就是设置 modulate.a = 0.0
		_handSprite.Modulate = new Color(_handSprite.Modulate.R, _handSprite.Modulate.G, _handSprite.Modulate.B, 0.0f);

		_label.Hide();
		_label.Modulate = new Color(_label.Modulate.R, _label.Modulate.G, _label.Modulate.B, 0.0f);
	}

	public override void _Process(double delta)
	{

	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("interact") && Game.Inventory.AcctiveItem != null)
		{
			Game.Inventory.SetDeferred("AcctiveItem", Variant.From<Item>(null));
			_handOutroTween = CreateTween();
			_handOutroTween.SetEase(Tween.EaseType.InOut).SetTrans(Tween.TransitionType.Sine).SetParallel();
			_handOutroTween.TweenProperty(_handSprite, "scale", Vector2.One * 3, 0.15);
			_handOutroTween.TweenProperty(_handSprite, "modulate:a", 0.0, 0.15);

			// 动画执行完之后才执行
			_handOutroTween.Chain().TweenCallback(Callable.From(_handSprite.Hide));
		}
	}

	private void UpdateUi(bool isInit = false)
	{
		int count = Game.Inventory.GetItemCount();

		_prevButton.Disabled = count < 2;
		_nextButton.Disabled = count < 2;
		Visible = count > 0;

		Item item = Game.Inventory.GetCurrentItem();
		if (item == null) return;

		_label.Text = item.Description;
		_propSprite.Texture = item.PropTexture;

		if (isInit) return;

		Tween tween = CreateTween();
		tween.SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Back);
		tween.TweenProperty(_propSprite, "scale", Vector2.One, 0.15).From(Vector2.Zero);

		ShowLabel();
	}

	private void ShowLabel()
	{
		if (_labelOutroTween != null && _labelOutroTween.IsValid())
		{
			_labelOutroTween.Kill();
			_labelOutroTween = null;
		}

		_label.Show();

		Tween tween = CreateTween();
		tween.SetEase(Tween.EaseType.InOut).SetTrans(Tween.TransitionType.Sine);
		tween.TweenProperty(_label, "modulate:a", 1.0, 0.2);
		tween.TweenCallback(Callable.From(() => _labelTimer.Start()));
	}

	private void OnPrevPressed()
	{
		Game.Inventory.SelectPrev();
	}

	private void OnNextPressed()
	{
		Game.Inventory.SelectNext();
	}

	private void OnUsePressed()
	{
		Game.Inventory.AcctiveItem = Game.Inventory.GetCurrentItem();
		if (_handOutroTween != null && _handOutroTween.IsValid())
		{
			_handOutroTween.Kill();
			_handOutroTween = null;

		}
		_handSprite.Show();

		Tween tween = CreateTween();
		tween.SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Back).SetParallel();
		tween.TweenProperty(_handSprite, "scale", Vector2.One, 0.15).From(Vector2.Zero);
		tween.TweenProperty(_handSprite, "modulate:a", 1.0, 0.15);

		ShowLabel();
	}

	private void OnLabelTimerTimeout()
	{
		_labelOutroTween = CreateTween();
		_labelOutroTween.SetEase(Tween.EaseType.InOut).SetTrans(Tween.TransitionType.Sine);
		_labelOutroTween.TweenProperty(_label, "modulate:a", 0.0, 0.2);
		_labelOutroTween.TweenCallback(Callable.From(_label.Hide));
	}
}

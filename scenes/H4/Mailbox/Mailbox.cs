using Godot;
using System;

public partial class Mailbox : FlagSwitch
{
	public override void _Ready()
	{
		base._Ready();

		InventoryBus.Instance.Connect(InventoryBus.SignalName.OnInteractScene, Callable.From(OnInteractScene));
	}

	public override void _Process(double delta)
	{
	}

	private void OnInteractScene()
	{
		Item item = Game.Inventory.AcctiveItem;

		if (item == null || item != GD.Load<Item>("res://items/key.tres")) return;

		Game.Flags.Add(flag);

		Game.Inventory.RemoveItem(item);
	}
}

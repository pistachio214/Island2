using Godot;
using System;

[GlobalClass]
public partial class FlagSwitch : Node2D
{
	[Export]
	public string flag;

	// flag 不存在时,默认显示的节点
	public Node2D defaultNode;


	// flag 存在时,切换显示的节点
	public Node2D switchNode;

	public override void _Ready()
	{
		InitialArgs();

		Game.Flags.Connect(Flags.SignalName.OnFlagsChanged, Callable.From(UpdateNodes));

		UpdateNodes();
	}

	public override void _Process(double delta)
	{
	}

	private void InitialArgs()
	{
		int count = GetChildCount();
		if (count > 0)
		{
			defaultNode = GetChild<Node2D>(0);
		}

		if (count > 1)
		{
			switchNode = GetChild<Node2D>(1);
		}
	}

	private void UpdateNodes()
	{
		bool exists = Game.Flags.Has(flag);
		if (defaultNode != null)
		{
			defaultNode.Visible = !exists;
		}

		if (switchNode != null)
		{
			switchNode.Visible = exists;
		}
	}
}

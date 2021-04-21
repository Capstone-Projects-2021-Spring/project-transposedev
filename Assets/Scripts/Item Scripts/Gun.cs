using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : Item
{
	public abstract override bool Use();
	public abstract override bool HoldDown();
	public abstract override bool Release();
}

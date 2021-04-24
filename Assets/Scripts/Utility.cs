using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Utility : Item
{
    public abstract override bool Use();
    public abstract override bool HoldDown();
    public abstract override bool Release();
}

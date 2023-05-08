using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModifiableTurret : MonoBehaviour
{
    public abstract void UpgradeTurret();
    public abstract void DestroyTurret();
}

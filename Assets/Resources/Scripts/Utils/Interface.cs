using System.Collections;
using UnityEngine;

public interface IEnemyMovement
{
    void SetMovementSpeed();
}

public interface IUIGenericElement
{
    void ToggleUI(bool state);
}
using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlovesController : MonoBehaviour, ISelectHandler
{
    public Animator AnimatorGloves;

    public void OnSelectPressedAmountChanged(SelectPressedEventData eventData)
    {
        if (eventData.PressedAmount > 0)
        {
            AnimatorGloves.SetBool("Hand State", false);
        }
        else
        {
            AnimatorGloves.SetBool("Hand State", true);
        }
    }
}

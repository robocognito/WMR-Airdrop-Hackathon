using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorStateHandler : BaseStateHandler
{

    public CoverOpen coverOpen;
    public override void OnStateChange(bool newState)
    {
        if (newState) coverOpen.Open();
        else coverOpen.Close();
    }
}

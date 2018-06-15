using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luis_Handler : Singleton<Luis_Handler>
{
    /// <summary>
    /// Changes the color of the target GameObject by providing the name of the object
    /// and the name of the color
    /// </summary>
    public void ChangeObjectState(string targetName, string state)
    {
        GameObject foundTarget = FindTarget(targetName);
        if (foundTarget != null && !string.IsNullOrEmpty(state))
        {
            Debug.Log("Changing " + foundTarget.name + " : " + state);
            bool newState = true;
            switch (state)
            {
                case "open":
                case "start":
                case "on":
                case "true":
                    newState = true;
                    break;

                case "end":
                case "stop":
                case "close":
                case "shut":
                case "off":
                case "false":
                    newState = false;
                    break;
            }

            var stateHandler = foundTarget.GetComponent<BaseStateHandler>();
            if (stateHandler != null)
            {
                stateHandler.OnStateChange(newState);
            }
        }
        else
        {
            Debug.LogWarning("no target found!");
        }
    }

    private GameObject FindTarget(string targetName)
    {
        ModuleLight moduleLight;
        switch (targetName)
        {
            case "airlock":
            case "door":

                return LocationManager.Instance.Airlock;

            case "cupola":
            case "couple":

                moduleLight = LocationManager.Instance.Node3.GetComponent<ModuleLight>();
                return moduleLight.Cupola;

            case "light":
            case "lights":
                switch (LocationManager.Instance.CurrentLocation)
                {
                    case LocationManager.Locations.None:
                        break;

                    case LocationManager.Locations.Airlock:
                        moduleLight = LocationManager.Instance.Airlock.GetComponent<ModuleLight>();
                        return moduleLight.lights;

                    case LocationManager.Locations.Columbus:
                        moduleLight = LocationManager.Instance.Columbus.GetComponent<ModuleLight>();
                        return moduleLight.lights;

                    case LocationManager.Locations.JLP:
                        moduleLight = LocationManager.Instance.JLP.GetComponent<ModuleLight>();
                        return moduleLight.lights;

                    case LocationManager.Locations.JPM:
                        moduleLight = LocationManager.Instance.JPM.GetComponent<ModuleLight>();
                        return moduleLight.lights;

                    case LocationManager.Locations.Node1:
                        moduleLight = LocationManager.Instance.Node1.GetComponent<ModuleLight>();
                        return moduleLight.lights;

                    case LocationManager.Locations.Node2:
                        moduleLight = LocationManager.Instance.Node2.GetComponent<ModuleLight>();
                        return moduleLight.lights;

                    case LocationManager.Locations.Node3:
                        moduleLight = LocationManager.Instance.Node3.GetComponent<ModuleLight>();
                        return moduleLight.lights;

                    case LocationManager.Locations.PMM:
                        moduleLight = LocationManager.Instance.PMM.GetComponent<ModuleLight>();
                        return moduleLight.lights;

                    case LocationManager.Locations.US_Lab_Center:
                        moduleLight = LocationManager.Instance.US_Lab_Center.GetComponent<ModuleLight>();
                        return moduleLight.lights;
                }
                return null;


            case "this":
            case "it":
            case "that":
            default:
                return FocusManager.Instance.TryGetFocusedObject(FocusManager.Instance.GetGazePointerEventData());
        }
    }
}
using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : Singleton<LocationManager>
{
    public enum Locations
    {
        None = 0,
        Airlock,
        Columbus,
        JLP,
        JPM,
        Node1,
        Node2,
        Node3,
        PMM,
        US_Lab_Center
    };

    public Locations CurrentLocation = Locations.None;

    public GameObject Airlock;
    public GameObject Columbus;
    public GameObject JLP;
    public GameObject JPM;
    public GameObject Node1;
    public GameObject Node2;
    public GameObject Node3;
    public GameObject PMM;
    public GameObject US_Lab_Center;
    //public GameObject Cupola;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Airlock)
        {
            CurrentLocation = Locations.Airlock;
        }

        if (other.gameObject == Columbus)
        {
            CurrentLocation = Locations.Columbus;
        }

        if (other.gameObject == JLP)
        {
            CurrentLocation = Locations.JLP;
        }

        if (other.gameObject == JPM)
        {
            CurrentLocation = Locations.JPM;
        }

        if (other.gameObject == Node1)
        {
            CurrentLocation = Locations.Node1;
        }

        if (other.gameObject == Node2)
        {
            CurrentLocation = Locations.Node2;
        }

        if (other.gameObject == Node3)
        {
            CurrentLocation = Locations.Node3;
        }

        if (other.gameObject == PMM)
        {
            CurrentLocation = Locations.PMM;
        }

        if (other.gameObject == US_Lab_Center)
        {
            CurrentLocation = Locations.US_Lab_Center;
        }

        //if (other.gameObject == Cupola)
        //{
        //    CurrentLocation = Locations.Cupola;
        //}
    }
}

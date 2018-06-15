using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverOpen : MonoBehaviour {
    public enum coverState
    {
        Open,
        Closing,
        Closed,
        Opening
    }
    public class Cover
    {
        public Transform transform;
        public Quaternion closedRot;
        public Quaternion openRot;
    }
    private AudioSource audioSource;
    public Transform[] covers;
    private float openAmount = 0;
    private Cover[] _covers;
    public float openTime = 4;
    private coverState currentState;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        openAmount = 0;
        _covers = new Cover[covers.Length];
        currentState = coverState.Closed;
        for (int c =0; c< covers.Length; c++)
        {
            _covers[c] = new Cover();
            _covers[c].transform = covers[c];
            _covers[c].closedRot = covers[c].transform.rotation;
            _covers[c].transform.Rotate(172, 0, 0);
            _covers[c].openRot = covers[c].transform.rotation;
            _covers[c].transform.Rotate(-172, 0, 0);
        }
        Open();
	}
	
    public void Open()
    {
        currentState = coverState.Opening;
        audioSource.Play();
    }
    public void Close()
    {
        currentState = coverState.Closing;
        audioSource.Play();
    }
    void RotateCovers()
    {
        for (int c = 0; c < covers.Length; c++)
        {
            _covers[c].transform.rotation = Quaternion.Lerp(_covers[c].closedRot, _covers[c].openRot, openAmount);
        }
    }
	// Update is called once per frame
	void Update () {
        switch (currentState)
        {
            case (coverState.Open):

                break;
            case (coverState.Closing):
                openAmount -= Time.deltaTime / openTime;
                if (openAmount <= 0)
                {
                    openAmount = 0;
                    currentState = coverState.Closed;
                }
                RotateCovers();
                break;
            case (coverState.Closed):

                break;
            case (coverState.Opening):
                openAmount += Time.deltaTime / openTime;
                if (openAmount >= 1)
                {
                    openAmount = 1;
                    currentState = coverState.Open;
                }
                RotateCovers();
                break;
        }
	}
}

using System;
using UnityEngine;
using UniRx;

public class PinchEnd : MonoBehaviour
{
    public OVRHand Hand;
    [SerializeField] private Transform fingerTip;


 
    void Start()
    {
        var finger = OVRHand.HandFinger.Index; //- declaring finger
        bool IsFingerPinched () => Hand.GetFingerIsPinching(finger); //property to finger 

        var lastPinchStream = Observable.EveryUpdate().Select(_ => IsFingerPinched());

        var currentPinchStream = Observable.EveryUpdate().Select(_ => IsFingerPinched()).Skip(1);

        var pinchEnd = Observable.Zip(lastPinchStream, currentPinchStream).Where(p => p[0] && !p[1]);

        pinchEnd.Subscribe(_ =>
        {
            CreateObjectAtEnd();
        });
    }

    private void CreateObjectAtEnd(float size = 0.02f)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Cube);
        sphere.transform.position = fingerTip.position;
        sphere.transform.localScale = Vector3.one * size;
    }
}

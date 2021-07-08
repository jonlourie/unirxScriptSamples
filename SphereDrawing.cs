using System;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

public class SphereDrawing : MonoBehaviour
{

    [SerializeField]
    private OVRHand hand = default;

    [SerializeField]
    private Transform fingerTiplong = default;

    // Start is called before the first frame update
    private void Start()
    {
        bool IsIndexPinched() => hand.GetFingerIsPinching(OVRHand.HandFinger.Index); // I didnt know you could set a function like this or add things to a list via listnam[insert interger data].

        var lastPinchStream =
            Observable.EveryUpdate().Select(_ => IsIndexPinched());

        var currentPinchStream =
            Observable.EveryUpdate().Select(_ => (IsIndexPinched())).Skip(1);

        var pinchStartStream =
            Observable.Zip(lastPinchStream, currentPinchStream).Where(p => !p[0] && p[1]); // translation if its not the first last pinch but the first current pinch start the event

        var allowedDelay = 1.0f;

        var doublePinch = pinchStartStream.SelectMany(e => pinchStartStream.Buffer(TimeSpan.FromSeconds(allowedDelay), 1) //I got hung up on this put miliseconds instead 
            .Take(1)
            .Where(b => b.Count >= 1)); //here is where define the double pinch - im fairly certian you could do this right off the double pinch code in the lesson with less implementation not sure why its reapeat or take 1 vs take 2 need to look back on thsi


        //below the double pinch event leads us to subcribe to an observable event where while the isidex is pinched spheres are created at o.1 second rate
        doublePinch.Take(1).Repeat().Subscribe(_ =>
        {
            Observable
                .Interval(TimeSpan.FromSeconds(0.1))
                .TakeWhile(x => IsIndexPinched())
                .Subscribe(x =>
                {
                    CreateSphereObject();

                });



        }).AddTo(this);

    }

    private void CreateSphereObject(float size = 0.02f)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = fingerTiplong.transform.position;
        sphere.transform.localScale = Vector3.one * size;



    }

    
}

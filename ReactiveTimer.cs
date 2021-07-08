
using System;
using TMPro;
using UniRx;
using UnityEngine;

public class ReactiveTimer : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro counter = default;

    [SerializeField]
    private TextMeshPro message = default;

    public float animationSpeed;

    private void Awake()
    {
        message.alpha = 0.0f;
    }

    //public IntReactiveProperty timeSpan = new IntReactiveProperty(0);

    //public IntReactiveProperty Highscore = new IntReactiveProperty(0);


    //public void IncrementScore () 
    //{
    //   Highscore.Value++;
    //}

    private void Start()
    {
        //right now my fade anim on the timer message is tied to the clock seconds experiment later on
       
        var countSeconds = Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(x
            =>{


                counter.text = $"{x}";
                Observable.Timer(TimeSpan.FromMilliseconds(100)).Subscribe(_ =>
                {
                    message.alpha += animationSpeed;


                });

        });
    }

    //so i fiugured out how to animate a message to appear with the timer but its not set to dissapear nor is it tied to any specific
    //event in the stream other then the first event. Also there is no () => on complete tied to either of these overables I guess I could 
    //try and make the messages dissaper through that but thats for another day I think. As another solution to this problem I could experiment with making this code
    //more dynamic by converting counter or perhaps this class into a reactive variable and then playing around wiht it like that. Re-visit the video 
    // On reactive variables in the xrbootcamp tutporial guide. I wonder if I actually did it right the first time by adding a timeSpan.Value counter. cuase theoritically would that create 
    //events it would be tied to something else other then seconds. I could go back an make a new class for clock message tied to that. In the tutorial it was tied to a high score
    //I need it to be tied to nothing aactually - I guess maybe its best if its not tied to the clock unnless I want it to appear at certain time in the clock. Whatever ill return and experiment
    // more 
}

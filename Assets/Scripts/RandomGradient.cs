using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGradient : MonoBehaviour
{
    public Renderer Renderer;

    void Start()
    {
        // Create a new Gradient
        Gradient gradient = new Gradient();

        // Create a new array of GradientColorKey and assign random colors
        GradientColorKey[] colorKeys = new GradientColorKey[2];
        colorKeys[0].color = new Color(Random.value, Random.value, Random.value); // Random start color
        colorKeys[0].time = 0.0f;  // Start of the gradient
        colorKeys[1].color = new Color(Random.value, Random.value, Random.value); // Random end color
        colorKeys[1].time = 1.0f;  // End of the gradient

        // Create a new array of GradientAlphaKey (if you want random transparency as well)
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0].alpha = 1.0f; // Opaque at the start
        alphaKeys[0].time = 0.0f;
        alphaKeys[1].alpha = 1.0f; // Opaque at the end
        alphaKeys[1].time = 1.0f;

        // Assign keys to the gradient
        gradient.SetKeys(colorKeys, alphaKeys);

        // Randomly apply the gradient color over time
        float t = Mathf.PingPong(Time.time, 1f);
        Renderer.material.color = gradient.Evaluate(t);  // Get color based on 't'
    }
}

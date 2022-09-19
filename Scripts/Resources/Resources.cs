using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public bool scenarioIsOver;
    public List<Transform> resourceList = new List<Transform>();
    private AudioSource source;
    private bool toggle;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (resourceList.Count <= 0)
        {
            scenarioIsOver = true;
        }

        if(scenarioIsOver && !toggle)
        {
            if(!source.isPlaying)
                source.Play();
            toggle = true;
        }

    }
}

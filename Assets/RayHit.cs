using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayHit : MonoBehaviour
{
    public string tegn;
    // Start is called before the first frame update
    void Start()
    {
        if (tegn == null)
        {
            Debug.LogError("tegn er ikke blevet sat på: " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPointerEnter()
    {
        print("THE TEST WORKS: " + tegn);
    }
}

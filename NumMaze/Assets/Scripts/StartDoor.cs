using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDoor : MonoBehaviour
{
    public GameObject BegText;

    // Start is called before the first frame update
    void Start()
    {
        if (BegText == null) { Debug.LogError("Remeber to add BegText to:" + gameObject.name); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPointerEnter()
    {
        Debug.Log("RayTarget: " + gameObject.name + " was hit");
        BegText.SetActive(false);
        gameObject.SetActive(false);
    }
}

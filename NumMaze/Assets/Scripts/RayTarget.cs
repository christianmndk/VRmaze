using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class RayTarget : MonoBehaviour
{
    public string tegn;
    public OSCReceiver R;
    public GameObject CodeFoundText;
    private bool sent = false;

    // Start is called before the first frame update
    void Start()
    {
        if (tegn == "") { Debug.LogError("Tegn has not been set in: " + gameObject.name); }
        if (R == null) { Debug.LogError("Remeber to add OSCReciever to:" + gameObject.name); }
        if (CodeFoundText == null) { Debug.LogError("Remeber to add CodeFoundText to:" + gameObject.name); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPointerEnter()
    {
        if (sent) { return; }
        sent = true;
        Debug.Log("RayTarget: " + gameObject.name + " was hit sending: " + tegn);
        R.SendOCS("/re " + tegn);
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        CodeFoundText.SetActive(true);
        yield return new WaitForSeconds(5);
        CodeFoundText.SetActive(false);
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Net.Sockets;
using System.Net;
using System.Linq;
using UnityEngine.XR;

[AddComponentMenu("Scripts/OSCReceiver")]
public class OSCReceiver : MonoBehaviour
{
    public string remoteIp = "127.0.0.1";
    public int sendToPort = 6448;
    public int listenerPort = 12000;
    public GameObject controller; 
    public GameObject spotOSC;

    private Osc oscHandler;
    
    public GameObject messageCanvas;
    public Text messageText;
    private string Text = "";
    public GameObject messageCanvasNum;
    public Text messageTextNum;
    private string NumText = "";
    public GameObject messageCanvasWord;
    public Text messageTextWord;
    private string WordText = "";


    ~OSCReceiver()
    {
        if (oscHandler != null)
        {            
            oscHandler.Cancel();
        }

        // speed up finalization
        oscHandler = null;
        System.GC.Collect();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        messageText.text = Text;
        messageTextNum.text = NumText;
        messageTextWord.text = WordText;
        
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>

    void OnDisable()
    {
        // close OSC UDP socket
        Debug.Log("closing OSC UDP socket in OnDisable");
        oscHandler.Cancel();
        oscHandler = null;
    }
    //
    /// <summary>
    /// Start is called just before any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {

        UDPPacketIO udp = GetComponent<UDPPacketIO>();
        udp.init(remoteIp, sendToPort, listenerPort);
        
	    oscHandler = GetComponent<Osc>();
        oscHandler.init(udp);

        oscHandler.SetAddressHandler("/status", printStatus);
        oscHandler.SetAddressHandler("/remoteIP", setRemoteIP);        
        oscHandler.SetAddressHandler("/find", Find);
        oscHandler.SetAddressHandler("/text", textFromOSC);
        oscHandler.SetAddressHandler("/nu", CorrectNum);
        oscHandler.SetAddressHandler("/wo", CorrectWord);


        if (messageCanvas == null) {
            messageCanvas = GameObject.Find("OscMessageCanvas").gameObject;
        }
        if (messageCanvasNum == null) {
            messageCanvasNum = GameObject.Find("NumCanvas").gameObject;
        }
        if (messageCanvasWord == null)
        {
            messageCanvasWord = GameObject.Find("WordCanvas").gameObject;
        }

        messageText = GameObject.Find("MessageText").GetComponent<Text>();
        messageTextNum = GameObject.Find("MessageTextNum").GetComponent<Text>();
        messageTextWord = GameObject.Find("MessageTextWord").GetComponent<Text>();


        if (spotOSC == null) {
            spotOSC = GameObject.Find("SpotLight").gameObject;
        }
                 
        string localIP;
        using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
        {
            socket.Connect("8.8.8.8", 65530);
            IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
            localIP = "IP-adressen her er " + endPoint.Address.ToString();
            Debug.Log(localIP);
        }

        List<InputDevice> dimser = new List<InputDevice>();
        string dimseNavne = "";
        InputDevices.GetDevices(dimser);
        foreach (InputDevice d in dimser) {
            dimseNavne += "\n";
            dimseNavne += d.characteristics;
        }
        messageText.text = (localIP + dimseNavne);
        
    }
    private void printStatus(OscMessage m)
    {
        Debug.Log("Got status >> " + Osc.OscMessageToString(m));
    }


    public void setRemoteIP(OscMessage m) {
        Debug.Log("Called light from OSC >> " + Osc.OscMessageToString(m));
    }
    
    public void textFromOSC(OscMessage m)
    {
        Debug.Log("Called text from OSC > " + Osc.OscMessageToString(m));
        Text = (string)m.Values[0];
    }
    public void CorrectNum(OscMessage m)
    {
        Debug.Log("Called text from OSC > " + Osc.OscMessageToString(m));
        oscHandler.Send(Osc.StringToOscMessage("/test It doesnt even matter"));
        NumText = "JUBII NUM vIRKER";
    }
    public void CorrectWord(OscMessage m)
    {
        Debug.Log("Called text from OSC > " + Osc.OscMessageToString(m));
        messageTextWord.text = "JUBII WORD WIRKER";
    }

    public void Find(OscMessage m)
    {
        Debug.Log("Finding > " + m.Address);
        //Find()
    	Debug.Log("Called Example One > " + Osc.OscMessageToString(m));
    }

}
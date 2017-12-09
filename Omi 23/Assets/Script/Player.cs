using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    public SyncListInt state = new SyncListInt();
    public SyncListInt cType = new SyncListInt();
    public SyncListInt trumpValue = new SyncListInt();




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

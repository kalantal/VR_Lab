using UnityEngine;

public class DataBox : MonoBehaviour {

    private bool highTemp;
	public int boxID;				// ServerID #
	public int tempature;			
	public int currentStorage;
	public int maxStorage;
	public string status;
	private Color startcolor;		// place holder for box color
	private Renderer renderer;		// current box Color renderer

    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;

	public DataBox(int a, int b, string c, int cStorage, int mStorage){
		boxID = a;
        //tempature = b;
        tempature = r.Next(68, 75);
        status = c;
		currentStorage = cStorage;
		maxStorage = mStorage;
	}

	public DataBox(int a, int b, string c, bool ht){
		boxID = a;
        //tempature = b;
        tempature = r.Next(68, 75);
        status = c;
        highTemp = ht;
	}
	public int getTemp(){
		return tempature;
	}

	public int getBox(){
		return boxID;
	}

	public string getStatus(){
		return status;
	}

	public int getCurrStorage(){
		return currentStorage;
	}

	public int getMaxStorage(){
		return maxStorage;
	}

	public void setTemp(int a){
		tempature = a;
	}


	void Start(){
        // this gets the current render which is attached to the prefab
        trackedObject = GetComponent<SteamVR_TrackedObject>();
		renderer = gameObject.GetComponent <Renderer>();
	}

	// when cursor enters the obj space, turn prefab to specifc color based on tempature.
/*	void OnMouseEnter()
	{
		startcolor = renderer.material.color;
		if (tempature.CompareTo(96) == -1) 		// temp < 96
			renderer.material.color = Color.green;
		else if (tempature.CompareTo(96) == 1 && tempature.CompareTo(139) == -1) // 96 < temp < 139
			renderer.material.color = Color.yellow;
		else if (tempature.CompareTo(139)== 1)			// temp > 139
					renderer.material.color = Color.red;
		
	}

	// return box color back to original render when mouse leaves 
	void OnMouseExit()
	{
		renderer.material.color = startcolor;
	}

*/
	void Update(){

        UpdateTempature();

        device = SteamVR_Controller.Input((int)trackedObject.index);

		if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip)) {
            Debug.Log("hey we pressed it (the grip button!)");
			startcolor = renderer.material.color;
			if (tempature.CompareTo(72) == -1) 		// temp < 72
				renderer.material.color = Color.green;
			else if (tempature.CompareTo(72) == 1 && tempature.CompareTo(80) == -1) // 72 < temp < 80
				renderer.material.color = Color.yellow;
			else if (tempature.CompareTo(80)== 1)			// temp > 80
				renderer.material.color = Color.red;
		}
		//if (Input.GetKeyUp(KeyCode.Q))
		//renderer.material.color = startcolor;

		if (Input.GetKeyUp (KeyCode.E)) {
			
		}
	}
    static System.Random r = new System.Random();
    void UpdateTempature() {
        double rand = r.NextDouble();
        if (!highTemp) {
            if (rand > .95)
                setTemp(tempature + 1);
            else if (rand < .05)
                setTemp(tempature - 1);
        } else {
            if(rand > .75)
                setTemp(tempature + 1);
            else if (rand < .05)
                setTemp(tempature - 1);
        }
    }

}

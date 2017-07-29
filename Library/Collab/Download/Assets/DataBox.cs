using UnityEngine;

public class DataBox : MonoBehaviour {

	public int boxID;				// ServerID #
	public int temperature;			
	public int currentStorage;
	public int maxStorage;
	public string status;
	private Color startcolor;		// place holder for box color
	private Renderer renderer;		// current box Color renderer

    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;

	public DataBox(int a, int b, string c, int cStorage, int mStorage){
		boxID = a;
        //temperature = b;
        temperature = r.Next(60, 68);
        status = c;
		currentStorage = cStorage;
		maxStorage = mStorage;
	}

	public DataBox(int a, int b, string c, bool highTemp){
		boxID = a;
        //temperature = b;
        temperature = r.Next(60, 68);
        if (highTemp) temperature += 15;
        status = c;
	}
	public int getTemp(){
		return temperature;
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
		temperature = a;
	}


	void Start(){
        // this gets the current render which is attached to the prefab
        trackedObject = GetComponent<SteamVR_TrackedObject>();
		renderer = gameObject.GetComponent <Renderer>();
        startcolor = renderer.material.color;
    }

	// when cursor enters the obj space, turn prefab to specifc color based on temperature.
	public void setColor()
	{
		startcolor = renderer.material.color;
		if (temperature.CompareTo(60) == -1) 		// temp < 96
			renderer.material.color = Color.green;
		else if (temperature.CompareTo(60) == 1 && temperature.CompareTo(65) == -1) // 96 < temp < 139
			renderer.material.color = Color.yellow;
		else if (temperature.CompareTo(65)== 1)			// temp > 139
					renderer.material.color = Color.red;
		
	}

	// return box color back to original render when mouse leaves 
	public void resetColor()
	{
		renderer.material.color = startcolor;
	}


	void Update(){
        UpdateTempature();

       // device = SteamVR_Controller.Input((int)trackedObject.index);
	if (Input.GetKeyDown(KeyCode.Q) || GlobalVar.interfaces==3) {
            Debug.Log("hey we pressed it (the grip button!)");
			
			if (temperature.CompareTo(60) == -1) 		// temp < 72
				renderer.material.color = Color.green;
			else if (temperature.CompareTo(60) == 1 && temperature.CompareTo(65) == -1) // 72 < temp < 80
				renderer.material.color = Color.yellow;
			else if (temperature.CompareTo(65)== 1)			// temp > 80
				renderer.material.color = Color.red;
		}
       
	if (Input.GetKeyUp(KeyCode.Q) || GlobalVar.interfaces!=3)
		renderer.material.color = startcolor;

	}
    static System.Random r = new System.Random();
    void UpdateTempature() {
        double rand = r.NextDouble();
        if (rand > .95)
            setTemp(temperature + 1);
        else if (rand < .05)
            setTemp(temperature - 1);
    }

}

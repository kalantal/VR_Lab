using UnityEngine;

public class DataBox : MonoBehaviour {

    private bool highTemp;
	public int boxID;				// ServerID #
	public int tempature;			// Tempature value
	public int currentStorage;      // Current storage value
	public int maxStorage;          // Max value of storage
	public string status;           // Current status of the data box
	private Color startcolor;		// place holder for box color
	private Renderer renderer;		// current box Color renderer
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    private int updateCounter = 0;

    // this constructor adds storage value
    public DataBox(int a, int b, string c, int cStorage, int mStorage){
		boxID = a;
        // this reads in the tempature values from the text file
        //tempature = b;
        //
        tempature = r.Next(50, 63); // sets the tempature to randomized value between 65 - 75
        status = c;
		currentStorage = cStorage;
		maxStorage = mStorage;
	}
    // this is the current constructor currently being used
	public DataBox(int a, int b, string c, bool ht){
		boxID = a;
        // currently in the current build, tempature is being randomized and not set to b value
        //tempature = b;
        tempature = r.Next(50, 63); // sets the tempature to randomized value between 65 - 75
        status = c;
        highTemp = ht;
        if (highTemp) tempature += 20;
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
        startcolor = renderer.material.color;       // for changing the color back to original (white)
    }

	// return box color back to original render when mouse leaves 
	public void resetColor()
	{
		renderer.material.color = startcolor;
	}


	void Update(){
        if (updateCounter == 2)
        {
            renderer.material.color = Color.green;
            UpdateTempature(); // increment and decrement temp
            if (Input.GetKeyDown(KeyCode.Q) || GlobalVar.interfaces == 3)
            {   // gets input from the keyboard of vive controller
                Debug.Log("hey we pressed it (the grip button!)");
                //startcolor = renderer.material.color;
                if (tempature.CompareTo(65) == 1)
                    renderer.material.color = Color.red;
                else if (tempature.CompareTo(60) == -1)      // temp < 60
                    renderer.material.color = Color.green;
                else if (tempature.CompareTo(60) == 1 && tempature.CompareTo(65) == -1) // 60 < temp < 65
                    renderer.material.color = Color.yellow;
            }
            updateCounter = 0;
        }
        else updateCounter++;
           // changes color back to original start color
	    if (Input.GetKeyUp(KeyCode.Q) || GlobalVar.interfaces!=3)   
		    renderer.material.color = startcolor;

	}

    // updates tempature, this function is to simulate tempature incre/decre in a real data center
    static System.Random r = new System.Random();
    void UpdateTempature() {
        double rand = r.NextDouble();
        if (tempature > 65) return;
        if (rand > .99)
        {
            if (tempature > 63) //if 64, can't 65
                return;
            setTemp(tempature + 1);
        }
        else if (rand < .01)
        {
            setTemp(tempature - 1);
        }
    }

}

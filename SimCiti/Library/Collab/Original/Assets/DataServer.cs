using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;
public class DataServer : MonoBehaviour{

	// Describes whether the simulation is in the "high temperature" scenario or not
	private bool highTemp = false;

	public GameObject myCube;		// prefab for a server box
	public Vector3 spawnSpot;		// origin coord of first server box
	private DataBox[] box = new DataBox[200];			// information on all servers
	private int numberOfServers;    // used to keep track amt of servers in the scene
	public string file;				// name of file to be read
	// for text modification
	private Transform child;		
	private Text t;
	// end for text obj
	private Transform[] canvasChild = new Transform[5];     // array of canvas memory location
	private Texture[] networkImages = new Texture[6];       // array of memory location for the network canvas
	private RawImage imgTmp;  // each data box has a network I/O image
	void Awake(){
		// allows for faster obj intiation during runtime by storing memory location of the Canvas location
        // will later reference these location in the program.
		canvasChild[0] = myCube.transform.Find("Canvas/Panel/ID/Text");
		canvasChild[1] = myCube.transform.Find("Canvas/Panel/Tempature");
		canvasChild[2] = myCube.transform.Find("Canvas/Panel/Storage");
		canvasChild[3] = myCube.transform.Find("Canvas/Panel/Status");
		canvasChild[4] = myCube.transform.Find("NetworkCanvas/RawImage");
		//store network images location
		networkImages[0] = Resources.Load("1") as Texture;
		networkImages[1] = Resources.Load("2") as Texture;
		networkImages[2] = Resources.Load("3") as Texture;
		networkImages[3] = Resources.Load("4") as Texture;
		networkImages[4] = Resources.Load("5") as Texture;
		networkImages[5] = Resources.Load("6") as Texture;

		Load(file);     // loads the file and inputs the information to the DataBox array.
	}

	void Start(){
		// spawn server box
		Vector3 spawnTemp = spawnSpot;
		int perRow = 14;    // 14 boxes per row
		int numRow = numberOfServers / perRow;  // calculates the number of rows
		int i = 0;  // for box array indexing
		System.Random rnd = new System.Random();
		// read from array and create the dataBox
        // starts on row j, and moves each box to the left 0.86 x units.
		for (int j = 0; j < numRow + 1; j++) {
			for (int k = 0; k < perRow-1; k++) {
				GameObject clone = myCube;      // this is the dataobj and is made a clone
                // read from the box array to get information
                clone.GetComponent<DataBox> ().tempature = box [i].getTemp ();  
				clone.GetComponent<DataBox> ().boxID = box [i].getBox ();
				clone.GetComponent<DataBox> ().status = box [i].getStatus ();
				clone.GetComponent<DataBox> ().currentStorage = box[i].getCurrStorage();
				clone.GetComponent<DataBox> ().maxStorage = box [i].getMaxStorage();

				// tag
				//clone.tag = "data box";

				//this is setting the data to the "Canvas" overlay GUI
				child =  canvasChild[0];    // gets address location of the canvas
				t = child.GetComponent<Text>(); // makes t the component of the text object
				t.text = "BoxID: " + box [i].getBox ().ToString();  // sets the boxID as a string in this canvas

				child =  canvasChild[1];
				t = child.GetComponent<Text>();
				t.text = "Temp " + box [i].getTemp ().ToString() + " C";
                // this part sets the storage units, but will be added in the future
				/*child =  canvasChild[2];
				t = child.GetComponent<Text>();
				t.text = box [i].getCurrStorage().ToString() + "/" + box [i].getMaxStorage().ToString();
				*/
				child =  canvasChild[3];
				t = child.GetComponent<Text>();
				t.text = box [i].getStatus ();
				// end of GUI header

                // Start of network canvas overlay
				child = canvasChild [4];    // gets the memory location of the network canvas
				imgTmp = child.GetComponent<RawImage>();    // network canvas has a Raw image component, gets memory location
				imgTmp.texture = networkImages [rnd.Next(6)];   // add a random image from the networkImages array as a texture
                // End of network canvas overlay

				clone = (GameObject)Instantiate (myCube, spawnTemp, transform.rotation);    // this creates the obj using the spawn point. and orientation of the cube.
				spawnTemp.x = spawnTemp.x + (float).86;     // after spawn, moves the spawn point 0.86 units to the left.
				i++;    // increment the box [] index
			}
            // this parts provides two sided racks
			if (j % 2 == 0) {   
				transform.rotation = Quaternion.Euler (-90, 0, 180);
				spawnSpot.z = spawnSpot.z + 2;
				spawnTemp = spawnSpot;
			} 
			else { 
				transform.rotation = Quaternion.Euler (-90, 0, 0);
				spawnSpot.z = spawnSpot.z + (float).65;
				spawnTemp = spawnSpot;
			}

				
		}
	}



	public bool Load(string fileName)
	{
		// Handle any problems that might arise when reading the text
		try
		{
			string line;
			// Create a new StreamReader, tell it which file to read and what encoding the file
			StreamReader theReader = new StreamReader(fileName, Encoding.Default);
			int i = 0;
			using (theReader)
			{
				// While there's lines left in the text file, do this:
				do
				{
					line = theReader.ReadLine();

					if (line != null)
					{
						string[] entries = line.Split(',');
						if (entries.Length > 0)
						{
							// store data into array
							box[i] = new DataBox(Convert.ToInt32(entries[0]),
								Convert.ToInt32(entries[1]),
								entries[2],
								highTemp);
                            // the constructor below is used to read in storage (future implementation)
							/*box[i] = new DataBox(Convert.ToInt32(entries[0]),Convert.ToInt32(entries[1]),entries[2],
								Convert.ToInt32(entries[3]),
								Convert.ToInt32(entries[4]));
							*/
							i++;

						}

					}
				}
				while (line != null);
				// Done reading   
				theReader.Close();
				numberOfServers = i;
				return true;
			}
		}
		// If anything broke in the try block, we throw an exception with information
		// on what didn't work
		catch (Exception e)
		{
			Console.WriteLine("{0}\n", e.Message);
			return false;
		}
	}





}
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;
public class DataServer : MonoBehaviour{

	public GameObject myCube;		// prefab for a server box
	public Vector3 spawnSpot;		// origin coord of first server box
	private DataBox[] box = new DataBox[200];			// information on all servers
	private int numberOfServers;
	public string file;				// name of file to be read
	// for text modification
	private Transform child;		
	private Text t;
	// end for text obj
	private Transform[] canvasChild = new Transform[5];
	private Texture[] networkImages = new Texture[6];
	private RawImage imgTmp;
	void Awake(){
		// allows for faster obj intiation during runtime
		canvasChild[0] = myCube.transform.Find("Canvas/Panel/ID/Text");
		canvasChild[1] = myCube.transform.Find("Canvas/Panel/Tempature");
		canvasChild[2] = myCube.transform.Find("Canvas/Panel/Storage");
		canvasChild[3] = myCube.transform.Find("Canvas/Panel/Status");
		canvasChild[4] = myCube.transform.Find("NetworkCanvas/RawImage");
		//store network images
		networkImages[0] = Resources.Load("1") as Texture;
		networkImages[1] = Resources.Load("2") as Texture;
		networkImages[2] = Resources.Load("3") as Texture;
		networkImages[3] = Resources.Load("4") as Texture;
		networkImages[4] = Resources.Load("5") as Texture;
		networkImages[5] = Resources.Load("6") as Texture;

		Load(file);
	}

	void Start(){
		// spawn server box


		Vector3 spawnTemp = spawnSpot;
		int perRow = 14;
		int numRow = numberOfServers / perRow;
		int i = 0;
		System.Random rnd = new System.Random();
		// read from array and create the dataBox
		for (int j = 0; j < numRow + 1; j++) {
			for (int k = 0; k < perRow-1; k++) {
				GameObject clone = myCube;
				clone.GetComponent<DataBox> ().temperature = box [i].getTemp ();
				clone.GetComponent<DataBox> ().boxID = box [i].getBox ();
				clone.GetComponent<DataBox> ().status = box [i].getStatus ();
				clone.GetComponent<DataBox> ().currentStorage = box[i].getCurrStorage();
				clone.GetComponent<DataBox> ().maxStorage = box [i].getMaxStorage();

				// tag
				clone.tag = "data box";

				//this part adds the box ID # to the GUI
				child =  canvasChild[0];
				t = child.GetComponent<Text>();
				t.text = "BoxID: " + box [i].getBox ().ToString();

				child =  canvasChild[1];
				t = child.GetComponent<Text>();
				t.text = "Temp " + box [i].getTemp ().ToString();

				/*child =  canvasChild[2];
				t = child.GetComponent<Text>();
				t.text = box [i].getCurrStorage().ToString() + "/" + box [i].getMaxStorage().ToString();
				*/
				child =  canvasChild[3];
				t = child.GetComponent<Text>();
				t.text = box [i].getStatus ();
				// end of GUI header

				child = canvasChild [4];
				imgTmp = child.GetComponent<RawImage>();
				imgTmp.texture = networkImages [rnd.Next(6)];

				clone = (GameObject)Instantiate (myCube, spawnTemp, transform.rotation);
				spawnTemp.x = spawnTemp.x + (float).86;
				i++;
			}
			if (j % 2 == 0) {
				transform.rotation = Quaternion.Euler (-90, 0, 180);
				spawnSpot.z = spawnSpot.z + 2;
				spawnTemp = spawnSpot;
			} 
			else {
				transform.rotation = Quaternion.Euler (-90, 0, 0);
				//Quaternion.Euler (-90, 0, 180);
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
                            // Scenario: the 9th computer is very high-temperature
                            bool highTemp = (i==9);
							// store data into array
							box[i] = new DataBox(Convert.ToInt32(entries[0]),
								Convert.ToInt32(entries[1]),
								entries[2],
								highTemp);
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
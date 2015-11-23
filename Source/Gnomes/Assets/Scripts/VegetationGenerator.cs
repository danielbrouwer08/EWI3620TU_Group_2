using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VegetationGenerator : MonoBehaviour
{

	public float amountOfBranches;
	public int ruleLength;
	public float chanceF;
	public float chanceX;
	public float chancePlus;
	public float chanceMin;
	public float chanceStart;
	public float chanceEnd;

	public float shakeSpeed;
	public float flipTime;


	public float rotationIntensityX;
	public float rotationIntensityY;
	public float rotationIntensityZ;
	//public float scalingSpeed; //how much smaller do the branches get every iteration
	public float localScaleFactor;

	public float widthScaleFactor;
	public float heightScaleFactor;

	private bool drawState = false;

	// Use this for initialization
	void Start ()
	{
		GameObject startBranch = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
		startBranch.name = "startBranch";
		startBranch.transform.localScale = new Vector3 (widthScaleFactor, heightScaleFactor, widthScaleFactor);

		startBranch.transform.position = transform.position;

		GameObject lastBranch = startBranch;

		lastBranch.GetComponent<Renderer>().material = Resources.Load("VegetationMaterialTop") as Material;

		lastBranch.AddComponent<MushroomShake>();
		lastBranch.GetComponent<MushroomShake>().speed = shakeSpeed;
		lastBranch.GetComponent<MushroomShake>().flipTime = flipTime;

		Vector3 spawnPosition = new Vector3 (0, (1.0f + localScaleFactor), 0) + transform.position;

		string ruleString = generateRule ();
		//char[] drawRule = ruleString.ToCharArray;
		//char[] temp;


	

		//ruleString = "F[-F+F]F[++F]";

		print ("rulestring: " + ruleString);

		List<GameObject> previousBranches = new List<GameObject> ();
		previousBranches.Add (lastBranch);

		//int CNT = 0;

		while (true) {
			string[] ruleSplit = null;

			if (ruleString.IndexOf (']') < ruleString.IndexOf ('[') || !ruleString.Contains ("[")) {
				lastBranch = previousBranches [previousBranches.Count - 2]; //get last saved branch

				//ruleString.Remove (ruleString.IndexOf (']'));
				if(previousBranches.Count>1)
				{
					previousBranches.RemoveAt (previousBranches.Count-2); //remove second to last
				}
				previousBranches.RemoveAt (previousBranches.Count-1); //remove last
				ruleSplit = ruleString.Split (new char[] {']'}, 2);
			} else {
				ruleSplit = ruleString.Split (new char[] {'['}, 2);
			}

			//print ("rule[0] = " + ruleSplit [0]);
			//print ("rule[1] = " + ruleSplit [1]);
			//if(ruleSplit[1]=="")
			//{
		//		break;
		//	}

			char[] drawRule = ruleSplit [0].ToCharArray ();

			previousBranches.Add (lastBranch);
			lastBranch = (createBranch (lastBranch, drawRule));



			if(!string.IsNullOrEmpty(ruleSplit[1]))
			{
			ruleString = ruleSplit [1];
			}else
			{
				break;
			}

			//print ("kom je hier wel ofzo: " + ruleString + " " + previousBranches.Count);

		
			//CNT++;
		
		}
	}

	GameObject createBranch (GameObject startingBranch, char[] drawRule)
	{
		GameObject lastBranch = startingBranch;
		///Vector3 scale = startingBranch.transform.localScale;
		Vector3 scale = new Vector3 (localScaleFactor, localScaleFactor, localScaleFactor);
		//startingBranch.transform.localScale=scale;
		Vector3 rotation = new Vector3 (0.0f, 0.0f, 0.0f);
		float cylinderHeight = (1.0f + localScaleFactor);
		float cylinderWidth = (1.0f + localScaleFactor);

		for (int i=0; i<drawRule.Length; i++) {
	
			switch (drawRule [i]) {
			case 'F':
				if(scale.x>0 && scale.y>0 && scale.z>0) //only execute if scale is valid
				{
					GameObject branch = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
					branch.name = "Branch";
	
					branch.transform.parent = lastBranch.transform; //parent the new branch to the last branch
	
					branch.transform.localScale = scale; //scale the new branch

					branch.transform.localPosition = new Vector3 (0.0f, cylinderHeight, 0.0f); //transform the localposition cylinderHeight further so it will have the same rotation as the parent but will lay on top of its parent
		
					branch.transform.localRotation = Quaternion.Euler (rotation); //add the rotation
		
		
					rotation = new Vector3 (0.0f, 0.0f, 0.0f); //reset the rotation because use of local rotation
		
		
					lastBranch = branch; //save the last branch

					lastBranch.GetComponent<Renderer>().material = Resources.Load("VegetationMaterial") as Material; //Add texture to the branch
		
					//scale = scale * scalingSpeed; //branches will get smaller and smaller each iteration
					cylinderHeight = 1.0f + lastBranch.transform.lossyScale.y; //update the new cylinderheight according to the new scale
					cylinderWidth = 1.0f + lastBranch.transform.lossyScale.x; //update the new cylinderwidth according to the new scale
				}
				break;
			case 'X':
				rotation = rotation + new Vector3 (0.0f, rotationIntensityY, 0.0f); //apply torsion rotation
				break;
			case '-':
		//rotation = rotation - new Vector3(rotationIntensityX,0.0f,rotationIntensityZ);
				rotation = rotation - new Vector3 (0.0f, 0.0f, rotationIntensityZ);
				break;
			case '+':
		//rotation = rotation + new Vector3(rotationIntensityX,0.0f,rotationIntensityZ);
				rotation = rotation + new Vector3 (0.0f, 0.0f, rotationIntensityZ);
				break;
			default:
				print ("Error, rule does not exist");
				break;
		
			}
		}

		return lastBranch;
	}

	string generateRule ()
	{
		int unclosedDrawStates = 0;
		string rule = "F"; //draw starting branch

		for (int z=0; z<ruleLength; z++) {
			
			float randomNumber = Random.Range (0.0f, 1.0f);
			if (randomNumber < (chanceStart)) {
				rule += "["; //push draw state
				unclosedDrawStates++;
			}

			randomNumber = Random.Range (0.0f, 1.0f);
			if (randomNumber < (chanceEnd) && unclosedDrawStates > 0) {
				rule += "]"; //pull draw state
				unclosedDrawStates--;
			}

			float[] CDF = new float[4]; //CDF function
			float sum = chanceF + chanceX + chancePlus + chanceMin;
			float[] PDF = new float[4] {
				chanceF / sum,
				chanceX / sum,
				chancePlus / sum,
				chanceMin / sum
			};
			float temp = 0;

			for (int i=0; i<PDF.Length; i++) {
				temp += PDF [i];
				CDF [i] = temp;
			}

			float randomNum = Random.Range (0.0f, 1.0f); //generate a number from 0 to 1

			if (randomNum < CDF [0]) {
				rule += "F";
			} else if (randomNum < CDF [1]) {
				rule += "X";
			} else if (randomNum < CDF [2]) {
				rule += "+";
			} else if (randomNum < CDF [3]) {
				rule += "-";
			}

		}

		//close any unclosed draw states:

		while (unclosedDrawStates>0) {
			rule += "]"; //pull draw state
			unclosedDrawStates--;
		}


		return rule;
	}



}

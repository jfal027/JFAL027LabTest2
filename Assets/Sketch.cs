using UnityEngine;
using Pathfinding.Serialization.JsonFx; //make sure you include this using

public class Sketch : MonoBehaviour {
    public GameObject myPrefab;

    public string _WebsiteURL = "http://JFAL027PracticeTest2Resources.azurewebsites.net/tables/TreeSurvey?ZUMO-API-VERSION=2.0.0";

    public Material[] material;

    void Start () {
        
        string jsonResponse = Request.GET(_WebsiteURL);

        if (string.IsNullOrEmpty(jsonResponse))
        {
            return;
        }
        
        TreeSurvey[] trees = JsonReader.Deserialize<TreeSurvey[]>(jsonResponse);

        Debug.Log("Number of records: " + trees.Length);

        int totalCubes = trees.Length;
        int totalDistance = 5;
        int i = 0;
        float cubeSize = 0f;
        int x1 = 0, x2 = 0, x3 = 0;
        foreach (TreeSurvey tree in trees)
        {
            float perc = i / (float)totalCubes;
            i++;
            float x = perc * totalDistance;
            float y = 0f;
            float z = 0.0f;

            int point = int.Parse(tree.X);

            if (point > 28 && point <= 29 )
            {
                y = 7.5f;
                x = (x1 / (float)totalCubes) * totalDistance + cubeSize;
                x1++;
            }
            else if (point > 30.1 && point <= 31)
            {
                y = 5.0f;
                x = (x2 / (float)totalCubes) * totalDistance + cubeSize;
                x2++;
            }
            else
            {
                y = 2.5f;
                x = (x3 / (float)totalCubes) * totalDistance + cubeSize;
                x3++;
            }

            GameObject newCube = (GameObject)Instantiate(myPrefab, new Vector3(x, y, z), Quaternion.identity);
            cubeSize = 0.5f;
            newCube.GetComponent<myCubeScript>().setSize(cubeSize);
            newCube.GetComponent<myCubeScript>().ratateSpeed = perc;
            newCube.GetComponentInChildren<TextMesh>().text = tree.WhenReadingRecorded;

            if (tree.Location.Contains("Domain"))
            {
                newCube.GetComponent<Renderer>().material = material[0];
            }
            else if (tree.Location.Contains("Albert Park"))
            {
                newCube.GetComponent<Renderer>().material = material[1];
            }
            else if (tree.Location.Contains("Myers Park"))
            {
                newCube.GetComponent<Renderer>().material = material[2];
            }
            else if (tree.Location.Contains("Howick"))
            {
                newCube.GetComponent<Renderer>().material = material[3];
            }
        }

        Debug.Log("Number of cubes created: " + i);
    }
	
	void Update () {
	
	}
}

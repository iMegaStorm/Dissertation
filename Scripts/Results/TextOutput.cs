using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextOutput : MonoBehaviour
{
    public int scenario;
    Resources resource;
    private bool switchState;
    string data;
    string content;
    private GameManager gM;
    private bool isStarted;

    public static TextOutput instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        resource = FindObjectOfType<Resources>();
    }

    private void Update()
    {
        if(resource.scenarioIsOver && !switchState)
        {
            //Debug.Log("Called");
            AStarData();
            DijkstraData();
            switchState = true;
        }
        
    }

    public void AlgorithmPerformance(int _team)
    {
        gM = GameManager.instance;
        if (gM.algorithmsPerformance)
        {
            if (scenario == 1)
            {
                //Path to the text file and names the file
                data = Application.dataPath + "/Scripts/Results/Scenario1/" + "Temp.txt";

                if (!File.Exists(data))
                {
                    File.WriteAllText(data, "A Star,Dijkstra");
                }

                if (!isStarted && _team == 1)
                {
                    content = "\n";
                    isStarted = true;
                }

                if (_team == 1 && gM.dijkstra != "")
                {

                    content = "\nDijkstra Cycle, " + gM.dijkstra;
                }
                else if (_team == 2 && gM.aStar != "")
                {
                    content = "\nA Star Cycle, " + gM.aStar;
                }
            }
            else if (scenario == 2)
            {
                //Path to the text file and names the file
                data = Application.dataPath + "/Scripts/Results/Scenario2/" + "AlgorithmsPerformanceScenario2.txt";

                if (!File.Exists(data))
                {
                    File.WriteAllText(data, "A Star,Dijkstra");
                }

                if (!isStarted && _team == 1)
                {
                    content = "\n";
                    isStarted = true;
                }

                if (_team == 1 && gM.dijkstra != "")
                {
                    content = "\nDijkstra Cycle, " + gM.dijkstra;
                }
                else if (_team == 2 && gM.aStar != "")
                {
                    content = "\nA Star Cycle, " + gM.aStar;
                }

            }
            else if (scenario == 3)
            {
                //Path to the text file and names the file
                data = Application.dataPath + "/Scripts/Results/Scenario3/" + "AlgorithmsPerformanceScenario3.txt";

                if (!File.Exists(data))
                {
                    File.WriteAllText(data, "A Star,Dijkstra");
                }

                if (!isStarted && _team == 1)
                {
                    content = "\n";
                    isStarted = true;
                }

                if (_team == 1 && gM.dijkstra != "")
                {
                    content = "\nDijkstra Cycle, " + gM.dijkstra;
                }
                else if (_team == 2 && gM.aStar != "")
                {
                    content = "\nA Star Cycle, " + gM.aStar;
                }
            }
            File.AppendAllText(data, content);
        }
    }

    public void AStarData()
    {
        gM = GameManager.instance;
        if (gM.resourceResults)
        {
            if (scenario == 1)
            {
                //Path to the text file and names the file
                data = Application.dataPath + "/Scripts/Results/Scenario1/" + "AStarScenario1.txt";

                if (!File.Exists(data))
                {
                    File.WriteAllText(data, ",,A Star Scenario 1 Data \n" + "Unit Count, Food Completed, Tree Completed, Food Gathered, Tree Gathered,\n");
                }
                content = "\n" + gM.unitCountTeam2 + "," + gM.foodCompletedTeam2 + "," + gM.treeCompletedTeam2 + "," + gM.foodGatheredTeam2 + "," + gM.treeGatheredTeam2 + "\n";

            }
            else if (scenario == 2)
            {
                //Path to the text file and names the file
                data = Application.dataPath + "/Scripts/Results/Scenario2/" + "AStarScenario2.txt";

                if (!File.Exists(data))
                {
                    File.WriteAllText(data, ",,A Star Scenario 2 Data \n" + "Unit Count, Food Completed, Tree Completed, Food Gathered, Tree Gathered,\n");
                }
                content = "\n" + gM.unitCountTeam2 + "," + gM.foodCompletedTeam2 + "," + gM.treeCompletedTeam2 + "," + gM.foodGatheredTeam2 + "," + gM.treeGatheredTeam2 + "\n";
            }
            else if (scenario == 3)
            {
                //Path to the text file and names the file
                data = Application.dataPath + "/Scripts/Results/Scenario3/" + "AStarScenario3.txt";

                if (!File.Exists(data))
                {
                    File.WriteAllText(data, ",,A Star Scenario 3 Data \n" + "Unit Count, Food Completed, Tree Completed, Food Gathered, Tree Gathered,\n");
                }
                content = "\n" + gM.unitCountTeam2 + "," + gM.foodCompletedTeam2 + "," + gM.treeCompletedTeam2 + "," + gM.foodGatheredTeam2 + "," + gM.treeGatheredTeam2 + "\n";
            }
            File.AppendAllText(data, content);
        }
    }

    public void DijkstraData()
    {
        gM = GameManager.instance;
        if (gM.resourceResults)
        {
            if (scenario == 1)
            {
                //Path to the text file and names the file
                data = Application.dataPath + "/Scripts/Results/Scenario1/" + "DijkstraScenario1.txt";

                if (!File.Exists(data))
                {
                    File.WriteAllText(data, ",,Dijkstra Scenario 1 Data \n" + "Unit Count, Food Completed, Tree Completed, Food Gathered, Tree Gathered,\n");
                }
                content = "\n" + gM.unitCountTeam1 + "," + gM.foodCompletedTeam1 + "," + gM.treeCompletedTeam1 + "," + gM.foodGatheredTeam1 + "," + gM.treeGatheredTeam1 + "\n";
            }
            else if (scenario == 2)
            {
                //Path to the text file and names the file
                data = Application.dataPath + "/Scripts/Results/Scenario2/" + "DijkstraScenario2.txt";

                if (!File.Exists(data))
                {
                    File.WriteAllText(data, ",,Dijkstra Scenario 2 Data \n" + "Unit Count, Food Completed, Tree Completed, Food Gathered, Tree Gathered,\n");
                }
                content = "\n" + gM.unitCountTeam1 + "," + gM.foodCompletedTeam1 + "," + gM.treeCompletedTeam1 + "," + gM.foodGatheredTeam1 + "," + gM.treeGatheredTeam1 + "\n";
            }
            else if (scenario == 3)
            {
                //Path to the text file and names the file
                data = Application.dataPath + "/Scripts/Results/Scenario3/" + "DijkstraScenario3.txt";

                if (!File.Exists(data))
                {
                    File.WriteAllText(data, ",,Dijkstra Scenario 3 Data \n" + "Unit Count, Food Completed, Tree Completed, Food Gathered, Tree Gathered,\n");
                }
                content = "\n" + gM.unitCountTeam1 + "," + gM.foodCompletedTeam1 + "," + gM.treeCompletedTeam1 + "," + gM.foodGatheredTeam1 + "," + gM.treeGatheredTeam1 + "\n";
            }
            File.AppendAllText(data, content);
        }
    }
}


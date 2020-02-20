using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityAIScript : MonoBehaviour {

    public float howFull, sleep, howHappy, howFit, money;
    public float fitnessStat, moneyStat, happyStat, hungerStat, tiredStat; 
    [SerializeField]
    protected float eatWeighting; 
    [SerializeField]
    protected float sleepWeighting;
    [SerializeField]
    protected float moneyWeighting;
    [SerializeField]
    protected float happyWeighting;
    [SerializeField]
    protected float fitnessWeighting;

    public float eatUtility, sleepUtility, exerciseUtility, workUtility, playUtility;

    public float health;

    public GameObject[] waypoints;

    public bool stomachBug, insomnia, sad, sloth, bills;

    bool eatMove, sleepMove, playMove, workMove, exerciseMove;

    float arrivalRadius = 0.2f;
    float maxSpeed = 8f;
    Vector3 desiredVelocity = Vector3.zero;

    // Use this for initialization
    void Start () {

        InvokeRepeating("StatDecrease", 1.0f, 1.0f);
        InvokeRepeating("AdverseEffects", 3.0f, 5.0f);
        howFull = 50;
        sleep = 50;
        howHappy = 50;
        howFit = 50;
        money = 50;

        GetUtilityScores();
        
	}
	
	// Update is called once per frame
	void Update () {
        howFull = Mathf.Clamp(howFull, 0, 100);
        sleep = Mathf.Clamp(sleep, 0, 100);
        howHappy = Mathf.Clamp(howHappy, 0, 100);
        howFit = Mathf.Clamp(howFit, 0, 100);
        money = Mathf.Clamp(money, 0, 100);

        health = (howFull + sleep + howHappy + howFit + money) / 5;

        //values are hardcoded 
        if(eatMove)
        {
            agentMovement(0);
        }
        if (sleepMove)
        {
            agentMovement(1);
        }
        if(playMove)
        {
            agentMovement(2);
        }
        if(workMove)
        {
            agentMovement(3);
        }
        if(exerciseMove)
        {
            agentMovement(4);
        }

        if(health < 20)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if(health >= 20 && health < 50)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else if(health > 50)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    void AdverseEffects()
    {
        bool sickness = false;
        int whichEffect = 0;
        int randInt = Random.Range(0, 100);
        if((randInt > 45) && (randInt < 51))
        {
            sickness = true;
            whichEffect = Random.Range(1, 6);
        }
        if (sickness)
        {
            switch (whichEffect)
            {
                case 0:
                    break;
                case 1:
                    //howFull -= 50f; 
                    stomachBug = true;
                    Debug.Log("Stomach Bug");
                    break;
                case 2:
                    //sleep -= 70f; //Developed insomnia
                    insomnia = true;
                    Debug.Log("Insomnia");
                    break;
                case 3:
                    //howHappy -= 40f;
                    sad = true;
                    Debug.Log("Feeling down in the dumps");
                    break;
                case 4:
                    //howFit -= 50f;
                    sloth = true;
                    Debug.Log("Sloth Activate");
                    break;
                case 5:
                    //money -= 30;
                    bills = true;
                    Debug.Log("Bills");
                    break;
            }
            InvokeRepeating("ProcessAdverseEffects", 0.0f, 1.0f);
            StartCoroutine(IllnessCountdown());
        }
    }

    void ProcessAdverseEffects()
    {
        if(stomachBug)
        {
            howFull -= 10f;
        }
        else if(insomnia)
        {
            sleep -= 12f;
        }
        else if(sad)
        {
            howHappy -= 8f;
        }
        else if(sloth)
        {
            howFit -= 10f;
        }
        else if(bills)
        {
            money -= 6f;
        }
    }


    void StatDecrease()
    {
        howFull -= 3;
        sleep -= 2;
        howHappy -= 2;
        howFit -= 1;
        money -= 2;
    }

    void GetUtilityScores()
    {
        //float x, y;
        //x = 0.5;
        //y = (50 / 100);
        //hungerStat = Mathf.Pow((howFull / 100f), eatWeighting);


        //These are curves for calculating scores for decision factors
        hungerStat = Mathf.Pow((howFull / 100f), eatWeighting);
        fitnessStat = Mathf.Pow((howFit / 100f), fitnessWeighting);
        moneyStat = Mathf.Pow((money / 100f), moneyWeighting);
        happyStat = Mathf.Pow((howHappy / 100f), happyWeighting);
        tiredStat = Mathf.Pow((sleep / 100f), sleepWeighting);

        //Calculating the utility scores of the actions
        eatUtility = (1 - hungerStat);
        sleepUtility = (1 - tiredStat);
        exerciseUtility = ((1 - fitnessStat) + (1 - hungerStat))/2;
        workUtility = ((1 - moneyStat) + (1 - tiredStat))/2 ;
        playUtility = ((1 - happyStat) + (1 - moneyStat))/2;
        //Debug.Log(sleepUtility);
        //Debug.Log(eatUtility);
        ChooseAction();
    }

    void ChooseAction()
    {
        List<float> list = new List<float>();
        List<string> bucketList = new List<string>();

        list.Add(eatUtility);
        list.Add(sleepUtility);
        list.Add(exerciseUtility);
        list.Add(workUtility);
        list.Add(playUtility);

        bucketList = getList(0.8f, list);

        if(bucketList.Count.Equals(0))
        {
            bucketList = getList(0.6f, list);
        }

        if (bucketList.Count.Equals(0))
        {
            bucketList = getList(0.4f, list);
        }

        if (bucketList.Count.Equals(0))
        {
            bucketList = getList(0.2f, list);
        }

        if (bucketList.Count.Equals(0))
        {
            bucketList = getList(0.0f, list);
        }

        int choice = Random.Range(0, bucketList.Count);


        if(bucketList.Contains("eatUtility"))
        {
            if(bucketList.IndexOf("eatUtility") == choice)
            {
                StartCoroutine(EatAction());
            }
        }
        if (bucketList.Contains("sleepUtility"))
        {
            if (bucketList.IndexOf("sleepUtility") == choice)
            {
                StartCoroutine(SleepAction());
            }
        }
        if (bucketList.Contains("exerciseUtility"))
        {
            if (bucketList.IndexOf("exerciseUtility") == choice)
            {
                StartCoroutine(ExerciseAction());
            }
        }
        if (bucketList.Contains("workUtility"))
        {
            if (bucketList.IndexOf("workUtility") == choice)
            {
                StartCoroutine(WorkAction());
            }
        }
        if (bucketList.Contains("playUtility"))
        {
            if (bucketList.IndexOf("playUtility") == choice)
            {
                StartCoroutine(PlayAction());
            }
        }


        list.Clear();
        bucketList.Clear();
    }

    List<string> getList(float j, List<float> list)
    {
        List<string> tempList = new List<string>();

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] > j)
            {
                switch (i)
                {
                    case 0:
                        tempList.Add("eatUtility");
                        break;
                    case 1:
                        tempList.Add("sleepUtility");
                        break;
                    case 2:
                        tempList.Add("exerciseUtility");
                        break;
                    case 3:
                        tempList.Add("workUtility");
                        break;
                    case 4:
                        tempList.Add("playUtility");
                        break;
                }
            }
        }
        return tempList;
    }

    void agentMovement(int choice)
    {
        Vector3 targetPosition = waypoints[choice].transform.position;
        if (Vector3.Distance(transform.position, targetPosition) > arrivalRadius)
        {
            desiredVelocity = Vector3.Normalize(targetPosition - transform.position) * maxSpeed;
        }
        else
        {
            desiredVelocity = (Vector3.Normalize(targetPosition - transform.position) * maxSpeed) * (Vector3.Distance(transform.position, targetPosition) / arrivalRadius);
        }
        if (desiredVelocity.sqrMagnitude > 0.0f)
        {
            transform.up = Vector3.Normalize(new Vector3(desiredVelocity.x, desiredVelocity.y, 0));
        }
        transform.position += new Vector3(desiredVelocity.x, desiredVelocity.y, -0.1f) * Time.deltaTime;
    }




    IEnumerator EatAction()
    {
        howFull += 33;
        sleep -= 10;
        eatMove = true;
        yield return new WaitForSeconds(3);
        eatMove = false;
        GetUtilityScores();
 
    }

    IEnumerator SleepAction()
    {
        sleep += 60;
        howFull -= 10;
        howFit -= 5;
        howHappy += 10;
        sleepMove = true;
        yield return new WaitForSeconds(3);
        sleepMove = false;
        GetUtilityScores();
    }

    IEnumerator ExerciseAction()
    {
        howFit += 25;
        howFull -= 10;
        sleep -= 10;
        howHappy += 5;
        exerciseMove = true;
        yield return new WaitForSeconds(3);
        exerciseMove = false;
        GetUtilityScores();
    }

    IEnumerator WorkAction()
    {
        money += 30;
        howHappy -= 10;
        sleep -= 10;
        howFull -= 10;
        howFit -= 5;
        workMove = true;
        yield return new WaitForSeconds(3);
        workMove = false;
        GetUtilityScores();
    }

    IEnumerator PlayAction()
    {
        howHappy += 40;
        money -= 5;
        sleep -= 5;
        howFit += 5;
        howFull -= 5;
        playMove = true;
        yield return new WaitForSeconds(3);
        playMove = false;
        GetUtilityScores();
    }

    IEnumerator IllnessCountdown()
    {
        yield return new WaitForSeconds(5);
        CancelInvoke("ProcessAdverseEffects");
        stomachBug = false;
        insomnia = false;
        sad = false;
        sloth = false;
        bills = false;
    }
}

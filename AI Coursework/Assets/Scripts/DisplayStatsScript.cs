using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStatsScript : MonoBehaviour {

    public Text aiNameText, healthText, tiredStatText, hungerStatText, fitnessStatText, moneyStatText, happinessStatText, illnessText;
    public GameObject balancedButton, greedyButton, sleepyButton, happyButton, fitButton, richButton, closeUIButton, openUIButton;
    bool statsOn, balancedBool, greedyBool, sleepyBool, fitBool, happyBool, moneyBool;
    GameObject selectedAI;


    // Use this for initialization
    void Start() {
        statsOn = false;
        selectedAI = null;
        balancedBool = false;
        greedyBool = false;
        sleepyBool = false;
        fitBool = false;
        happyBool = false;
        moneyBool = false;
    }

    // Update is called once per frame
    void Update() {
        if(statsOn)
        {
            if(balancedBool)
            {
                DisplayAI(0);
            }
            else if(greedyBool)
            {
                DisplayAI(1);
            }
            else if (sleepyBool)
            {
                DisplayAI(2);
            }
            else if (fitBool)
            {
                DisplayAI(3);
            }
            else if (happyBool)
            {
                DisplayAI(4);
            }
            else if (moneyBool)
            {
                DisplayAI(5);
            }
        }
    }

    public void CancelUI()
    {
        statsOn = false;
        aiNameText.enabled = false;
        healthText.enabled = false;
        hungerStatText.enabled = false;
        tiredStatText.enabled = false;
        fitnessStatText.enabled = false;
        happinessStatText.enabled = false;
        moneyStatText.enabled = false;
        illnessText.enabled = false;
        balancedButton.SetActive(false);
        greedyButton.SetActive(false);
        sleepyButton.SetActive(false);
        happyButton.SetActive(false);
        fitButton.SetActive(false);
        richButton.SetActive(false);
        closeUIButton.SetActive(false);
        openUIButton.SetActive(true);
    }

    public void OpenUI()
    {
        statsOn = true;
        aiNameText.enabled = true;
        healthText.enabled = true;
        hungerStatText.enabled = true;
        tiredStatText.enabled = true;
        fitnessStatText.enabled = true;
        happinessStatText.enabled = true;
        moneyStatText.enabled = true;
        illnessText.enabled = true;
        balancedButton.SetActive(true);
        greedyButton.SetActive(true);
        sleepyButton.SetActive(true);
        happyButton.SetActive(true);
        fitButton.SetActive(true);
        richButton.SetActive(true);
        closeUIButton.SetActive(true);
        openUIButton.SetActive(false);
    }

    public void SelectBalancedAI()
    {
        aiNameText.text = "AI: Balanced";
        statsOn = true;
        balancedBool = true;
        greedyBool = false;
        sleepyBool = false;
        fitBool = false;
        happyBool = false;
        moneyBool = false;
    }

    public void SelectGreedyAI()
    {
        aiNameText.text = "AI: Greedy";
        DisplayAI(1);
        statsOn = true;
        greedyBool = true;
        balancedBool = false;
        sleepyBool = false;
        fitBool = false;
        happyBool = false;
        moneyBool = false;
    }

    public void SelectTiredAI()
    {
        aiNameText.text = "AI: Sleepy";
        statsOn = true;
        balancedBool = false;
        greedyBool = false;
        sleepyBool = true;
        fitBool = false;
        happyBool = false;
        moneyBool = false;
    }

    public void SelectFitnessAI()
    {
        aiNameText.text = "AI: Fit";
        statsOn = true;
        balancedBool = false;
        greedyBool = false;
        sleepyBool = false;
        fitBool = true;
        happyBool = false;
        moneyBool = false;
    }

    public void SelectHappyAI()
    {
        aiNameText.text = "AI: Happy";
        statsOn = true;
        balancedBool = false;
        greedyBool = false;
        sleepyBool = false;
        fitBool = false;
        happyBool = true;
        moneyBool = false;
    }

    public void SelectRichAI()
    {
        aiNameText.text = "AI: Rich";
        statsOn = true;
        balancedBool = false;
        greedyBool = false;
        sleepyBool = false;
        fitBool = false;
        happyBool = false;
        moneyBool = true;
    }

    void DisplayAI(int choice)
    {
        switch(choice)
        {
            case 0:
                selectedAI = GameObject.Find("BalancedAI");
                healthText.text = "Health: " + selectedAI.GetComponent<UtilityAIScript>().health;
                hungerStatText.text = "Hunger Stat: " + selectedAI.GetComponent<UtilityAIScript>().howFull;
                tiredStatText.text = "Tired Stat: " + selectedAI.GetComponent<UtilityAIScript>().sleep;
                happinessStatText.text = "Happiness Stat: " + selectedAI.GetComponent<UtilityAIScript>().howHappy;
                fitnessStatText.text = "Fitness Stat: " + selectedAI.GetComponent<UtilityAIScript>().howFit;
                moneyStatText.text = "Money: " + selectedAI.GetComponent<UtilityAIScript>().money;
                if(selectedAI.GetComponent<UtilityAIScript>().stomachBug)
                {
                    illnessText.text = "Effect: Stomach Bug";
                }
                if (selectedAI.GetComponent<UtilityAIScript>().insomnia)
                {
                    illnessText.text = "Effect: Insomnia";
                }
                if (selectedAI.GetComponent<UtilityAIScript>().sad)
                {
                    illnessText.text = "Effect: Down in the dumps";
                }
                if (selectedAI.GetComponent<UtilityAIScript>().sloth)
                {
                    illnessText.text = "Effect: Sloth";
                }
                if (selectedAI.GetComponent<UtilityAIScript>().bills)
                {
                    illnessText.text = "Effect: Bills";
                }
                break;
            case 1:
                selectedAI = GameObject.Find("GreedyAI");
                healthText.text = "Health: " + selectedAI.GetComponent<UtilityAIScript>().health;
                hungerStatText.text = "Hunger Stat: " + selectedAI.GetComponent<UtilityAIScript>().howFull;
                tiredStatText.text = "Tired Stat: " + selectedAI.GetComponent<UtilityAIScript>().sleep;
                happinessStatText.text = "Happiness Stat: " + selectedAI.GetComponent<UtilityAIScript>().howHappy;
                fitnessStatText.text = "Fitness Stat: " + selectedAI.GetComponent<UtilityAIScript>().howFit;
                moneyStatText.text = "Money: " + selectedAI.GetComponent<UtilityAIScript>().money;
                if (selectedAI.GetComponent<UtilityAIScript>().stomachBug)
                {
                    illnessText.text = "Effect: Stomach Bug";
                }
                if (selectedAI.GetComponent<UtilityAIScript>().insomnia)
                {
                    illnessText.text = "Effect: Insomnia";
                }
                if (selectedAI.GetComponent<UtilityAIScript>().sad)
                {
                    illnessText.text = "Effect: Down in the dumps";
                }
                if (selectedAI.GetComponent<UtilityAIScript>().sloth)
                {
                    illnessText.text = "Effect: Sloth";
                }
                if (selectedAI.GetComponent<UtilityAIScript>().bills)
                {
                    illnessText.text = "Effect: Bills";
                }
                break;
            case 2:
                selectedAI = GameObject.Find("SleepyAI");
                healthText.text = "Health: " + selectedAI.GetComponent<UtilityAIScript>().health;
                hungerStatText.text = "Hunger Stat: " + selectedAI.GetComponent<UtilityAIScript>().howFull;
                tiredStatText.text = "Tired Stat: " + selectedAI.GetComponent<UtilityAIScript>().sleep;
                happinessStatText.text = "Happiness Stat: " + selectedAI.GetComponent<UtilityAIScript>().howHappy;
                fitnessStatText.text = "Fitness Stat: " + selectedAI.GetComponent<UtilityAIScript>().howFit;
                moneyStatText.text = "Money: " + selectedAI.GetComponent<UtilityAIScript>().money;
                if (selectedAI.GetComponent<UtilityAIScript>().stomachBug)
                {
                    illnessText.text = "Effect: Stomach Bug";
                }
                if (selectedAI.GetComponent<UtilityAIScript>().insomnia)
                {
                    illnessText.text = "Effect: Insomnia";
                }
                if (selectedAI.GetComponent<UtilityAIScript>().sad)
                {
                    illnessText.text = "Effect: Down in the dumps";
                }
                if (selectedAI.GetComponent<UtilityAIScript>().sloth)
                {
                    illnessText.text = "Effect: Sloth";
                }
                if (selectedAI.GetComponent<UtilityAIScript>().bills)
                {
                    illnessText.text = "Effect: Bills";
                }
                break;
            case 3:
                selectedAI = GameObject.Find("FitAI");
                healthText.text = "Health: " + selectedAI.GetComponent<UtilityAIScript>().health;
                hungerStatText.text = "Hunger Stat: " + selectedAI.GetComponent<UtilityAIScript>().howFull;
                tiredStatText.text = "Tired Stat: " + selectedAI.GetComponent<UtilityAIScript>().sleep;
                happinessStatText.text = "Happiness Stat: " + selectedAI.GetComponent<UtilityAIScript>().howHappy;
                fitnessStatText.text = "Fitness Stat: " + selectedAI.GetComponent<UtilityAIScript>().howFit;
                moneyStatText.text = "Money: " + selectedAI.GetComponent<UtilityAIScript>().money;
                if (selectedAI.GetComponent<UtilityAIScript>().stomachBug)
                {
                    illnessText.text = "Effect: Stomach Bug";
                }
                if (selectedAI.GetComponent<UtilityAIScript>().insomnia)
                {
                    illnessText.text = "Effect: Insomnia";
                }
                if (selectedAI.GetComponent<UtilityAIScript>().sad)
                {
                    illnessText.text = "Effect: Down in the dumps";
                }
                if (selectedAI.GetComponent<UtilityAIScript>().sloth)
                {
                    illnessText.text = "Effect: Sloth";
                }
                if (selectedAI.GetComponent<UtilityAIScript>().bills)
                {
                    illnessText.text = "Effect: Bills";
                }
                break;
            case 4:
                selectedAI = GameObject.Find("HappyAI");
                healthText.text = "Health: " + selectedAI.GetComponent<UtilityAIScript>().health;
                hungerStatText.text = "Hunger Stat: " + selectedAI.GetComponent<UtilityAIScript>().howFull;
                tiredStatText.text = "Tired Stat: " + selectedAI.GetComponent<UtilityAIScript>().sleep;
                happinessStatText.text = "Happiness Stat: " + selectedAI.GetComponent<UtilityAIScript>().howHappy;
                fitnessStatText.text = "Fitness Stat: " + selectedAI.GetComponent<UtilityAIScript>().howFit;
                moneyStatText.text = "Money: " + selectedAI.GetComponent<UtilityAIScript>().money;
                if (selectedAI.GetComponent<UtilityAIScript>().stomachBug)
                {
                    illnessText.text = "Effect: Stomach Bug";
                }
                if (selectedAI.GetComponent<UtilityAIScript>().insomnia)
                {
                    illnessText.text = "Effect: Insomnia";
                }
                if (selectedAI.GetComponent<UtilityAIScript>().sad)
                {
                    illnessText.text = "Effect: Down in the dumps";
                }
                if (selectedAI.GetComponent<UtilityAIScript>().sloth)
                {
                    illnessText.text = "Effect: Sloth";
                }
                if (selectedAI.GetComponent<UtilityAIScript>().bills)
                {
                    illnessText.text = "Effect: Bills";
                }
                break;
            case 5:
                selectedAI = GameObject.Find("RichAI");
                healthText.text = "Health: " + selectedAI.GetComponent<UtilityAIScript>().health;
                hungerStatText.text = "Hunger Stat: " + selectedAI.GetComponent<UtilityAIScript>().howFull;
                tiredStatText.text = "Tired Stat: " + selectedAI.GetComponent<UtilityAIScript>().sleep;
                happinessStatText.text = "Happiness Stat: " + selectedAI.GetComponent<UtilityAIScript>().howHappy;
                fitnessStatText.text = "Fitness Stat: " + selectedAI.GetComponent<UtilityAIScript>().howFit;
                moneyStatText.text = "Money: " + selectedAI.GetComponent<UtilityAIScript>().money;
                if (selectedAI.GetComponent<UtilityAIScript>().stomachBug)
                {
                    illnessText.text = "Effect: Stomach Bug";
                }
                if (selectedAI.GetComponent<UtilityAIScript>().insomnia)
                {
                    illnessText.text = "Effect: Insomnia";
                }
                if (selectedAI.GetComponent<UtilityAIScript>().sad)
                {
                    illnessText.text = "Effect: Down in the dumps";
                }
                if (selectedAI.GetComponent<UtilityAIScript>().sloth)
                {
                    illnessText.text = "Effect: Sloth";
                }
                if (selectedAI.GetComponent<UtilityAIScript>().bills)
                {
                    illnessText.text = "Effect: Bills";
                }
                break;
        }
    }
}

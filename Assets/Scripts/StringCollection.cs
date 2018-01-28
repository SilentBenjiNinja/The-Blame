using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringCollection
{
    public const string VERSION = "v0.0.1";
    public const string TESTBUTTON = "TestButton";
    public const string BOOLTEXT = "boolText";
    public const string LIFETEXT = "LifeText";
    public const string KILLBUTTON = "KillButton";
    public const string PLAYERSTATUS = "Your Player is: ";

    public const string GAMERULESONE = "Game Rules 1";
    public const string GAMERULESTWO = "Game Rules 2";
    public const string GAMERULESTHREE = "Game Rules 3";

    public const string GAMERULESPAGEONE = "There are four departments: " +
                                            "Management, Sales, HR and IT who have to deal with a pool of workload that is slowly increased over time" +
                                            "and distributed evenly among them. However, players can target other departments to give them some of their own workload or take some of their workload from themselves. " +
                                            "After targeting one department, there is a cooldown, before it can be targeted again by the same player.";

    public const string GAMERULESPAGETWO = "Every Department has a Workload-meter and a Blame-meter." +
                                            "The Workload-meter goes from 0% - 100%. While a player’s workload is above 25% and below 75%, their Blame decreases at a constant rate.The closer they are to 50% the more blame they decrease.";
        
    public const string GAMERULESPAGETHREE = "If their workload goes below 25% or past 75%, their blame starts to increase.If the workload reaches 0% or surpasses 100%, they go ‘bust’, meaning that they instantly get ‘The Blame’. Players also get the blame, if their Blame-meter reaches 100%."; 

}

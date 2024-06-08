using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect
{
    public statusEffect inflictStatus;
    public statusEffect selfStatus;
    public hazard hazard;
    public fieldEffects inflictField;
    public fieldEffects selfField;
    public LibraryStatus status;
    public effect(statusEffect InflictStatus, statusEffect SelfStatus)
    {
        inflictStatus = InflictStatus;
        selfStatus = SelfStatus;
    }
    public effect(statusEffect InflictStatus, statusEffect SelfStatus, hazard Hazard)
    {
        inflictStatus = InflictStatus;
        selfStatus = SelfStatus;
        hazard = Hazard;
    }
    public effect(statusEffect InflictStatus, statusEffect SelfStatus, fieldEffects InflictField, fieldEffects SelfField)
    {
        inflictStatus = InflictStatus;
        selfStatus = SelfStatus;
        inflictField = InflictField;
        selfField = SelfField;
    }
    public effect(statusEffect InflictStatus, statusEffect SelfStatus, hazard Hazard, fieldEffects InflictField, fieldEffects SelfField)
    {
        inflictStatus = InflictStatus;
        selfStatus = SelfStatus;
        hazard = Hazard;
        inflictField = InflictField;
        selfField = SelfField;
    }
    public effect(string effectname1, string effectname2)
    {
        //get reference to single status library
        status = GameObject.Find("BattleHandler").GetComponent<LibraryStatus>();
        inflictStatus =  status.statusDictionary[effectname1];
        selfStatus = status.statusDictionary[effectname2];
    }
    public effect(string effectname1, string effectname2, string hazardName)
    {
        status = GameObject.Find("BattleHandler").GetComponent<LibraryStatus>();
        inflictStatus = status.statusDictionary[effectname1];
        selfStatus = status.statusDictionary[effectname2];
    }
    public effect(string effectname1, string effectname2, string Field1, string Field2)
    {
        status = GameObject.Find("BattleHandler").GetComponent<LibraryStatus>();
        inflictStatus = status.statusDictionary[effectname1];
        selfStatus = status.statusDictionary[effectname2];
    }
    public effect(string effectname1, string effectname2, string hazardName, string Field1, string Field2)
    {
        status = GameObject.Find("BattleHandler").GetComponent<LibraryStatus>();
        inflictStatus = status.statusDictionary[effectname1];
        selfStatus = status.statusDictionary[effectname2];
    }
    
}
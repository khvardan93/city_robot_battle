using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData")]
public class GameData : ScriptableObject
{
    [Serializable]
    public struct Task
    {
        public Currencies currency;
        public int reward;
        public string description;
        public int progress;
        public int skip;
        [Space]
        public TaskType taskType;
        public bool hasRobot;
        public RobotType robot;
    }

    public Task[] tasks;

    [Serializable]
    public struct Case
    {
        public Currencies currency;
        public int count;
        public RobotType robotType;
    }
    public Case[] firstCase;
    public Case[] secondCase;
    public static Case[] currentCase;

    public int[] rewards;

    [Serializable]
    public struct DonateItem
    {
        public float price;
        public int count;
    }
    [Space]
    public DonateItem[] goldItems;
    public DonateItem[] silverItems;
}
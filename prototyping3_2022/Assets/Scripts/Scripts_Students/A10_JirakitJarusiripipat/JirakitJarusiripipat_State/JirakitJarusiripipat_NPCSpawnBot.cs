using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JirakitJarusiripipat_NPCSpawnBot : JirakitJarusiripipat_IState
{
    JirakitJarusiripipat_Weapon spawnBot;
    public JirakitJarusiripipat_NPCSpawnBot(JirakitJarusiripipat_Weapon bot)
    {
        spawnBot = bot;
    }
    public void OnEnter()
    {
        Debug.Log("Enter SpawnBotState");
        spawnBot.SpawnSuicideBot();
    }

    public void OnExit()
    {
        //JirakitJarusiripipat_BotA10NPC.isMoving = true;
        Debug.Log("Out SpawnBotState");
    }

    public void Tick()
    {
        Debug.Log("In SpawnBotState");
    }
}

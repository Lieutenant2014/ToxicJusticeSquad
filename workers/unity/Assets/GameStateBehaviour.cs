using System.Collections.Generic;
using Assets.Gamelogic.Visualizers;
using UnityEngine;
using Improbable.Global;
using Improbable.Unity.Visualizer;
using Improbable.Util.Collections;

public class GameStateBehaviour : MonoBehaviour
{
    [Require]
    private GameStateReader GameState;
    
    // Use this for initialization
    void OnEnable()
    {
        Debug.Log("Started game state");

        GameState.TimeUpdated += OnTimeUpdated;
        GameState.MapEntitiesUpdated += OnMapUpdated;
        GameState.MinionCountUpdated += OnMinionCountUpdated;
        GameState.ScoreUpdated += OnScoreUpdated;
    }

    void OnTimeUpdated(long updatedtime)
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            var scoreVisualizers = player.GetComponent<TimeVisualizer>();
            if (scoreVisualizers)
            {
                scoreVisualizers.SetTimeInSeconds(updatedtime);
            }
        }
    }

    void OnScoreUpdated(int score)
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            var scoreVisualizers = player.GetComponent<ScoreVisualizer>();
            if (scoreVisualizers)
            {
                scoreVisualizers.ScoreUpdated(score);
            }
        }
    }

    void OnMinionCountUpdated(int numMinions)
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            var minionsVisualizer = player.GetComponent<MinionsVisualizer>();
            if (minionsVisualizer)
            {
                minionsVisualizer.SetNumMinions(numMinions);
            }
        }
    }

    void OnMapUpdated(IReadOnlyList<MapEntity> updatedEntities)
    {
        Debug.Log("Map updated: " + updatedEntities.Count);

        List<MapEntity> playerUpdated = new List<MapEntity>();

        foreach (var updatedEntity in updatedEntities)
        {
            switch (updatedEntity.EntityType)
            {
                case "Player":
                    playerUpdated.Add(updatedEntity);
                    break;
                case "Barrel":

                    break;
            }
        }

        var players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log("Found " + players.Length);
        foreach (var player in players)
        {
            var displayPlayerDirections = player.GetComponent<DisplayPlayerDirections>();
            if (displayPlayerDirections)
            {
                Debug.Log("Calling " + player.name);
                displayPlayerDirections.UpdatePlayerLocations(playerUpdated);
            }
        }
    }
}

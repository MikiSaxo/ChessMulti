using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerIOClient;

public class PlayerIOScript : MonoBehaviour
{
    public static PlayerIOScript Instance;

    private string gameID = "tutoplayerio-w5ldqrjpsuuaowphn1djnw";

    public Connection Pioconnection;
    private List<Message> msgList = new List<Message>(); //  Messsage queue implementation
    private bool joinedroom = false;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Application.runInBackground = true;

        // Create a random userid 
        System.Random random = new System.Random();
        string userid = "Guest" + random.Next(0, 10000);

        Debug.Log("Starting");

        PlayerIO.Authenticate(
            gameID, //Your game id
            "public", //Your connection id
            new Dictionary<string, string>
            {
                //Authentication arguments
                { "userId", userid },
            },
            null, //PlayerInsight segments
            MasterServerJoined,
            delegate(PlayerIOError error) { Debug.Log("Error connecting: " + error.ToString()); }
        );
    }

    void MasterServerJoined(Client client)
    {
        Debug.Log("Successfully connected to Player.IO");

        // Comment out the line below to use the live servers instead of your development server
        client.Multiplayer.DevelopmentServer = new ServerEndpoint("localhost", 8184);

        Debug.Log("CreateJoinRoom");
        //Create or join the room 
        client.Multiplayer.CreateJoinRoom(
            "UnityDemoRoom", //Room id. If set to null a random roomid is used
            "SamServer", //The room type started on the server
            true, //Should the room be visible in the lobby?
            null,
            null,
            RoomJoined,
            delegate(PlayerIOError error) { Debug.Log("Error Joining Room: " + error.ToString()); }
        );
    }

    void RoomJoined(Connection connection)
    {
        Debug.Log("Joined Room.");
        // We successfully joined a room so set up the message handler
        Pioconnection = connection;
        Pioconnection.OnMessage += HandleMessage;
        joinedroom = true;

        Pioconnection.Send("TEST", 42, "michel");
    }

    void HandleMessage(object sender, Message m)
    {
        msgList.Add(m);
    }
    
    private void ProcessMessageQueue()
    {
        // print($"length msgList : {msgList.Count}");
        foreach (Message m in msgList)
        {
            switch (m.Type)
            {
                case "MOVE":
                    // string pieceId = m.GetString(0);
                    // Vector2Int moveCoordinates = new Vector2Int(m.GetInt(1), m.GetInt(2));
                    // UI.DebugMessage($"move piece {pieceId} to {moveCoordinates.x},{moveCoordinates.y}");
                    // Board.MovePiece(pieceId, moveCoordinates);
                    print($"get pos bang");
                    int oldPosX = m.GetInt(1);
                    int oldPosY = m.GetInt(2);
                    int newPosX = m.GetInt(3);
                    int newPosY = m.GetInt(4);
                    GridManager.Instance.MovePawn(new Vector2Int(newPosX, newPosY), new Vector2Int(oldPosX, oldPosY));
                    break;
            }
        }

        // Clear message queue after it's been processed
        msgList.Clear();
    }

    void Update()
    {
        ProcessMessageQueue();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerIOClient;

public class PlayerIOScript : MonoBehaviour
{
    private string gameID = "tutoplayerio-w5ldqrjpsuuaowphn1djnw";

    void Start()
    {
        // PlayerIO.UnityInit(this);

        PlayerIO.Authenticate(
            gameID, //Your game id
            "public", //Your connection id
            new Dictionary<string, string>
            {
                //Authentication arguments
                { "userId", "MikiSaxo" },
            },
            new string[] {  }, //Optional PlayerInsight segments
            delegate(Client client)
            {
                Debug.Log("Connected to PlayerIO!");
                //Success!
            },
            delegate(PlayerIOError error)
            {
                //Error authenticating.
                Debug.LogError("Error connecting to PlayerIO: " + error.Message);
            }
        );
    }

    // void ConnectToPlayerIO()
    // {
    //     // Connect to PlayerIO
    //     PlayerIO.Connect(
    //         gameID,      // Your game ID
    //         "public",            // Connection ID (public for most games)
    //         "user-id",           // User ID (can be any string)
    //         null,                // Auth (null for most games)
    //         null,                // Partner ID (null for most games)
    //         null,                // PlayerInsightSegments (null for most games)
    //         delegate (Client client)
    //         {
    //             Debug.Log("Connected to PlayerIO!");
    //             client.ErrorLog.WriteError("bang");
    //             // Continue with your logic after successful connection
    //         },
    //         delegate (PlayerIOError error)
    //         {
    //             Debug.LogError("Error connecting to PlayerIO: " + error.Message);
    //         }
    //     );
    // }
}
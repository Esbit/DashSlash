using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TotemEntities;
using TotemEntities.DNA;
using TotemServices.DNA;

public class test : MonoBehaviour
{
    /*///Id of your game
    ///Used for legacy records identification
    private string _gameId = "TotemDemo";

    // Start is called before the first frame update
    void Start()
    {
        //Initialize TotemCore
        TotemCore totemCore = new TotemCore(_gameId);


        //Authenticate user through social login in web browser and get user's assets
        totemCore.AuthenticateCurrentUser(Provider.GOOGLE, (user) =>
        {
            //Using default filter with a default avatar model. You can implement your own filters and/or models
            totemCore.GetUserAvatars<TotemDNADefaultAvatar>(user, TotemDNAFilter.DefaultAvatarFilter, (avatars) =>
            {
                foreach (var avatar in avatars)
                {
                    Debug.Log(avatar.ToString());
                }
            });
        });

    }

    public void AddLegacyRecord(object asset, int data)
    {
        totemCore.AddLegacyRecord(asset, data.ToString(), (record) =>
        {
            Debug.Log("Legacy record created");
        });
    }


    public void GetLegacyRecords(object asset, UnityAction<List<TotemLegacyRecord>> onSuccess)
    {
        totemCore.GetLegacyRecords(asset, onSuccess, legacyGameIdInput.text);
    }*/

}
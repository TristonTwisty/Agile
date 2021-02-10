using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardItem : ItemBase
{
    public override void ActivateObject(GameObject source)
    {
        VisualManager.instace.BoardVisual.SetActive(true);
        PlayerRefs.instance.PlayerBoard.transform.position = PlayerRefs.instance.Player.transform.position;
        PlayerRefs.instance.PlayerBoard.transform.rotation = PlayerRefs.instance.Player.transform.rotation;
        PlayerRefs.instance.PlayerBoard.gameObject.SetActive(true);
        PlayerRefs.instance.PlayerCamera.gameObject.SetActive(false);
        PlayerRefs.instance.movementS.enabled = false;
    }

    public override void DeActivateObject(GameObject source)
    {
        VisualManager.instace.BoardVisual.SetActive(false);
        PlayerRefs.instance.Player.transform.position = PlayerRefs.instance.PlayerBoard.transform.position;
        PlayerRefs.instance.Player.transform.rotation = PlayerRefs.instance.PlayerBoard.transform.rotation;
        PlayerRefs.instance.PlayerBoard.gameObject.SetActive(false);
        PlayerRefs.instance.movementS.enabled = true;
        PlayerRefs.instance.PlayerCamera.gameObject.SetActive(true);
    }
}

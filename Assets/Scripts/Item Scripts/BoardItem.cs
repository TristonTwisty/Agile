using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoardItem : ItemBase
{
    //private KeyCode SelectionKey = KeyCode.Alpha3;


    //Added for UI 
    private Scriptforui scriptForUI;
    private void Start()
    {
        scriptForUI = Scriptforui.FindObjectOfType<Scriptforui>();
        scriptForUI.firstItem.rectTransform.sizeDelta= new Vector2(100, 100);
    }


    //public override bool PressSelectKey(KeyCode KeyPressed)
    //{
       // if (KeyPressed == SelectionKey)
      //  {
       //     return true;
      //  }
      //  return false;
   // }

    public override void ActivateObject(GameObject source)
    {
        VisualManager.instace.BoardVisual.SetActive(true);
        PlayerRefs.instance.PlayerBoard.transform.position = PlayerRefs.instance.Player.transform.position;
        PlayerRefs.instance.PlayerBoard.transform.rotation = PlayerRefs.instance.Player.transform.rotation;
        PlayerRefs.instance.PlayerBoard.gameObject.SetActive(true);
        PlayerRefs.instance.PlayerCamera.gameObject.SetActive(false);
        PlayerRefs.instance.movementS.enabled = false;
        PlayerRefs.instance.Player.GetComponent<Animator>().SetBool("Board", true);
        PlayerRefs.instance.Player.GetComponent<Rigidbody>().isKinematic = true;
        VisualManager.instace.BoardVisual.transform.parent = null;
        PlayerRefs.instance.Player.transform.parent = VisualManager.instace.BoardVisual.transform;
        VisualManager.instace.PlayerSurface.gameObject.SetActive(false);
        VisualManager.instace.PlayerJoints.gameObject.SetActive(false);
        VisualManager.instace.BoardVisual.GetComponent<HoverboardMovement>().enabled = true;
    }

    public override void DeActivateObject(GameObject source)
    {
        VisualManager.instace.BoardVisual.SetActive(false);
        PlayerRefs.instance.Player.transform.position = PlayerRefs.instance.PlayerBoard.transform.position;
        PlayerRefs.instance.Player.transform.rotation = PlayerRefs.instance.PlayerBoard.transform.rotation;
        PlayerRefs.instance.PlayerBoard.gameObject.SetActive(false);
        PlayerRefs.instance.movementS.enabled = true;
        PlayerRefs.instance.PlayerCamera.gameObject.SetActive(true);
        PlayerRefs.instance.Player.GetComponent<Animator>().SetBool("Board", false);
        PlayerRefs.instance.Player.GetComponent<Rigidbody>().isKinematic = false;
        VisualManager.instace.BoardVisual.transform.parent = PlayerRefs.instance.Player.transform;
        PlayerRefs.instance.Player.transform.parent = null;
        VisualManager.instace.PlayerSurface.gameObject.SetActive(true);
        VisualManager.instace.PlayerJoints.gameObject.SetActive(true);
        VisualManager.instace.BoardVisual.GetComponent<HoverboardMovement>().enabled = false;
    }
}

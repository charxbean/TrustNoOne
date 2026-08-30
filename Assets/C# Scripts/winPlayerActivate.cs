using UnityEngine;

public class winPlayerActivate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject winPlayer;

    public void ActivateWinPlayer(){
        player.SetActive(false);
        if(winPlayer!= null)
        {
            //Debug.Log("Actiavted");
            winPlayer.SetActive(true);
        }
        else
        {
            Debug.Log("winPlayer null");
        }
    }
}

using UnityEngine;

public class DiceSensor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Scacchiera")
        {
            gameObject.GetComponentInParent<DiceController>().SetCasellaAttuale(other.gameObject.name);
            gameObject.GetComponentInParent<DiceController>().SetFaccia(7 - int.Parse(gameObject.name));
            gameObject.GetComponentInParent<DiceController>().ping = true;
            
        }
    }
}

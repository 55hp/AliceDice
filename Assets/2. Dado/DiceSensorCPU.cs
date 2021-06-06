
using UnityEngine;

public class DiceSensorCPU : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Scacchiera")
        {
            gameObject.GetComponentInParent<DiceCPU>().SetCasellaAttuale(other.gameObject.name);
            gameObject.GetComponentInParent<DiceCPU>().SetFaccia(7 - int.Parse(gameObject.name));
            gameObject.GetComponentInParent<DiceCPU>().ping = true;
        }
    }
}

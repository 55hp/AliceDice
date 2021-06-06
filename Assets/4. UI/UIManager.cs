using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] Text _mosseRestanti;
    [SerializeField] Text _timer;
    [SerializeField] Text _turno;

    [SerializeField] Text _winnerText;


    public void RefreshMosse(int mosseRestanti)
    {
        if (mosseRestanti == 0)
        { 
            _mosseRestanti.text = "";
        }
        else
        {
            _mosseRestanti.text = "" + mosseRestanti;
        }
    }

    public void RefreshTimerText(float actualTime)
    {
        _timer.text = actualTime.ToString("F2");
    }

    public void TurnoPlayer()
    {
        _turno.text = "Tocca a te!";
    }

    public void TurnoCPU()
    {
        _turno.text = "Turno del pc!";
    }

    public void TurnoDiNessuno()
    {
        _turno.text = "";
    }

    public void SetWinner(string winner)
    {
        if (winner != "none")
        {
            _winnerText.text = winner + "wins!";
        }
        else
        {
            _winnerText.text = "";
        }
    }
}

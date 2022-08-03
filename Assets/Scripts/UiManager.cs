using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class UiManager : MonoBehaviour
{
    //Instructions List and in navigator
    [SerializeField]
    List<GameObject> _instructions = new List<GameObject>();
    private int _instructionCount = -1;
    //Tips List and int navigator
    [SerializeField]
    List<GameObject> _tips = new List<GameObject>();
    private int _tipCount = -1;
    //Audio Files For Simulation
    [SerializeField]
    Camera _camLocation;
    [SerializeField]
    List<AudioClip> _instructionsAudio = new List<AudioClip>();
    [SerializeField]
    List<AudioClip> _tipsAudio = new List<AudioClip>();


    public void NextInstruction()
    {
        if(_instructionCount >= -1 && _instructionCount < 2)
        {
            _instructionCount++;
            _instructions[_instructionCount].SetActive(true);
            AudioSource.PlayClipAtPoint(_instructionsAudio[_instructionCount], _camLocation.transform.position);
            _instructions[_instructionCount - 1].SetActive(false);          
        }
        return;
    }

    public void PreviousInstruction()
    {
        if (_instructionCount >= 0 && _instructionCount < 3)
        {
            _instructionCount--;
            _instructions[_instructionCount].SetActive(true);
            AudioSource.PlayClipAtPoint(_instructionsAudio[_instructionCount], _camLocation.transform.position);
            _instructions[_instructionCount + 1].SetActive(false);
        }
        return;
    }

    public void NextTip()
    {
        if(_tipCount >= -1 && _tipCount < 9)
        {
            _tipCount++;
            _tips[_tipCount].SetActive(true);
            AudioSource.PlayClipAtPoint(_tipsAudio[_tipCount], _camLocation.transform.position);
            _tips[_tipCount - 1].SetActive(false);

        }
        return;
    }

    public void PreviousTip()
    {
        if (_tipCount >= 0 && _tipCount < 10)
        {
            _tipCount--;
            _tips[_tipCount].SetActive(true);
            AudioSource.PlayClipAtPoint(_tipsAudio[_tipCount], _camLocation.transform.position);
            _tips[_tipCount + 1].SetActive(false);

        }
        return;
    }




}

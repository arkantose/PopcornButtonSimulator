using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Animator _smallBagAnim;
    [SerializeField]
    Animator _mediumBagAnim;
    [SerializeField]
    Animator _largeBagAnim;
    [SerializeField]
    GameObject _smoke;
    [SerializeField]
    Animator _microwave;
    [SerializeField]
    Camera _mainCam;
    [SerializeField]
    public AudioSource _audioSource;
    [SerializeField]
    List<AudioClip> _audioClips = new List<AudioClip>();
    private bool _microwaveIsOpen = false;
    [SerializeField]
    private TextMesh _microwaveTimer;
    [SerializeField]
    List<GameObject> _popcornBags = new List<GameObject>();
    private bool _popcornIsInMicrowave;
    string[] _popcornButtonSettings= new string[] {"3.5 oz", "2.75 oz", "1.5 oz"};
    [SerializeField]
    public int _popcornButtonSettingsNum = 0;
    //TrackPopcornPoppingTime
    public int _poppingTime = 29;
    private bool _popcornIsPopping;
    [SerializeField]
    private int _isPopcornDone = 0;
    [SerializeField]
    public int _popcornBagNumber;
    [SerializeField]
    public int _popcornButtonNumber;
    [SerializeField]
    GameObject _easterEgg;
    [SerializeField]
    GameObject _mwEasterEgg;
    public void Start()
    {  
        AudioSource.PlayClipAtPoint(_audioClips[5], _mainCam.transform.position);
    }



    //Door Shuts = _microwaveisOpen = false;
    //Popcorn is inside = _popcornIsInMicrowave = true;

    //Controls the microwave doors animation and sound
    public void OpenAndCloseMicrowave()
    {
        if (_microwaveIsOpen == false && _popcornIsPopping == false)
        {
            AudioSource.PlayClipAtPoint(_audioClips[0], _mainCam.transform.position);
            _microwave.SetBool("Open", true);
            _microwaveIsOpen = true;
        }

        else if (_microwaveIsOpen == true && _popcornIsPopping == false)
        {
            AudioSource.PlayClipAtPoint(_audioClips[0], _mainCam.transform.position);
            _microwave.SetBool("Open", false);
            _microwaveIsOpen = false;
        }

    }

    //This starts the microwave process if all conditions are met
    public void StartButton()
    {
        if (_popcornIsPopping == false && _popcornIsInMicrowave == true && _microwaveIsOpen == false)
        {
            Debug.LogError("Start Button Was Pressed");
            _microwaveTimer.text = "00:30";
            AudioSource.PlayClipAtPoint(_audioClips[1], _mainCam.transform.position);
            _audioSource.Play();
            _popcornIsPopping = true;
            StartCoroutine(PopcornIsPopping());
            StartCoroutine(PopcornSoundMaker());
            StartCoroutine(PopcornIsDone());
            switch (_popcornBagNumber)
            {
                case 0:
                    _smallBagAnim.SetBool("CornIsPopped", true);
                    break;
                case 2:
                    _mediumBagAnim.SetBool("CornIsPopped", true);
                    break;
                case 1:
                    _largeBagAnim.SetBool("CornIsPopped", true);
                    break;
            }
        }
    }
    //This coroutine controls how quickly the popcorn popping sound is made
    IEnumerator PopcornSoundMaker()
    {
        float randomTime;

        while(_popcornIsPopping == true)
        {
            randomTime = Random.Range(.5f, 1.25f);
            if (_isPopcornDone >=3 && _isPopcornDone <= 10)
            {
                AudioSource.PlayClipAtPoint(_audioClips[3], _mainCam.transform.position);
                yield return new WaitForSeconds(1f * randomTime);
            }
            else if (_isPopcornDone >= 11 && _isPopcornDone <= 18)
            {
                AudioSource.PlayClipAtPoint(_audioClips[3], _mainCam.transform.position);
                yield return new WaitForSeconds(.5f * randomTime);
            }
            else if (_isPopcornDone >= 19 && _isPopcornDone <= 25)
            {
                AudioSource.PlayClipAtPoint(_audioClips[3], _mainCam.transform.position);
                yield return new WaitForSeconds(.2f * randomTime);
                AudioSource.PlayClipAtPoint(_audioClips[3], _mainCam.transform.position);
            }
            else if (_isPopcornDone >= 26 && _isPopcornDone <=30)
            {
                AudioSource.PlayClipAtPoint(_audioClips[3], _mainCam.transform.position);
                yield return new WaitForSeconds(1.5f * randomTime);
            }
            else if (_isPopcornDone >= 31)
            {
                PopcornIsBurnt();
                yield return new WaitForSeconds(1.5f);
            }
            else
            yield return new WaitForSeconds(1.0f);


        }
    }
    //This coroutine will decide how cooked the popcorn is based off the _isPopcornDone int
    IEnumerator PopcornIsDone()
    {
        while(_popcornIsPopping == true)
        {
            if (_popcornBagNumber == _popcornButtonNumber)
            {
                yield return new WaitForSeconds(1f);
                _isPopcornDone++;
            }
            else if (_popcornBagNumber == 2 && _popcornButtonNumber == 0 )
            {
                yield return new WaitForSeconds(2f);
                _isPopcornDone++;
            }
            else if (_popcornBagNumber == 1 && _popcornButtonNumber == 0 )
            {
                yield return new WaitForSeconds(3f);
                _isPopcornDone++;
            }
            else if (_popcornBagNumber == 0 && _popcornButtonNumber == 2 )
            {
                yield return new WaitForSeconds(.5f);
                _isPopcornDone++;
            }
            else if (_popcornBagNumber == 1 && _popcornButtonNumber == 2 )
            {
                yield return new WaitForSeconds(2f);
                _isPopcornDone++;
            }
            else if (_popcornBagNumber == 0 && _popcornButtonNumber == 1 )
            {
                yield return new WaitForSeconds(.25f);
                _isPopcornDone++;
            }
            else if (_popcornBagNumber == 2 && _popcornButtonNumber == 1 )
            {
                yield return new WaitForSeconds(.5f);
                _isPopcornDone++;
            }
            else
            yield return new WaitForSeconds(1.0f);
        }
    }
    //This Coroutine Controls the microwave cooking the timer
    IEnumerator PopcornIsPopping()
    {
        while(_popcornIsPopping == true)
        {
            _microwaveTimer.text = $"00:{_poppingTime}";
            yield return new WaitForSeconds(1.0f);
            _poppingTime--;

            if(_poppingTime == 0)
            {
                _microwaveTimer.text = $"00:{_poppingTime}";
                yield return new WaitForSeconds(1.0f);
                _microwaveTimer.text = $"D0NE";
                _popcornIsPopping = false;
                _audioSource.Stop();
                AudioSource.PlayClipAtPoint(_audioClips[2], _mainCam.transform.position);
                StopAllCoroutines();
                WeMadeItToTheEnd();
                switch (_popcornBagNumber)
                {
                    case 0:
                        _smallBagAnim.speed = 0;
                        break;
                    case 2:
                        _mediumBagAnim.speed = 0;
                        break;
                    case 1:
                        _largeBagAnim.speed = 0;
                        break;
                }

            }

            if(_poppingTime > 70)
            {
                _popcornIsPopping = false;
                StartCoroutine(EasterEgg());
            }
        }
    }

    //Cute little Easter Egg that blows Shit Up :D
    IEnumerator EasterEgg()
    {
        _easterEgg.SetActive(true);
        AudioSource.PlayClipAtPoint(_audioClips[4], _mainCam.transform.position);
        PopcornIsBurnt();
        yield return new WaitForSeconds(1.0f);
        switch (_popcornBagNumber)
        {
            case 0:
                _popcornBags[1].SetActive(false);
                break;
            case 2:
                _popcornBags[3].SetActive(false);
                break;
            case 1:
                _popcornBags[5].SetActive(false);
                break;
        }
        _mwEasterEgg.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(1);
    }
    //PopcornButtonNumber
    // 3.5 oz = 1, 2.75 oz = 2, 1.5 oz = 0
    public void PopcornButton()
    {
        
        if (_popcornButtonSettingsNum <= 4 && _popcornIsPopping == false)
        {
            Debug.LogError("Popcorn Button Was Pressed");
            AudioSource.PlayClipAtPoint(_audioClips[1], _mainCam.transform.position);
            _microwaveTimer.text = _popcornButtonSettings[_popcornButtonSettingsNum];
            _popcornButtonSettingsNum++;
            _popcornButtonNumber = _popcornButtonSettingsNum;
            if (_popcornButtonSettingsNum == 3)
            {
                _popcornButtonSettingsNum = 0;
                _popcornButtonNumber = _popcornButtonSettingsNum;
            }
        }
    }
    //Stops the microwave
    public void StopButton()
    {
        if (_popcornIsPopping == true)
        {
            Debug.LogError("Stop Button Was Pressed");
            AudioSource.PlayClipAtPoint(_audioClips[1], _mainCam.transform.position);
            _popcornIsPopping = false;
            StopAllCoroutines();
            _audioSource.Stop();
            WeMadeItToTheEnd();
            switch (_popcornBagNumber)
            {
                case 0:
                    _smallBagAnim.speed = 0;
                    break;
                case 2:
                    _mediumBagAnim.speed = 0;
                    break;
                case 1:
                    _largeBagAnim.speed = 0;
                    break;
            }
        }
    }
    //Adds ten seconds to the cooking time
    public void Add30Button()
    {

        if(_popcornIsPopping == true && _poppingTime < 90)
        {
            Debug.LogError("Add 30 Button Was Pressed");
            AudioSource.PlayClipAtPoint(_audioClips[1], _mainCam.transform.position);
            _poppingTime += 10;
            _microwaveTimer.text = $"00:{_poppingTime}";
        }
    }
    //Subtracts ten seconds from the cooking time
    public void TimerButton()
    {
        if(_popcornIsPopping == true && _poppingTime > 10)
        {
            Debug.LogError("Timer Button Was Pressed");
            AudioSource.PlayClipAtPoint(_audioClips[1], _mainCam.transform.position);
            _poppingTime -= 10;
            _microwaveTimer.text = $"00:{_poppingTime}";
        }
    }
    // Places the 1.5oz popcorn bag in the microwave and sets the bag number
    public void SmallestBag()
    {
        if (_microwaveIsOpen == true && _popcornIsInMicrowave == false)
        {
            AudioSource.PlayClipAtPoint(_audioClips[1], _mainCam.transform.position);
            _popcornBags[0].SetActive(false);
            _popcornBags[1].SetActive(true);
            _popcornIsInMicrowave = true;
            _popcornBagNumber = 0;
        }
        else
            return;
    }
    // Places the 2.75oz popcorn bag in the microwave and sets the bag number
    public void MediumBag()
    {
        if (_microwaveIsOpen == true && _popcornIsInMicrowave == false)
        {
            AudioSource.PlayClipAtPoint(_audioClips[1], _mainCam.transform.position);
            _popcornBags[2].SetActive(false);
            _popcornBags[3].SetActive(true);
            _popcornIsInMicrowave = true;
            _popcornBagNumber = 2;
        }
        else
            return;
    }
    // Places the 3.5oz popcorn bag in the microwave and sets the bag number
    public void LargeBag()
    {
        if (_microwaveIsOpen == true && _popcornIsInMicrowave == false)
        {
            AudioSource.PlayClipAtPoint(_audioClips[1], _mainCam.transform.position);
            _popcornBags[4].SetActive(false);
            _popcornBags[5].SetActive(true);
            _popcornIsInMicrowave = true;
            _popcornBagNumber = 1;
        }
        else
            return;
    }
    //This method will reload the scene and reset everything
    public void ResetGame()
    {
        SceneManager.LoadScene(1);
    }
    //This method will register for when the popcorn is burnt.
    public void PopcornIsBurnt()
    {
        if (_isPopcornDone > 30)
        {
            _smoke.SetActive(true);

        }
    }

    public void WeMadeItToTheEnd()
    {
        if (_isPopcornDone >0 && _isPopcornDone <18)
        {
            _popcornBags[9].SetActive(true);
            AudioSource.PlayClipAtPoint(_audioClips[8], _mainCam.transform.position);
        }
        else if (_isPopcornDone >= 18 && _isPopcornDone < 25)
        {
            _popcornBags[8].SetActive(true);
            AudioSource.PlayClipAtPoint(_audioClips[7], _mainCam.transform.position);
        }
        else if (_isPopcornDone >= 25 && _isPopcornDone <= 30)
        {
            _popcornBags[6].SetActive(true);
            AudioSource.PlayClipAtPoint(_audioClips[9], _mainCam.transform.position);
        }
        else if (_isPopcornDone > 30)
        {
            _popcornBags[7].SetActive(true);
            AudioSource.PlayClipAtPoint(_audioClips[6], _mainCam.transform.position);
        }
    }
}

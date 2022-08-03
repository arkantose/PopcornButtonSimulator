using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [SerializeField]
    Camera _cam;
    Button button = null;
    // Start is called before the first frame update



    // Update is called once per frame
    void Update()
    {
        //Gamepad.current.aButton.wasPressedThisFrame

        Debug.DrawLine(transform.position, transform.forward *1000f, Color.red);
        if (RayDetector() && Keyboard.current.spaceKey.wasPressedThisFrame)
        {

            button?.onClick.Invoke();
            
        }
    }
    //Detects the button when looked at and changes its colors as well as changing its color back to the
    //Original when it is no longer in the rays view.
    private bool RayDetector()
    {
        Ray ray;
        ray = new Ray(transform.position, transform.forward * 1000f);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit))
        {
            button = hit.collider.GetComponent<Button>();
            if (button != null)
            {
                var buttonColor = button.colors;
                buttonColor.normalColor = Color.yellow;
                button.colors = buttonColor;
                return true;
            }
            else
                return false;
        }
        else
        {
            if(button != null)
            {
                var buttonColor = button.colors;
                buttonColor.normalColor = Color.white;
                button.colors = buttonColor;
                button = null;
            }
            return false;
        }
    }
}



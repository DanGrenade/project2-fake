using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class InputHandler
{
	
	FNode localPosition;
	
	enum InputDevice
	{
		MouseAndKeyboard,
		Gamepad
	};
	InputDevice currentInputDevice = InputDevice.MouseAndKeyboard;
	
	#region Keyboard inputs
	KeyCode leftKey = KeyCode.A;
	KeyCode rightKey = KeyCode.D;
	KeyCode upKey = KeyCode.W;
	KeyCode downKey = KeyCode.S;
	
	KeyCode primaryKey = KeyCode.Mouse0;
	KeyCode secondaryKey = KeyCode.Mouse1;
	
	KeyCode selectKey = KeyCode.E;
	KeyCode backKey = KeyCode.F;
	KeyCode escKey = KeyCode.Escape;
	#endregion
	
	#region Controller inputs
	string leftRightControl = "LeftRightController";
	string upDownControl = "UpDownController";
	
	KeyCode primaryControl = KeyCode.Joystick1Button4;
	KeyCode secondaryControl = KeyCode.Joystick1Button5;
	
	KeyCode menuControl = KeyCode.Joystick1Button0;
	KeyCode backControl = KeyCode.Joystick1Button1;
	KeyCode escControl = KeyCode.Joystick1Button7;
	#endregion


    #region Control parameters
	
	#region Rotation parameters
	Vector2 mousePosition;
	
	Vector2 rightStickRotation;
	
	public Vector2 actionDirection;
	
	#endregion
	
    #region Movement parameters
    Vector2 horizontalVector = new Vector2(1, 0);
    Vector2 verticalVector = new Vector2(0, 1);

    public Vector2 inputVector;
    float x;
    float y;
    float moveSpeedParam;
    #endregion

    #region Item activation params
    public bool primaryActivated;
	public bool primaryHold;
    
	public bool secondaryActivated;
	public bool secondaryHold;
    #endregion

    #region Menu params
	public bool selectButtonPressed = false;
	public bool backButtonPressed = false;
	public bool escButtonPressed = false;
	bool escButtonHeld = false;

    public bool pressedUp = false;
    public bool holdingUp = false;
    public bool pressedDown = false;
    public bool holdingDown = false;
	
	public bool pressedLeft = false;
	public bool holdingLeft = false;
	public bool pressedRight = false;
	public bool holdingRight = false;

    float threshold = 0.3f;

    #endregion

    #endregion

    public InputHandler(FNode playerChar)
	{
		localPosition = playerChar;
	}
	
	
	// Update is called once per frame
	public void HandleUpdate ()
    {
        inputVector = Vector2.zero;
		
		
		if(currentInputDevice == InputDevice.MouseAndKeyboard)
		{
        #region Keyboard controls
			
		#region Rotation controls	

		mousePosition = localPosition.GetLocalMousePosition();
						
		if(mousePosition != Vector2.zero)
		{
			mousePosition.Normalize ();
		}
			
		actionDirection = mousePosition;
			
		#endregion
			
        #region Movement controls

        if (Input.GetKey(leftKey))
        {
            inputVector -= horizontalVector;
        }
        if (Input.GetKey(rightKey))
        {
            inputVector += horizontalVector;
        }
        if (Input.GetKey(upKey))
        {
            inputVector += verticalVector;
        }
        if (Input.GetKey(downKey))
        {
            inputVector -= verticalVector;
        }

        if (inputVector != Vector2.zero)
        {
            inputVector.Normalize();
        }


        #endregion

        #region ItemActivation
			
		if(Input.GetKey (primaryKey)) primaryHold = true;
			else primaryHold = false;
			
        if (Input.GetKeyDown(primaryKey)) primaryActivated = true;
			else primaryActivated = false;
        
		if(Input.GetKey (secondaryKey)) secondaryHold = true;
			else secondaryHold = false;

        if (Input.GetKeyDown(secondaryKey)) secondaryActivated = true;
			else secondaryActivated = false;

        #endregion

        #region Menu controls
			
		if (Input.GetKeyDown(selectKey)) selectButtonPressed = true;
    		else selectButtonPressed = false;
			
		if(Input.GetKeyDown (backKey)) backButtonPressed = true;
			else backButtonPressed = false;
			
		if(Input.GetKeyDown (escKey)) escButtonPressed = true;
			else escButtonPressed = false;

        if (Input.GetKey(upKey))
        {
            if (holdingUp == false)
            {
                pressedUp = true;
                holdingUp = true;
            }
            else if (pressedUp)
            {
                pressedUp = false;
            }
        }
        else
        {
            pressedUp = false;
            holdingUp = false;
        }

        if (Input.GetKey(downKey))
		{
				
            if (holdingDown == false)
            {
                pressedDown = true;
                holdingDown = true;
            }
            else if (pressedDown)
            {
                pressedDown = false;
            }
        }
        else
        {
            pressedDown = false;
            holdingDown = false;
        }
			
			
		if(Input.GetKey(rightKey))
		{
			if(holdingRight == false)
			{
				pressedRight = true;
				holdingRight = true;
			}
			else if(pressedRight)
			{
				pressedRight = false;
			}
		}
		else
		{
			pressedRight = false;
			holdingRight = false;
		}
			
		if(Input.GetKey(leftKey))
		{
			if(holdingLeft == false)
			{
				pressedLeft  = true;
				holdingLeft = true;
			}
			else if(pressedLeft)
			{
				pressedLeft = false;
			}
		}
		else
		{
			pressedLeft = false;
			holdingLeft = false;
		}

        #endregion 

        #endregion
		}
		
		if(currentInputDevice == InputDevice.Gamepad)
		{
        #region Controller controls
			
		#region Rotation Controls
		x = Input.GetAxis ("RightStick_Horizontal");
		y = Input.GetAxis ("RightStick_Verticle");
			
		rightStickRotation = new Vector2(x, y);
			
		
			
		if(rightStickRotation != Vector2.zero)
		{
			rightStickRotation.Normalize();
		}
			
		actionDirection = rightStickRotation;	
		
		#endregion
			
        #region Movement controls

        x = Input.GetAxis("LeftStick_Horizontal");
        y = Input.GetAxis("LeftStick_Verticle");
			
        moveSpeedParam = x + y;

        inputVector = new Vector2(x, y);

        if (inputVector != Vector2.zero)
        {
            inputVector.Normalize();
        }

        #endregion

        #region ItemActivation
		
		if(Input.GetKey(primaryControl)) primaryHold = true;
			else primaryHold = false;
			
			
        if (Input.GetKeyDown(primaryControl)) primaryActivated = true;
        	else primaryActivated = false;
			
		if(Input.GetKey(secondaryControl)) secondaryHold = true;
			else secondaryHold = false;			
			
        if (Input.GetKeyDown(secondaryControl)) secondaryActivated = true;
       		else secondaryActivated = false;
			
        #endregion

        #region Menu controls
			
		if (Input.GetKeyDown(menuControl)) selectButtonPressed = true;
    		else selectButtonPressed = false;

        if (Mathf.Abs(Input.GetAxis("LeftStick_Verticle")) >= threshold)
        {
            if (Input.GetAxis("LeftStick_Verticle") > 0)
            {
                if (holdingUp == false)
                {
                    pressedUp = true;
                    holdingUp = true;
                }
                else if (pressedUp)
                {
                    pressedUp = false;
                }
            }
            else
            {
                pressedUp = false;
                holdingUp = false;
            }


            if (Input.GetAxis("LeftStick_Verticle") < 0)
            {
                if (holdingDown == false)
                {
                    pressedDown = true;
                    holdingDown = true;
                }
                else if (pressedDown)
                {
                    pressedDown = false;
                }
            }
            else
            {
                pressedDown = false;
                holdingDown = false;
            }
        }


        #endregion

        #endregion
		}

    }
	
	public void AddKeys(List<KeyCode> keys)
	{
		leftKey = keys[0];
		rightKey = keys[1];
		upKey = keys[2];
		downKey = keys[3];
		
		primaryKey = keys[4];
		secondaryKey = keys[5];
		
		selectKey = keys[6];
		backKey = keys[7];
		escKey = keys[8];	
		
	}
	
	public void AddButtons(List<KeyCode> button)
	{
		primaryControl = button[0];
		secondaryControl = button[1];
		
		menuControl = button[2];
		backControl = button[3];
		escControl = button[4];
	}
	
	public List<string> GetKeyNames()
	{
		List<string> Keys = new List<string>();
		
		Keys.Add("Move left");
		Keys.Add("Move right");
		Keys.Add("Move up");
		Keys.Add("Move down");
		
		Keys.Add("Primary active");
		Keys.Add("Secondary active");
		
		Keys.Add("Select");
		Keys.Add("Back");
		Keys.Add("Escape");
		
		return Keys;
	}
	
	public List<string> GetButtonNames()
	{
		List<string> Buttons = new List<string>();
		
		Buttons.Add("Primary Active");
		Buttons.Add("Seconday Active");
		
		Buttons.Add("Select");
		Buttons.Add("Back");
		Buttons.Add("Escape");
		
		return Buttons;
	}
	
	
	public void ChangeLocal(FNode local)
	{
		localPosition = local;
	}
	
}

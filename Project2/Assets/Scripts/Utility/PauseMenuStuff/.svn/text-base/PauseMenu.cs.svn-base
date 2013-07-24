using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class PauseMenu
{
	public enum PauseState
	{
		Off = 0, Main = 1, Controls = 2, Volume = 3
	};
	PauseState currentState = PauseState.Off;
	
	List<PauseMainOption> mainOptions = new List<PauseMainOption>();
	
	List<ControlOption> keyOptions = new List<ControlOption>();
	List<ControlOption> controlOptions = new List<ControlOption>();
	
	
	bool editingControl = false;
	
	int mainSelection = 0;
	int subMenuSelection = 0;
	
	public PauseMenu()
	{
		mainOptions.Add(new PauseMainOption("Resume", PauseState.Off));
		mainOptions.Add(new PauseMainOption("Controls", PauseState.Controls));	
		
		ReadFromXML ();
		
	}
	
	
	
	
	public void HandleUpdate()
	{
		switch(currentState)
		{
		case PauseState.Off:
			break;
		case PauseState.Main:	
			#region Pause Main Page
			#region Go up and down
			if(Overlord.CurrentLevel.input.pressedDown)
			{
				mainOptions[mainSelection].Deselect();
				
				mainSelection++;
				if(mainSelection >= mainOptions.Count)
				{
					mainSelection = 0;
				}
				
				mainOptions[mainSelection].SetToSelected();
			}
			else if(Overlord.CurrentLevel.input.pressedUp)
			{
				mainOptions[mainSelection].Deselect();
				
				mainSelection--;
				if(mainSelection < 0)
				{
					mainSelection = mainOptions.Count - 1;
				}			
				
				mainOptions[mainSelection].SetToSelected();
			}
			#endregion
			
			
			#region Button Presses
			if(Overlord.CurrentLevel.input.escButtonPressed
				|| Overlord.CurrentLevel.input.backButtonPressed)
			{
				currentState = PauseState.Off;
				TurnOffMenu();
				
			}			
			else if(Overlord.CurrentLevel.input.selectButtonPressed)
			{
				currentState = mainOptions[mainSelection].ReturnNext();
				TurnOffMain ();
				subMenuSelection = 0;
				
				switch(currentState)
				{
				case PauseState.Controls:
					TurnOnControls();
					break;
				case PauseState.Volume:
					TurnOnVolume();
					break;					
				}
				
				
				
			}
			#endregion
			#endregion
			
			break;
		case PauseState.Controls:
			controlOptions[subMenuSelection].SetToSelected();
			
			if(editingControl)
			{
				if(Input.anyKey)
				{
					controlOptions[subMenuSelection].SetKey(FetchKey());
				}				
			}
			else
			{
				if(Overlord.CurrentLevel.input.pressedUp)
				{
					controlOptions[subMenuSelection].Deselect();
					
					subMenuSelection++;
					if(subMenuSelection >= controlOptions.Count)
					{
						subMenuSelection = 0;
					}
					controlOptions[subMenuSelection].SetToSelected();
				}
				else if(Overlord.CurrentLevel.input.pressedDown)
				{
					controlOptions[subMenuSelection].Deselect();
					
					subMenuSelection--;
					if(subMenuSelection < 0)
					{
						subMenuSelection = controlOptions.Count - 1;
					}
					controlOptions[subMenuSelection].SetToSelected();
				}
				
				if(Overlord.CurrentLevel.input.backButtonPressed)
				{
					TurnOffControls();
					TurnOnMain();
				}
				else if(Overlord.CurrentLevel.input.selectButtonPressed)
				{
					editingControl = true;
					controlOptions[subMenuSelection].EditOption();
				}
				
			}
			break;
		case PauseState.Volume:
			
			break;
		}
		
		
	}
	
	void TurnOffMenu()
	{
		TurnOffMain();
		TurnOffControls();
		TurnOffVolume ();
		subMenuSelection = 0;		
	}
	
	public void TurnOnMenu()
	{
		TurnOnMain();
	}
	
	void TurnOnMain()
	{
		foreach(PauseMainOption option in mainOptions)
		{
			option.SetToOn ();
		}
		
		mainOptions[mainSelection].SetToSelected();
	}
	
	void TurnOffMain()
	{
		foreach(PauseMainOption option in mainOptions)
		{
			option.SetToOff ();
		}
	}
	
	void TurnOnControls()
	{
		foreach(ControlOption option in controlOptions)
		{
			
		}
	}
	
	void TurnOffControls()
	{
		subMenuSelection = 0;
		
	}
	
	void TurnOnVolume()
	{
		
	}
	
	void TurnOffVolume()
	{
		subMenuSelection = 0;
		
	}
	
	void ReadFromXML()
	{
		XmlDocument xmldoc = new XmlDocument();
		
		List<string> keyNames = Overlord.CurrentLevel.input.GetKeyNames();
		List<string> buttonNames = Overlord.CurrentLevel.input.GetButtonNames();
		
		List<KeyCode> keys = new List<KeyCode>();
		List<KeyCode> buttons = new List<KeyCode>();
		
		TextAsset textAsset = (TextAsset)Resources.Load("PlayerOptions");
		xmldoc.LoadXml(textAsset.text);
		
		XmlNodeList mylist = xmldoc.GetElementsByTagName("ControlType");
		
		KeyCode key;
		
		for(int k = 0; k < mylist.Count; k++)
		{
			XmlNodeList buttonList = mylist[k].ChildNodes;
			
			for(int i = 0; i < buttonList.Count; i++)
			{
				key = (KeyCode)Enum.Parse(typeof(KeyCode), buttonList[i].InnerText);
				
				if(k == 0)	keys.Add(key);
				else	buttons.Add(key);
			}	
		}
		
		for(int i = 0; i < keys.Count; i++)
		{
			keyOptions.Add(new ControlOption(keys[i], keyNames[i]));
		}
		for(int i = 0; i < buttons.Count; i++)
		{
			controlOptions.Add(new ControlOption(buttons[i], keyNames[i]));
		}	
		
	}
	
	
	
	
	
	 KeyCode FetchKey()
     {
       int e = System.Enum.GetNames(typeof(KeyCode)).Length;
       for(int i = 0; i < e; i++)
       {
         if(Input.GetKey((KeyCode)i))
         {
          return (KeyCode)i;
         }
       }

       return KeyCode.None;
	}
	
	
}

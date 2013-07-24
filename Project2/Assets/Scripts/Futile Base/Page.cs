using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class Page : FContainer
{
	private XmlDocument xmldoc = new XmlDocument();
	public InputHandler input;
	
	public Page()
	{
		input = new InputHandler(this);
		LoadXML();
	}
	
	virtual public void Start()
	{
		
	}
	
	virtual public void HandleUpdate()
	{
		
	}
	
	private void LoadXML()
	{
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
		
		input.AddKeys(keys);
		input.AddButtons(buttons);
	}
}
using System;
using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public enum PageType
{
	None,
	Menu,
	Test
}

public class Overlord : MonoBehaviour
{
	public static Overlord instance;
	public static System.Random random = new System.Random();
	public static Level CurrentLevel;
	public int frameCount;
	
	public RoomGenerator roomGenerator;
	
	private PageType _currentPageType = PageType.None;
	private Page _currentPage = null;
	private FStage _stage;
	
	public FAtlasManager am;
	
	uint bankID;
	
	public GameObject SoundPlayer;
	
	void Start () 
	{	
		FutileParams fparams = new FutileParams(true, true, false, false);
		fparams.shouldLerpToNearestResolutionLevel = false;
		
		fparams.AddResolutionLevel(900.0f,	1.0f, 1.0f, ""); 
		
		fparams.origin = new Vector2(0.0f, 1.0f);
		
		Futile.instance.Init(fparams);
		
		roomGenerator = new RoomGenerator();
	
		instance = this;	
		
		//am = new FAtlasManager();
		
		//am.LoadAtlas("Atlases/testAtlas");
		//am.LoadFont("Museo", "Museo" + Futile.resourceSuffix, "Atlases/Museo" + Futile.resourceSuffix, 70.0f, 20.0f);
		
		Futile.atlasManager.LoadAtlas("Atlases/64Atlas");
		//Futile.atlasManager.LoadFont("Museo", "Museo" + Futile.resourceSuffix, "Atlases/Museo" + Futile.resourceSuffix, 70.0f, 20.0f);
		
		_stage = Futile.stage;
		
		GoToPage(PageType.Test);
	}
	
	public void GoToPage (PageType pageType)
	{
		if(_currentPageType == pageType) return; //we're already on the same page, so don't bother doing anything
		
		Page pageToCreate = null;
		
		if(pageType == PageType.Test)
		{
			pageToCreate = new Test();
		}
		
		if(pageType == PageType.Menu)
		{
			//pageToCreate = new Menu();
		}
		
		if(pageToCreate != null) //destroy the old page and create a new one
		{
			_currentPageType = pageType;	
			
			if(_currentPage != null)
			{
				_stage.RemoveChild(_currentPage);
			}
			
			_currentPage = pageToCreate;
			_stage.AddChild(_currentPage);
			CurrentLevel = _currentPage as Level;
			_currentPage.Start();
		}
	}
	
	void Update()
	{
		if(frameCount <= 60){frameCount++;}
		else{frameCount = 0;}

		_currentPage.HandleUpdate();
	}	
}

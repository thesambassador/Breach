// Bryan Raymond
// 2/17/14
using UnityEngine;
using System.Collections;

using UnityEngine;
using Assets.Modules.Managers;





public class PlayerStatsContext : EZData.Context
{
	#region Property Name
	private readonly EZData.Property<string> _privateNameProperty = new EZData.Property<string> ();
	public EZData.Property<string> NameProperty { get { return _privateNameProperty; } }
	public string pName
	{
		get { return NameProperty.GetValue ();}
		set { NameProperty.SetValue (value);}
	}
	#endregion

	#region Property Title
	private readonly EZData.Property<string> _privateTitleProperty = new EZData.Property<string> ();
	public EZData.Property<string> TitleProperty { get { return _privateTitleProperty; } }
	public string pTitle
	{
		get { return TitleProperty.GetValue ();}
		set { TitleProperty.SetValue (value);}
	}
	#endregion

	// Current experience
	#region Property Experience
	private readonly EZData.Property<float> _privateExperienceProperty = new EZData.Property<float> ();
	public EZData.Property<float> ExperienceProperty { get { return _privateExperienceProperty; } }
	public float pExperience
	{
		get { return ExperienceProperty.GetValue ();}
		set { ExperienceProperty.SetValue (value);}
	}
	#endregion

	#region Property ExperienceToLevel
	private readonly EZData.Property<float> _privateExperienceToLevelProperty = new EZData.Property<float> ();
	public EZData.Property<float> ExperienceToLevelProperty { get { return _privateExperienceToLevelProperty; } }
	public float pExperienceToLevel
	{
		get { return ExperienceToLevelProperty.GetValue ();}
		set { ExperienceToLevelProperty.SetValue (value);}
	}
	#endregion

	#region Property Mana
	private readonly EZData.Property<float> _privateManaProperty = new EZData.Property<float> ();
	public EZData.Property<float> ManaProperty { get { return _privateManaProperty; } }
	public float pMana
	{
		get { return ManaProperty.GetValue ();}
		set { ManaProperty.SetValue (value);}
	}
	#endregion

	// Current health
	#region Property Health
	private readonly EZData.Property<float> _privateHealthProperty = new EZData.Property<float> ();
	public EZData.Property<float> HealthProperty { get { return _privateHealthProperty; } }
	public float pHealth
	{
		get { return HealthProperty.GetValue ();}
		set { HealthProperty.SetValue (value);}
	}
	#endregion

	#region Property MaxHealth
	private readonly EZData.Property<float> _privateMaxHealthProperty = new EZData.Property<float> ();
	public EZData.Property<float> MaxHealthProperty { get { return _privateMaxHealthProperty; } }
	public float pMaxHealth
	{
		get { return MaxHealthProperty.GetValue ();}
		set { MaxHealthProperty.SetValue (value);}
	}
	#endregion

	// Strength is just a placeholder until we decide on the stats
	#region Property Strength
	private readonly EZData.Property<int> _privateStrengthProperty = new EZData.Property<int> ();
	public EZData.Property<int> StrengthProperty { get { return _privateStrengthProperty; } }
	public int pStrength
	{
		get { return StrengthProperty.GetValue ();}
		set { StrengthProperty.SetValue (value);}
	}
	#endregion

	#region Property StatPoints
	private readonly EZData.Property<int> _privateStatPointsProperty = new EZData.Property<int> ();
	public EZData.Property<int> StatPointsProperty { get { return _privateStatPointsProperty; } }
	public int pStatPoints
	{
		get { return StatPointsProperty.GetValue ();}
		set { StatPointsProperty.SetValue (value);}
	}
	#endregion

	public void increment()
	{
		if(pStatPoints>0)
		{
			pStrength += 1;
			pStatPoints -=1;
		}
	}

	public void decrement()
	{
		if(pStrength>0)
		{
			pStrength-=1;
			pStatPoints +=1;
		}
	}
}



using Alchemy.Models;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Alchemy.Controllers
{
	public class InputHandler : MonoBehaviour
	{
		int previousGameSpeed;

		void Update()
		{
			if (Input.GetButtonDown("Increase Game Speed"))
			{
				GameManager.instance.world.Speed += 1;
			}

			if (Input.GetButtonDown("Decrease Game Speed"))
			{
				GameManager.instance.world.Speed -= 1;
			}

			if (Input.GetButtonDown("Jump") && EventSystem.current.currentSelectedGameObject == null)
			{
				if (GameManager.instance.world.Speed != 0)
				{
					previousGameSpeed = GameManager.instance.world.Speed;
					GameManager.instance.world.Speed = 0;
				}
				else
				{
					GameManager.instance.world.Speed = previousGameSpeed;
				}
			}

			if (Input.GetKeyDown(KeyCode.Q))
			{
				for (int i = 0; i < 100; i++)
				{
					var herb = GameManager.instance.world.HerbDatabase[GameManager.instance.world.Random.Next(GameManager.instance.world.HerbDatabase.Length)];
					GameManager.instance.world.Shop.DeliverIngredient(herb);
				}
				for (int i = 0; i < 100; i++)
				{
					var flask = (Flask)GameManager.instance.world.FlaskDatabase[GameManager.instance.world.Random.Next(GameManager.instance.world.FlaskDatabase.Length)].Clone();
					GameManager.instance.world.Shop.PurchaseFlask(flask);
				}
			}

			if (Input.GetKeyDown(KeyCode.F5))
			{
				File.WriteAllText("game.json", JsonUtility.ToJson(GameManager.instance.world));
			}


			if (Input.GetKeyDown(KeyCode.A))
			{
				int dropdownOption = 0;
				string name = "";
				var changeName = UserInterface.CreateAlert(new AlertData()
				{
					title = "Name",
					message = "Enter your name",
					inputs = new IInputData[]
					{
						new DropdownData()
						{
							options = new List<Dropdown.OptionData>()
							{
								new Dropdown.OptionData("Mr."),
								new Dropdown.OptionData("Ms."),
								new Dropdown.OptionData("Mrs.")
							},
							onValueChanged = (value) =>
							{
								dropdownOption = value;
							}
						},
						new InputFieldData()
						{
							onEndEdit = (value) =>
							{
								string prefix = "";
								switch (dropdownOption)
								{
									case 0:
										prefix = "Mr.";
										break;
									case 1:
										prefix = "Ms.";
										break;
									case 2:
										prefix = "Mrs.";
										break;
								}
								name = prefix + " " + value;
							}
						}
					}
				});
				changeName.AddButton(new ButtonData()
				{
					text = "Cancel",
					onClick = () =>
					{
						changeName.onClose.Invoke(changeName);
					}
				});
				changeName.AddButton(new ButtonData()
				{
					text = "Ok",
					onClick = () =>
					{
						this.name = name;
						changeName.onClose.Invoke(changeName);
						var success = UserInterface.CreateAlert(new AlertData()
						{
							title = "Name",
							message = "Name changed"
						});
						success.AddButton(new ButtonData()
						{
							text = "Ok",
							onClick = () =>
							{
								success.onClose.Invoke(success);
							}
						});
					}
				});
			}

			if (Input.GetKeyDown(KeyCode.B))
			{
				var things = new BoolThing[10];
				for (int i = 0; i < things.Length; i++)
				{
					things[i] = new BoolThing()
					{
						name = "Thing " + i,
						value = i % 2 == 0
					};
				}

				var checklist = UserInterface.CreateChecklist(new ChecklistData()
				{
					title = "Checklist",
					onClickOk = () =>
					{
						foreach (var thing in things)
						{
							Debug.Log(thing.name + " is " + thing.value);
						}
					}
				});
				foreach (var thing in things)
				{
					checklist.AddToggle(new ToggleData()
					{
						text = thing.name,
						isOn = thing.value,
						onValueChanged = (value) =>
						{
							thing.value = value;
						}
					});
				}
			}
		}

		public class BoolThing
		{
			public string name;
			public bool value;
		}
	}
}
using Alchemy.Models;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Alchemy.Controllers {
    public class InputHandler : MonoBehaviour {
        private int previousGameSpeed;

        private void Update() {
            if (Input.GetButtonDown("Increase Game Speed")) {
                GameManager.Instance.World.Speed += 1;
            }

            if (Input.GetButtonDown("Decrease Game Speed")) {
                GameManager.Instance.World.Speed -= 1;
            }

            if (Input.GetButtonDown("Jump") && EventSystem.current.currentSelectedGameObject == null) {
                if (GameManager.Instance.World.Speed != 0) {
                    previousGameSpeed = GameManager.Instance.World.Speed;
                    GameManager.Instance.World.Speed = 0;
                } else {
                    GameManager.Instance.World.Speed = previousGameSpeed;
                }
            }

            if (Input.GetKeyDown(KeyCode.Z)) {
                var apothecary = new Apothecary("Apothecary", 10);

                GameManager.Instance.World.ReceiveApplication(apothecary);
                GameManager.Instance.World.Shop.HireEmployee(apothecary);

                var herbalist = new Herbalist("Herbalist", 10);

                GameManager.Instance.World.ReceiveApplication(herbalist);
                GameManager.Instance.World.Shop.HireEmployee(herbalist);

                var shopkeeper = new Shopkeeper("Shopkeeper", 10);

                GameManager.Instance.World.ReceiveApplication(shopkeeper);
                GameManager.Instance.World.Shop.HireEmployee(shopkeeper);
            }

            if (Input.GetKeyDown(KeyCode.X)) {
                for (int i = 0; i < 10; i++) {
                    var herb = (Herb)GameManager.Instance.World.HerbDatabase[GameManager.Instance.World.Random.Next(GameManager.Instance.World.HerbDatabase.Length)].Clone();

                    GameManager.Instance.World.Shop.DeliverIngredient(herb);
                }

                for (int i = 0; i < 10; i++) {
                    var flask = (Flask)GameManager.Instance.World.FlaskDatabase[GameManager.Instance.World.Random.Next(GameManager.Instance.World.FlaskDatabase.Length)].Clone();

                    GameManager.Instance.World.Shop.PurchaseFlask(flask);
                }
            }

            if (Input.GetKeyDown(KeyCode.Y)) {
                var flask = GameManager.Instance.World.Shop.Flasks[GameManager.Instance.World.Random.Next(GameManager.Instance.World.Shop.Flasks.Count)];
                var ingredients = new Ingredient[] {
                        GameManager.Instance.World.Shop.Ingredients[GameManager.Instance.World.Random.Next(GameManager.Instance.World.Shop.Ingredients.Length)],
                        GameManager.Instance.World.Shop.Ingredients[GameManager.Instance.World.Random.Next(GameManager.Instance.World.Shop.Ingredients.Length)]
                    };

                GameManager.Instance.World.Shop.ResearchPotion(flask, null, ingredients);

            }

            if (Input.GetKeyDown(KeyCode.F5)) {
                File.WriteAllText("game.json", JsonUtility.ToJson(GameManager.Instance.World));
            }

            if (Input.GetKeyDown(KeyCode.A)) {
                int dropdownOption = 0;
                string name = "";
                var changeName = UserInterface.Instance.CreateAlert(new AlertData() {
                    title = "Name",
                    message = "Enter your name",
                    inputs = new IInputData[] {
                        new DropdownData() {
                            options = new List<Dropdown.OptionData>() {
                                new Dropdown.OptionData("Mr."),
                                new Dropdown.OptionData("Ms."),
                                new Dropdown.OptionData("Mrs.")
                            },
                            onValueChanged = (value) => {
                                dropdownOption = value;
                            }
                        },
                        new InputFieldData() {
                            onEndEdit = (value) => {
                                string prefix = "";

                                switch (dropdownOption) {
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
                changeName.AddButton(new ButtonData() {
                    text = "Cancel",
                    onClick = () => {
                        changeName.Close();
                    }
                });
                changeName.AddButton(new ButtonData() {
                    text = "Ok",
                    onClick = () => {
                        this.name = name;

                        changeName.Close();

                        var success = UserInterface.Instance.CreateAlert(new AlertData() {
                            title = "Name",
                            message = "Name changed"
                        });

                        success.AddButton(new ButtonData() {
                            text = "Ok",
                            onClick = () => {
                                success.Close();
                            }
                        });
                    }
                });
            }

            if (Input.GetKeyDown(KeyCode.B)) {
                var things = new BoolThing[10];
                for (int i = 0; i < things.Length; i++) {
                    things[i] = new BoolThing() {
                        name = "Thing " + i,
                        value = i % 2 == 0
                    };
                }

                var checklist = UserInterface.Instance.CreateList(new ListData() {
                    title = "Toggle List"
                });
                checklist.AddButton(new ButtonData() {
                    text = "Cancel",
                    onClick = () => {
                        checklist.Close();
                    }
                });
                checklist.AddButton(new ButtonData() {
                    text = "Done",
                    onClick = () => {
                        foreach (var thing in things) {
                            Debug.Log(thing.name + " is " + thing.value);
                        }

                        checklist.Close();
                    }
                });
                foreach (var thing in things) {
                    checklist.AddToggle(new ToggleData() {
                        text = thing.name,
                        isOn = thing.value,
                        onValueChanged = (value) => {
                            thing.value = value;
                        }
                    });
                }
            }

            if (Input.GetKeyDown(KeyCode.C)) {
                var things = new BoolThing[10];
                for (int i = 0; i < things.Length; i++) {
                    things[i] = new BoolThing() {
                        name = "Thing " + i,
                        value = false
                    };
                }

                var radiolist = UserInterface.Instance.CreateList(new ListData() {
                    title = "Radio List",
                    message = "This is a message"
                });
                radiolist.AddButton(new ButtonData() {
                    text = "Cancel",
                    onClick = () => {
                        radiolist.Close();
                    }
                });
                radiolist.AddButton(new ButtonData() {
                    text = "Done",
                    onClick = () => {
                        foreach (var thing in things) {
                            Debug.Log(thing.name + " is " + thing.value);
                        }

                        radiolist.Close();
                    }
                });
                foreach (var thing in things) {
                    radiolist.AddRadio(new RadioData() {
                        text = thing.name,
                        isOn = thing.value,
                        onValueChanged = (value) => {
                            thing.value = value;
                        }
                    });
                }
            }
        }

        public class BoolThing {
            public string name;
            public bool value;
        }
    }
}
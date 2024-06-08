// GENERATED AUTOMATICALLY FROM 'Assets/battleInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @BattleInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @BattleInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""battleInputs"",
    ""maps"": [
        {
            ""name"": ""BattleMenu"",
            ""id"": ""6b8575a7-764b-4c80-b0fe-6e7af56d181e"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Value"",
                    ""id"": ""e51c19d0-22ca-4ea5-bc36-5894cd0a7cce"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""7ba3c17c-89c0-4331-ab8c-fc8e04902f91"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""a0a5a239-a936-4a5a-a4d8-8eda01ac2a1a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Swap"",
                    ""type"": ""Button"",
                    ""id"": ""901d4e41-1f05-4dff-a86d-acdad6c891cc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UndoSwap"",
                    ""type"": ""Button"",
                    ""id"": ""4af28011-f23d-42dd-a724-633d774c45a0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""A"",
                    ""type"": ""Button"",
                    ""id"": ""480ed432-ac9e-43eb-875e-54f0a715c29f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""B"",
                    ""type"": ""Button"",
                    ""id"": ""8ec36a43-df1e-456a-82c2-c52eaf4bb7b5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Direction"",
                    ""type"": ""Button"",
                    ""id"": ""6a725c17-81c9-4068-86f4-4523c3162920"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""keyboard"",
                    ""id"": ""6fd68240-192a-4a9b-8091-0ed1bf4875e2"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""3f0aef39-bf62-426c-a45c-bdc4250a7167"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""keyboard"",
                    ""id"": ""c8e36ef7-9875-448d-b5f1-df1fe0df3826"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""bfe803e5-d37d-4d3e-a8a4-4667a8016bdb"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up/down"",
                    ""id"": ""5028fbee-38db-40ef-bc7d-37bf4e6b7c8c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""55ad8e54-5db0-4b0e-9814-461518804799"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""4d4ae494-308b-42d6-b9c3-eabe8a8b3fdb"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""keyboard"",
                    ""id"": ""3d4375ed-47a5-4d76-a459-aab3aa787f34"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swap"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""6c4fce8a-4592-435b-8c5d-1e3a6305aef2"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""keyboard"",
                    ""id"": ""ec093edc-b062-4418-bb50-dc00cb896c62"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UndoSwap"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""282c8dc4-ef19-4e05-b8c5-cba682efc5ff"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UndoSwap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""keyboard"",
                    ""id"": ""c162e5c0-92e3-4e9d-8ea9-c7d1cc63e41c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""A"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""8fb80ddf-b5ad-41f2-a62e-119adfdb2163"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""keyboard"",
                    ""id"": ""4706c530-c9c7-4a79-95af-7381e511732f"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""B"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""1c63fe97-a1aa-4286-bc4e-ae18a15820c2"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""B"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""553e8cfb-34cf-4624-9589-0a2e67d17c0a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""05398870-0f6a-487b-8aef-229d5d395920"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9a9cc2c5-ae30-41b8-ad7e-ef169d0e26b6"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7b8a211b-1595-4545-a973-f0e1eb57cc6b"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""83b80785-8a2e-4937-b747-24d2bc2c6c78"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""ActionCommands"",
            ""id"": ""090e6445-2c58-4623-89ee-c4080f54fee0"",
            ""actions"": [
                {
                    ""name"": ""L"",
                    ""type"": ""Button"",
                    ""id"": ""1745a2bd-f83e-4179-a2db-bb9c9d3e4319"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""R"",
                    ""type"": ""Button"",
                    ""id"": ""b6faf954-5885-4ba8-96a0-78d33de0ea17"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""A"",
                    ""type"": ""Button"",
                    ""id"": ""e3e62543-cc42-4b11-9d0f-4fbeebab8944"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""B"",
                    ""type"": ""Button"",
                    ""id"": ""7b2dcc28-f0c0-45cd-9bdd-fb0125256cb3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Direction"",
                    ""type"": ""Button"",
                    ""id"": ""56e4951f-6bd7-401e-abcc-65321755f43d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""keyboard"",
                    ""id"": ""97e2eda8-6402-455a-8c4a-1f168f1f43e1"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""L"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""15b22128-3907-4606-8eb5-337a7977c6f1"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""L"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""keyboard"",
                    ""id"": ""57327a45-136b-43a1-ba75-ff577f9813ba"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""R"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f9adca45-5f8a-41d1-bef0-8409bf56edfa"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""R"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""keyboard"",
                    ""id"": ""7daf3161-f256-4917-a829-827b899ca8a5"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""A"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""6c678311-10fb-497c-bfa7-d1db82a0668d"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""keyboard"",
                    ""id"": ""f080f18f-b8e3-436e-a4b6-8d057d95fba8"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""B"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""b311d7c9-1dc2-4cba-9182-8bb942bf33e9"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""B"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""e31d3410-502c-49ea-ac6f-7b4e4273c846"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""44765d6f-2169-40e2-a501-5d4955b11c3e"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6cbacb2c-ac75-40e2-93cc-b33e5c1e4f50"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7f9d9599-7ad0-4088-ac6a-b124d67af037"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""098c6e3d-7a10-45cf-a284-0131e420c0e1"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Target"",
            ""id"": ""42a6b3c0-3d55-48e8-9781-63f464ae08f9"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Button"",
                    ""id"": ""3bb2a23f-1648-4b69-a4d8-40e91a4cc742"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""d4242aa9-2fee-4b98-8910-68e601882b2a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""fd3981f1-11c5-405c-89e4-ca667ec5ab08"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""forward/back"",
                    ""id"": ""54115847-25fd-481d-9ba6-2699b185c2da"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""5d4e28ed-811d-4476-9b59-0176775dcbf6"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e5cc76bb-9662-4a0f-9858-bee27d0e62dc"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""81e38539-5871-4c25-9421-8468ffefc4e1"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""68d8cddf-d680-40fb-86fc-20388af5588a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""b7529d47-77e2-4cf9-bcb7-644f2ced57e2"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""dd085e9e-95e2-4e95-8872-496d3ed758c3"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""bd003229-694a-4a85-b091-82994528f029"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""5620b384-61c0-4cf2-a4d2-ea4c320fb86f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""keyboard"",
                    ""id"": ""02554c43-11c6-4ddb-b731-3fa03024d199"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""29fbdf82-2de4-48d7-a6c1-c0517d64b13b"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""66b4f98a-1210-4c05-8791-fd0bf2796038"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""keyboard"",
                    ""id"": ""b779a683-2918-446b-a68a-e7948bb5ce10"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ad8c72ab-6a2a-4202-b91e-bdce6626b8db"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""8ae625ba-8627-4612-b9ae-0f30f26a54d8"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Overworld"",
            ""id"": ""88738e3a-a94d-455d-9ac3-33c127f3243c"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""ef72dcb1-9338-4fa1-9cd2-12ba3deb6908"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""9104dcd3-d854-4831-804f-5c0b619c8a34"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""a496e8c9-ca0d-411a-ba39-7e8a25e4f208"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""ca37ec65-b709-4329-b3bc-082186b92ec6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NavigateMenu"",
                    ""type"": ""Button"",
                    ""id"": ""8fc3d1f4-fb63-4dae-b24c-ffd244bcfd73"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NavigateMenuPages"",
                    ""type"": ""Button"",
                    ""id"": ""81dcaf81-4122-42fe-8240-c1965a44dd6e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""b143fdf7-534e-4366-b5dd-da518be47a77"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c2f04e58-caf2-4e9a-b603-b2e5dbfc1cbe"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b4618100-63fe-498e-b259-a1af115de405"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""34e0f6e7-0d0a-43c5-b06e-5dac62d476f2"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9ef12642-46f3-41ab-8c81-31ca0251f01b"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""z"",
                    ""id"": ""655dc325-bf11-44b1-8f9e-bea8d28f3fc9"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""8d2d64f6-27fd-4a82-ac63-d46058448c9c"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""escape"",
                    ""id"": ""314fef14-65b7-418a-a4f0-caf7b0cf6cb5"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""aefe42f6-798e-4d42-86bd-5246976f0bc6"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f4d4f572-9118-43af-9e4b-39f2ba9a3447"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""0003f9c8-c7c2-4c48-8346-91b02f22ef34"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NavigateMenu"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0ab7aafa-7b97-4deb-a064-237f40960a17"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""NavigateMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d769f617-cb5b-476b-b137-a8bccc1bb0b4"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NavigateMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""11738ddd-ebd9-471b-8d5d-6d37c5554829"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NavigateMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0adf2908-c455-4268-aeb0-0f2396cd0365"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NavigateMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""9d024d18-18c1-4678-9296-b454283181cd"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NavigateMenuPages"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""0ce4348e-c879-436d-9dd5-7f3237070be8"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""NavigateMenuPages"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""64eb4b09-9357-4a17-9475-68ec992936e0"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""NavigateMenuPages"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""B"",
                    ""id"": ""4922a495-afc1-4b92-9a32-c6a364325780"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""bdb92a99-ea9f-4e3f-bda1-c44ddb8d903d"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Joystick"",
            ""bindingGroup"": ""Joystick"",
            ""devices"": [
                {
                    ""devicePath"": ""<Joystick>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""XR"",
            ""bindingGroup"": ""XR"",
            ""devices"": [
                {
                    ""devicePath"": ""<XRController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // BattleMenu
        m_BattleMenu = asset.FindActionMap("BattleMenu", throwIfNotFound: true);
        m_BattleMenu_Navigate = m_BattleMenu.FindAction("Navigate", throwIfNotFound: true);
        m_BattleMenu_Select = m_BattleMenu.FindAction("Select", throwIfNotFound: true);
        m_BattleMenu_Back = m_BattleMenu.FindAction("Back", throwIfNotFound: true);
        m_BattleMenu_Swap = m_BattleMenu.FindAction("Swap", throwIfNotFound: true);
        m_BattleMenu_UndoSwap = m_BattleMenu.FindAction("UndoSwap", throwIfNotFound: true);
        m_BattleMenu_A = m_BattleMenu.FindAction("A", throwIfNotFound: true);
        m_BattleMenu_B = m_BattleMenu.FindAction("B", throwIfNotFound: true);
        m_BattleMenu_Direction = m_BattleMenu.FindAction("Direction", throwIfNotFound: true);
        // ActionCommands
        m_ActionCommands = asset.FindActionMap("ActionCommands", throwIfNotFound: true);
        m_ActionCommands_L = m_ActionCommands.FindAction("L", throwIfNotFound: true);
        m_ActionCommands_R = m_ActionCommands.FindAction("R", throwIfNotFound: true);
        m_ActionCommands_A = m_ActionCommands.FindAction("A", throwIfNotFound: true);
        m_ActionCommands_B = m_ActionCommands.FindAction("B", throwIfNotFound: true);
        m_ActionCommands_Direction = m_ActionCommands.FindAction("Direction", throwIfNotFound: true);
        // Target
        m_Target = asset.FindActionMap("Target", throwIfNotFound: true);
        m_Target_Navigate = m_Target.FindAction("Navigate", throwIfNotFound: true);
        m_Target_Select = m_Target.FindAction("Select", throwIfNotFound: true);
        m_Target_Back = m_Target.FindAction("Back", throwIfNotFound: true);
        // Overworld
        m_Overworld = asset.FindActionMap("Overworld", throwIfNotFound: true);
        m_Overworld_Move = m_Overworld.FindAction("Move", throwIfNotFound: true);
        m_Overworld_Interact = m_Overworld.FindAction("Interact", throwIfNotFound: true);
        m_Overworld_Cancel = m_Overworld.FindAction("Cancel", throwIfNotFound: true);
        m_Overworld_Pause = m_Overworld.FindAction("Pause", throwIfNotFound: true);
        m_Overworld_NavigateMenu = m_Overworld.FindAction("NavigateMenu", throwIfNotFound: true);
        m_Overworld_NavigateMenuPages = m_Overworld.FindAction("NavigateMenuPages", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // BattleMenu
    private readonly InputActionMap m_BattleMenu;
    private IBattleMenuActions m_BattleMenuActionsCallbackInterface;
    private readonly InputAction m_BattleMenu_Navigate;
    private readonly InputAction m_BattleMenu_Select;
    private readonly InputAction m_BattleMenu_Back;
    private readonly InputAction m_BattleMenu_Swap;
    private readonly InputAction m_BattleMenu_UndoSwap;
    private readonly InputAction m_BattleMenu_A;
    private readonly InputAction m_BattleMenu_B;
    private readonly InputAction m_BattleMenu_Direction;
    public struct BattleMenuActions
    {
        private @BattleInputs m_Wrapper;
        public BattleMenuActions(@BattleInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate => m_Wrapper.m_BattleMenu_Navigate;
        public InputAction @Select => m_Wrapper.m_BattleMenu_Select;
        public InputAction @Back => m_Wrapper.m_BattleMenu_Back;
        public InputAction @Swap => m_Wrapper.m_BattleMenu_Swap;
        public InputAction @UndoSwap => m_Wrapper.m_BattleMenu_UndoSwap;
        public InputAction @A => m_Wrapper.m_BattleMenu_A;
        public InputAction @B => m_Wrapper.m_BattleMenu_B;
        public InputAction @Direction => m_Wrapper.m_BattleMenu_Direction;
        public InputActionMap Get() { return m_Wrapper.m_BattleMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BattleMenuActions set) { return set.Get(); }
        public void SetCallbacks(IBattleMenuActions instance)
        {
            if (m_Wrapper.m_BattleMenuActionsCallbackInterface != null)
            {
                @Navigate.started -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnNavigate;
                @Select.started -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnSelect;
                @Back.started -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnBack;
                @Swap.started -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnSwap;
                @Swap.performed -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnSwap;
                @Swap.canceled -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnSwap;
                @UndoSwap.started -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnUndoSwap;
                @UndoSwap.performed -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnUndoSwap;
                @UndoSwap.canceled -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnUndoSwap;
                @A.started -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnA;
                @A.performed -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnA;
                @A.canceled -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnA;
                @B.started -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnB;
                @B.performed -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnB;
                @B.canceled -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnB;
                @Direction.started -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnDirection;
                @Direction.performed -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnDirection;
                @Direction.canceled -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnDirection;
            }
            m_Wrapper.m_BattleMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
                @Swap.started += instance.OnSwap;
                @Swap.performed += instance.OnSwap;
                @Swap.canceled += instance.OnSwap;
                @UndoSwap.started += instance.OnUndoSwap;
                @UndoSwap.performed += instance.OnUndoSwap;
                @UndoSwap.canceled += instance.OnUndoSwap;
                @A.started += instance.OnA;
                @A.performed += instance.OnA;
                @A.canceled += instance.OnA;
                @B.started += instance.OnB;
                @B.performed += instance.OnB;
                @B.canceled += instance.OnB;
                @Direction.started += instance.OnDirection;
                @Direction.performed += instance.OnDirection;
                @Direction.canceled += instance.OnDirection;
            }
        }
    }
    public BattleMenuActions @BattleMenu => new BattleMenuActions(this);

    // ActionCommands
    private readonly InputActionMap m_ActionCommands;
    private IActionCommandsActions m_ActionCommandsActionsCallbackInterface;
    private readonly InputAction m_ActionCommands_L;
    private readonly InputAction m_ActionCommands_R;
    private readonly InputAction m_ActionCommands_A;
    private readonly InputAction m_ActionCommands_B;
    private readonly InputAction m_ActionCommands_Direction;
    public struct ActionCommandsActions
    {
        private @BattleInputs m_Wrapper;
        public ActionCommandsActions(@BattleInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @L => m_Wrapper.m_ActionCommands_L;
        public InputAction @R => m_Wrapper.m_ActionCommands_R;
        public InputAction @A => m_Wrapper.m_ActionCommands_A;
        public InputAction @B => m_Wrapper.m_ActionCommands_B;
        public InputAction @Direction => m_Wrapper.m_ActionCommands_Direction;
        public InputActionMap Get() { return m_Wrapper.m_ActionCommands; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActionCommandsActions set) { return set.Get(); }
        public void SetCallbacks(IActionCommandsActions instance)
        {
            if (m_Wrapper.m_ActionCommandsActionsCallbackInterface != null)
            {
                @L.started -= m_Wrapper.m_ActionCommandsActionsCallbackInterface.OnL;
                @L.performed -= m_Wrapper.m_ActionCommandsActionsCallbackInterface.OnL;
                @L.canceled -= m_Wrapper.m_ActionCommandsActionsCallbackInterface.OnL;
                @R.started -= m_Wrapper.m_ActionCommandsActionsCallbackInterface.OnR;
                @R.performed -= m_Wrapper.m_ActionCommandsActionsCallbackInterface.OnR;
                @R.canceled -= m_Wrapper.m_ActionCommandsActionsCallbackInterface.OnR;
                @A.started -= m_Wrapper.m_ActionCommandsActionsCallbackInterface.OnA;
                @A.performed -= m_Wrapper.m_ActionCommandsActionsCallbackInterface.OnA;
                @A.canceled -= m_Wrapper.m_ActionCommandsActionsCallbackInterface.OnA;
                @B.started -= m_Wrapper.m_ActionCommandsActionsCallbackInterface.OnB;
                @B.performed -= m_Wrapper.m_ActionCommandsActionsCallbackInterface.OnB;
                @B.canceled -= m_Wrapper.m_ActionCommandsActionsCallbackInterface.OnB;
                @Direction.started -= m_Wrapper.m_ActionCommandsActionsCallbackInterface.OnDirection;
                @Direction.performed -= m_Wrapper.m_ActionCommandsActionsCallbackInterface.OnDirection;
                @Direction.canceled -= m_Wrapper.m_ActionCommandsActionsCallbackInterface.OnDirection;
            }
            m_Wrapper.m_ActionCommandsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @L.started += instance.OnL;
                @L.performed += instance.OnL;
                @L.canceled += instance.OnL;
                @R.started += instance.OnR;
                @R.performed += instance.OnR;
                @R.canceled += instance.OnR;
                @A.started += instance.OnA;
                @A.performed += instance.OnA;
                @A.canceled += instance.OnA;
                @B.started += instance.OnB;
                @B.performed += instance.OnB;
                @B.canceled += instance.OnB;
                @Direction.started += instance.OnDirection;
                @Direction.performed += instance.OnDirection;
                @Direction.canceled += instance.OnDirection;
            }
        }
    }
    public ActionCommandsActions @ActionCommands => new ActionCommandsActions(this);

    // Target
    private readonly InputActionMap m_Target;
    private ITargetActions m_TargetActionsCallbackInterface;
    private readonly InputAction m_Target_Navigate;
    private readonly InputAction m_Target_Select;
    private readonly InputAction m_Target_Back;
    public struct TargetActions
    {
        private @BattleInputs m_Wrapper;
        public TargetActions(@BattleInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate => m_Wrapper.m_Target_Navigate;
        public InputAction @Select => m_Wrapper.m_Target_Select;
        public InputAction @Back => m_Wrapper.m_Target_Back;
        public InputActionMap Get() { return m_Wrapper.m_Target; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TargetActions set) { return set.Get(); }
        public void SetCallbacks(ITargetActions instance)
        {
            if (m_Wrapper.m_TargetActionsCallbackInterface != null)
            {
                @Navigate.started -= m_Wrapper.m_TargetActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_TargetActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_TargetActionsCallbackInterface.OnNavigate;
                @Select.started -= m_Wrapper.m_TargetActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_TargetActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_TargetActionsCallbackInterface.OnSelect;
                @Back.started -= m_Wrapper.m_TargetActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_TargetActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_TargetActionsCallbackInterface.OnBack;
            }
            m_Wrapper.m_TargetActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
            }
        }
    }
    public TargetActions @Target => new TargetActions(this);

    // Overworld
    private readonly InputActionMap m_Overworld;
    private IOverworldActions m_OverworldActionsCallbackInterface;
    private readonly InputAction m_Overworld_Move;
    private readonly InputAction m_Overworld_Interact;
    private readonly InputAction m_Overworld_Cancel;
    private readonly InputAction m_Overworld_Pause;
    private readonly InputAction m_Overworld_NavigateMenu;
    private readonly InputAction m_Overworld_NavigateMenuPages;
    public struct OverworldActions
    {
        private @BattleInputs m_Wrapper;
        public OverworldActions(@BattleInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Overworld_Move;
        public InputAction @Interact => m_Wrapper.m_Overworld_Interact;
        public InputAction @Cancel => m_Wrapper.m_Overworld_Cancel;
        public InputAction @Pause => m_Wrapper.m_Overworld_Pause;
        public InputAction @NavigateMenu => m_Wrapper.m_Overworld_NavigateMenu;
        public InputAction @NavigateMenuPages => m_Wrapper.m_Overworld_NavigateMenuPages;
        public InputActionMap Get() { return m_Wrapper.m_Overworld; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(OverworldActions set) { return set.Get(); }
        public void SetCallbacks(IOverworldActions instance)
        {
            if (m_Wrapper.m_OverworldActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnMove;
                @Interact.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnInteract;
                @Cancel.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnCancel;
                @Pause.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnPause;
                @NavigateMenu.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnNavigateMenu;
                @NavigateMenu.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnNavigateMenu;
                @NavigateMenu.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnNavigateMenu;
                @NavigateMenuPages.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnNavigateMenuPages;
                @NavigateMenuPages.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnNavigateMenuPages;
                @NavigateMenuPages.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnNavigateMenuPages;
            }
            m_Wrapper.m_OverworldActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @NavigateMenu.started += instance.OnNavigateMenu;
                @NavigateMenu.performed += instance.OnNavigateMenu;
                @NavigateMenu.canceled += instance.OnNavigateMenu;
                @NavigateMenuPages.started += instance.OnNavigateMenuPages;
                @NavigateMenuPages.performed += instance.OnNavigateMenuPages;
                @NavigateMenuPages.canceled += instance.OnNavigateMenuPages;
            }
        }
    }
    public OverworldActions @Overworld => new OverworldActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_TouchSchemeIndex = -1;
    public InputControlScheme TouchScheme
    {
        get
        {
            if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
            return asset.controlSchemes[m_TouchSchemeIndex];
        }
    }
    private int m_JoystickSchemeIndex = -1;
    public InputControlScheme JoystickScheme
    {
        get
        {
            if (m_JoystickSchemeIndex == -1) m_JoystickSchemeIndex = asset.FindControlSchemeIndex("Joystick");
            return asset.controlSchemes[m_JoystickSchemeIndex];
        }
    }
    private int m_XRSchemeIndex = -1;
    public InputControlScheme XRScheme
    {
        get
        {
            if (m_XRSchemeIndex == -1) m_XRSchemeIndex = asset.FindControlSchemeIndex("XR");
            return asset.controlSchemes[m_XRSchemeIndex];
        }
    }
    public interface IBattleMenuActions
    {
        void OnNavigate(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
        void OnSwap(InputAction.CallbackContext context);
        void OnUndoSwap(InputAction.CallbackContext context);
        void OnA(InputAction.CallbackContext context);
        void OnB(InputAction.CallbackContext context);
        void OnDirection(InputAction.CallbackContext context);
    }
    public interface IActionCommandsActions
    {
        void OnL(InputAction.CallbackContext context);
        void OnR(InputAction.CallbackContext context);
        void OnA(InputAction.CallbackContext context);
        void OnB(InputAction.CallbackContext context);
        void OnDirection(InputAction.CallbackContext context);
    }
    public interface ITargetActions
    {
        void OnNavigate(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
    }
    public interface IOverworldActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnNavigateMenu(InputAction.CallbackContext context);
        void OnNavigateMenuPages(InputAction.CallbackContext context);
    }
}

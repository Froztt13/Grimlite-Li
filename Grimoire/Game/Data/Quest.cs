using Grimoire.Tools;
using Newtonsoft.Json;
using System.Collections.Generic;
using Grimoire.Networking;
using System.Linq;
using System;

namespace Grimoire.Game.Data
{
	public class Quest
	{
		[JsonProperty("sFaction")]
		public string Faction
		{
			get;
			set;
		}

		[JsonProperty("iClass")]
		public int ClassPointsReward
		{
			get;
			set;
		}

		[JsonProperty("oRewards")]
		[JsonConverter(typeof(QuestRewardConverter))]
		public List<ItemBase> oRewards
		{
			get;
			set;
		} //= new List<ItemBase>();

		//[JsonProperty("oReqd")]
		//[JsonConverter(typeof(QuestReqConverter))]
		//public List<ItemBase> oReqd
		//{
		//    get;
		//    set;
		//} //= new List<ItemBase>();

		[JsonProperty("sDesc")]
		public string Description
		{
			get;
			set;
		}

		[JsonProperty("iReqRep")]
		public int RequiredReputation
		{
			get;
			set;
		}

		[JsonProperty("iRep")]
		public int ReputationReward
		{
			get;
			set;
		}

		[JsonProperty("iLvl")]
		public int Level
		{
			get;
			set;
		}

		//[JsonProperty("turnin")] changed to the custom one from the SWF
		[JsonProperty("RequiredItems")]
		public List<InventoryItem> RequiredItems
		{
			get;
			set;
		}

		[JsonProperty("iGold")]
		public int GoldReward
		{
			get;
			set;
		}

		[JsonProperty("iReqCP")]
		public int RequiredClassPoints
		{
			get;
			set;
		}

		[JsonProperty("QuestID")]
		public int Id
		{
			get;
			set;
		}

		[JsonConverter(typeof(BoolConverter))]
		[JsonProperty("bOnce")]
		public bool IsNotRepeatable
		{
			get;
			set;
		}

		[JsonProperty("iExp")]
		public int ExperienceReward
		{
			get;
			set;
		}

		[JsonProperty("reward")]
		public List<InventoryItem> Rewards
		{
			get;
			set;
		}

		[JsonProperty("sName")]
		public string Name
		{
			get;
			set;
		}

		[JsonConverter(typeof(BoolConverter))]
		[JsonProperty("bUpg")]
		public bool IsMemberOnly
		{
			get;
			set;
		}

		[JsonProperty("FactionID")]
		public int FactionId
		{
			get;
			set;
		}

        private int? _iSlot;
        [JsonProperty("iSlot")]
        public int? ISlot
        {
            get => (_iSlot == null || _iSlot == -1) ? 0 : _iSlot;
            set => _iSlot = value;
        }

        [JsonProperty("iValue")]
		public int IValue
		{
			get;
			set;
		}

		public string ItemId
		{
			get;
			set;
		}
		public bool CompleteInBlank
		{
			get;
			set;
		}

		public bool SafeRelogin
		{
			get;
			set;
		}

		public string Text
		{
			get;
			set;
		}

		public bool IsInProgress
		{
			get
			{
				return Flash.Call<bool>("IsInProgress", new string[1]
				{
					Id.ToString()
				});
			}
		}

		public bool CanComplete
		{
			get
			{
				return Flash.Call<bool>("CanComplete", new string[1]
				{
					Id.ToString()
				});
			}
		}

		public void Accept()
		{
			Flash.Call("Accept", Id.ToString());
		}

        public void GhostAccept()
        {
			Proxy.Instance.SendToServer($"%xt%zm%acceptQuest%1%{Id}%");
        }

        public void Complete(int qty = 1, bool max = false)
		{
			if (max)
				qty = getMax();
            if (!string.IsNullOrEmpty(ItemId))
			{
				Flash.Call("Complete", Id.ToString(), qty, ItemId);
			}
			else
			{
				Flash.Call("Complete", Id.ToString(), qty);
			}
		}
        private int getMax()
        {
            var quest = Player.Quests.Quest(Id);
            int complete = 10000;
            foreach (var req in quest.RequiredItems)
            {
                //LogForm.Instance.devDebug($"{req.Name} is temp? {req.IsTemporary}");
                int owned = 0;
                if (req.IsTemporary)
                {
                    var tempItem = Player.TempInventory.Items.FirstOrDefault(i => i.Id == req.Id);
                    owned = tempItem?.Quantity ?? 0;
                }
                else
                {
                    var invItem = Player.Inventory.Items.FirstOrDefault(i => i.Id == req.Id);
                    owned = invItem?.Quantity ?? 0;
                }

                int required = req.Quantity;
                int possible = owned / required;

                complete = Math.Min(complete, owned / req.Quantity);
            }
			return complete;
        }
        public override string ToString()
		{
			string itemId = ItemId != null ? $": {ItemId}" : "";
			string safeRelogin = SafeRelogin ? " [SafeRelogin]" : "";
			return $"{Id}{itemId}{safeRelogin}";
		}

		#region ShouldSerialize
		public bool ShouldSerializeFaction() => false;
        public bool ShouldSerializeISlot() => false;

        public bool ShouldSerializeClassPointsReward() => false;

		public bool ShouldSerializeDescription() => false;

		public bool ShouldSerializeRequiredReputation() => false;

		public bool ShouldSerializeReputationReward() => false;

		public bool ShouldSerializeLevel() => false;

		public bool ShouldSerializeGoldReward => false;

		public bool ShouldSerializeRequiredClassPoints => false;

		public bool ShouldSerializeIsNotRepeatable => false;

		public bool ShouldSerializeExperienceReward => false;

		public bool ShouldSerializeRewards => false;

		public bool ShouldSerializeName => false;

		public bool ShouldSerializeIsMemberOnly => false;

		public bool ShouldSerializeFactionId => false;

		public bool ShouldSerializeIsInProgress => false;

		public bool ShouldSerializeCanComplete => false;
		#endregion
	}
}
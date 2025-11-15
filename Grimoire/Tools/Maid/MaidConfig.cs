using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Tools.Maid
{
	internal class MaidConfig
	{
		public string Target;

		public string SkillList;

		public int SkillDelay;

		public bool WaitSkill;

		public bool StopFailedGoto;

		public bool LockedZoneHandler;

		public string LockedZoneHandlerMaps;

		public bool WhitelistMap;

		public string WhitelistMapMaps;

		public int RelogDelay;

		public bool GlobalHotkey;

		public bool SafeSkill;

		public string SafeSkillList;

		public int SafeSkillHP;

		public bool SafeSkill2;

		public string SafeSkillList2;

		public int? SafeSkillHP2;

		public bool BuffStopAttack;

		public string BuffStopAttackList;

		public bool AttackPriority;

		public string AttackPriorityMonster;

		public bool PartyCmd;

		public bool CopyWalk;

		public string SpecialMsg;

		public int SpecialAct;

		public bool AntiCounter;

		public int UltraBossExtra;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Tools.Maid
{
    public static class ClassPreset
    {
        static bool heal = true;
        public static void LR()
        {
            // skill list
            UI.Maid.MaidRemake.Instance.tbSkillList.Text = "3,1,2,4";

			// heal skill
			UI.Maid.MaidRemake.Instance.tbSkillHpLower.Text = String.Empty;
            UI.Maid.MaidRemake.Instance.numHpLowerPercentage.Value = 60;

			// buff skill
			UI.Maid.MaidRemake.Instance.tbSkillStop.Text = "3";

            // additional settings
            UI.Maid.MaidRemake.Instance.cbWaitSkill.Checked = false;
        }

        public static void LC()
        {
			// skill list
			UI.Maid.MaidRemake.Instance.tbSkillList.Text = "1,2,4,1,2,4,3";

            // heal skill
            UI.Maid.MaidRemake.Instance.tbSkillHpLower.Text = "3";
			UI.Maid.MaidRemake.Instance.numHpLowerPercentage.Value = 60;
            heal = true;

			// buff skill
			UI.Maid.MaidRemake.Instance.tbSkillStop.Text = String.Empty;

            // additional settings
            UI.Maid.MaidRemake.Instance.cbWaitSkill.Checked = false;
        }

        public static void LOO()
        {
            // skill list
            UI.Maid.MaidRemake.Instance.tbSkillList.Text = "2,1,3,4";

            // heal skill
            UI.Maid.MaidRemake.Instance.tbSkillHpLower.Text = String.Empty;
            UI.Maid.MaidRemake.Instance.numHpLowerPercentage.Value = 60;

            // buff skill
            UI.Maid.MaidRemake.Instance.tbSkillStop.Text = "1,2,3";

            // additional settings
            UI.Maid.MaidRemake.Instance.cbWaitSkill.Checked = false;
        }
        public static void AI()
        {
            // skill list
            UI.Maid.MaidRemake.Instance.tbSkillList.Text = "2,3,4";

            // buff skill
            UI.Maid.MaidRemake.Instance.tbSkillStop.Text = "5";
            heal = false;

            // additional settings
            UI.Maid.MaidRemake.Instance.cbWaitSkill.Checked = false;
        }
        public static void AM()
        {
            // skill list
            UI.Maid.MaidRemake.Instance.tbSkillList.Text = "2,3,1";

            // buff skill
            UI.Maid.MaidRemake.Instance.tbSkillStop.Text = "5";

            // additional settings
            UI.Maid.MaidRemake.Instance.cbWaitSkill.Checked = false;
        }
        public static void CSH()
        {
            // skill list
            UI.Maid.MaidRemake.Instance.tbSkillList.Text = "3,3,3,2";

            // heal skill
            UI.Maid.MaidRemake.Instance.tbSkillHpLower.Text = String.Empty;
            UI.Maid.MaidRemake.Instance.numHpLowerPercentage.Value = 80;
            heal = false;

            // buff skill
            UI.Maid.MaidRemake.Instance.tbSkillStop.Text = "5";

            // additional settings
            UI.Maid.MaidRemake.Instance.cbWaitSkill.Checked = true;
        }

        public static void CSHGunslinger()
        {
            // skill list
            UI.Maid.MaidRemake.Instance.tbSkillList.Text = "3,2,3,2,3,4,1";

            // heal skill
            UI.Maid.MaidRemake.Instance.tbSkillHpLower.Text = String.Empty;
            UI.Maid.MaidRemake.Instance.numHpLowerPercentage.Value = 80;
            heal = false;

            // buff skill
            UI.Maid.MaidRemake.Instance.tbSkillStop.Text = "5";

            // additional settings
            UI.Maid.MaidRemake.Instance.cbWaitSkill.Checked = true;
        }

        public static void SC()
        {
            // skill list
            UI.Maid.MaidRemake.Instance.tbSkillList.Text = "3,1,2,4";

            // heal skill
            UI.Maid.MaidRemake.Instance.tbSkillHpLower.Text = String.Empty;
            UI.Maid.MaidRemake.Instance.numHpLowerPercentage.Value = 60;

            // buff skill
            UI.Maid.MaidRemake.Instance.tbSkillStop.Text = "2,3";

            // additional settings
            UI.Maid.MaidRemake.Instance.cbWaitSkill.Checked = false;
        }

        public static void AP()
        {
            // skill list
            UI.Maid.MaidRemake.Instance.tbSkillList.Text = "1,2,3";

            // heal skill
            UI.Maid.MaidRemake.Instance.tbSkillHpLower.Text = String.Empty;
            UI.Maid.MaidRemake.Instance.numHpLowerPercentage.Value = 60;

            // buff skill
            UI.Maid.MaidRemake.Instance.tbSkillStop.Text = String.Empty;

            // additional settings
            UI.Maid.MaidRemake.Instance.cbWaitSkill.Checked = false;
        }

        public static void CCMD()
        {
            // skill list
            UI.Maid.MaidRemake.Instance.tbSkillList.Text = "1,1,1,1,3";

            // heal skill
            UI.Maid.MaidRemake.Instance.tbSkillHpLower.Text = "2";
            UI.Maid.MaidRemake.Instance.numHpLowerPercentage.Value = 75;

            // buff skill
            UI.Maid.MaidRemake.Instance.tbSkillStop.Text = String.Empty;

            // additional settings
            UI.Maid.MaidRemake.Instance.cbWaitSkill.Checked = true;
        }

        public static void SSOT()
        {
            // skill list
            UI.Maid.MaidRemake.Instance.tbSkillList.Text = "4,2,3,1,2";

            // heal skill
            UI.Maid.MaidRemake.Instance.tbSkillHpLower.Text = String.Empty;
            UI.Maid.MaidRemake.Instance.numHpLowerPercentage.Value = 60;

            // buff skill
            UI.Maid.MaidRemake.Instance.tbSkillStop.Text = String.Empty;

            // additional settings
            UI.Maid.MaidRemake.Instance.cbWaitSkill.Checked = true;
        }

        public static void NCM()
        {
            // skill list
            UI.Maid.MaidRemake.Instance.tbSkillList.Text = "1,2,4,1,2,1,3";

            // heal skill
            UI.Maid.MaidRemake.Instance.tbSkillHpLower.Text = String.Empty;
            UI.Maid.MaidRemake.Instance.numHpLowerPercentage.Value = 60;

            // buff skill
            UI.Maid.MaidRemake.Instance.tbSkillStop.Text = String.Empty;

            // additional settings
            UI.Maid.MaidRemake.Instance.cbWaitSkill.Checked = true;
        }

        public static void TK()
        {
            // skill list
            UI.Maid.MaidRemake.Instance.tbSkillList.Text = "1,2,1,4,1,3";

            // heal skill
            UI.Maid.MaidRemake.Instance.tbSkillHpLower.Text = String.Empty;
            UI.Maid.MaidRemake.Instance.numHpLowerPercentage.Value = 60;

            // buff skill
            UI.Maid.MaidRemake.Instance.tbSkillStop.Text = String.Empty;

            // additional settings
            UI.Maid.MaidRemake.Instance.cbWaitSkill.Checked = true;
        }

        // for making sure the setting is applied

        public static void cbClear()
        {
            UI.Maid.MaidRemake.Instance.cbHpLower.Checked = false;
            UI.Maid.MaidRemake.Instance.cbSkillStop.Checked = false;
        }

        public static void cbSet()
        {
            if (heal)
            {
                UI.Maid.MaidRemake.Instance.cbHpLower.Checked = true;
            }
            else
            {
                UI.Maid.MaidRemake.Instance.cbHpLower.Checked = false;
            }
            UI.Maid.MaidRemake.Instance.cbSkillStop.Checked = true;
        } 
    }
}
